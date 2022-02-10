namespace roguelike_spbu
{
    class FOV
    {
        static List<List<(int, int)>> Rays = new List<List<(int, int)>>();
        static int A;
        static int B;
        static bool filled = false;

        static int F(int x, int y, int a, int b){
            return b * b * x * x + a * a * y * y - a * a * b * b;
        }
        public static List<List<(int, int)>> GetRaysInEllipse(int a, int b)
        {
            if (A == b && B == b && filled){
                return Rays;
            }
            A = a;
            B = b;
            filled = true;

            Rays = new List<List<(int, int)>>();
            List<(int, int)> rectanglePoints = new List<(int, int)>();

            for (int i = 1 - a; i < a; i++)
            {
                rectanglePoints.Add((1 - b, i));
                rectanglePoints.Add((b - 1, i));
            }
            for (int i = 2 - b; i < (b - 1); i++)
            {
                rectanglePoints.Add((i, 1 - a));
                rectanglePoints.Add((i, a - 1));
            }
            foreach ((int, int) rectanglePoint in rectanglePoints)
            {
                List<(int, int)> linePoints = Primitives.GetLinePoints(rectanglePoint.Item1, rectanglePoint.Item2);
                List<(int, int)> ray = new List<(int, int)>();
                foreach ((int, int) linePoint in linePoints)
                {
                    if (F(linePoint.Item1, linePoint.Item2, b, a) < 0){
                        ray.Add(linePoint);
                    }
                }
                Rays.Add(ray);
            }

            return Rays;
        }
        
    }
}