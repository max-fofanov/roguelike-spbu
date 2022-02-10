using System.Collections.Generic;

namespace roguelike_spbu
{
    class Primitives
    {
        public static List<(int, int)> GetEllipsePoints(int a, int b)
        {
            List<(int, int)> points = new List<(int, int)>();
            int x = 0;
            int y = b;
            points.Add((x, y));
            points.Add((-x, y));
            points.Add((x, -y));
            points.Add((-x, -y));
            while (a * a * (2 * y - 1) > 2 * b * b * (x + 1))
            /* 
            Doing "horizontal" part of the ellipse
            Until slope is 45 degrees (tan == 1)
            */
            {
                if ((4 * b * b * (x + 1) * (x + 1) + a * a * (2 * y - 1) * (2 * y - 1) - 4 * a * a * b * b) >= 0)
                {
                    /*
                    *---*
                    |   |
                    |   * <- if that point in ellipse choose point above
                    |   |    otherwise below
                    *---*
                    */
                    y--;
                }
                x++;
                points.Add((x, y));
                points.Add((-x, y));
                points.Add((x, -y));
                points.Add((-x, -y));
            }
            while (y >= 0)
            {
                if ((b * b * (2 * x + 1) * (2 * x + 1) + 4 * a * a * (y - 1) * (y - 1) - 4 * a * a * b * b) < 0)
                {
                    /*
                    *---*
                    |   |
                    |   |
                    |   |
                    *-*-*
                      /\ - if that point in ellipse choose point to the right
                           otherwise to the left
                    */
                    x++;
                }
                y--;
                points.Add((x, y));
                points.Add((-x, y));
                points.Add((x, -y));
                points.Add((-x, -y));
            }
            return points;
        }
        public static List<(int, int)> GetLinePoints(int x1, int y1)
        {
            List<(int, int)> points = new List<(int, int)>();
            int A = y1;
            int B = -1 * x1;
            int SignA = A < 0 ? -1 : 1;
            int SignB = B < 0 ? -1 : 1;

            int f = 0;
            int x = 0;
            int y = 0;

            points.Add((x, y));
            if (A * A < B * B) // |A| < |B|
            {
                while (x != x1 || y != y1)
                {
                    f += A * SignA;
                    if (f > 0)
                    {
                        f -= B * SignB;
                        y += SignA;
                    }
                    x -= SignB;
                    points.Add((x, y));
                }
            }
            else
            {
                while (x != x1 || y != y1)
                {
                    f += B * SignB;
                    if (f > 0)
                    {
                        f -= A * SignA;
                        x -= SignB;
                    }
                    y += SignA;
                    points.Add((x, y));
                }
            }
            return points;
        }
    }
}