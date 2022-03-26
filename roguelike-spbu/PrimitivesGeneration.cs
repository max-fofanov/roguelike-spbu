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
        static int? WeightOfPlace(Map map, List<Entity> entities, Player player, int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.Height || y >= map.Width)
                return null;

            if (map.Tiles[x][y].Impassable)
                return null;

            if (entities.Where(e => e.X == x && e.Y == y && e is Chest).Count() > 0)
                return null;

            if (entities.Where(e => e.X == x && e.Y == y).Count() > 0)
                return 3;

            if (player.X == x && player.Y == y)
                return 1;

            return 1;
        }
        static List<(int, int, int)> GetNeighbors(Map map, List<Entity> entities, Player player, (int, int) point)
        {
            List<(int, int, int)> neighbors = new List<(int, int, int)>();

            int x = point.Item1;
            int y = point.Item2;

            (int, int)[] possibleNeightbors = { (x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1) };

            foreach ((int px, int py) in possibleNeightbors)
            {
                int? weight = WeightOfPlace(map, entities, player, px, py);
                if (weight != null)
                {
                    neighbors.Add((px, py, weight ?? 0));
                }
            }

            return neighbors;
        }
        static int GetDistanseToGoal((int, int) start, (int, int) goal)
        {
            // return Math.Abs(start.Item1 - goal.Item1) + Math.Abs(start.Item2 - goal.Item2);
            return (int)(Math.Pow(start.Item1 - goal.Item1, 2) + Math.Pow(start.Item2 - goal.Item2, 2));
        }
        public static List<(int, int)> AStarSearch(Map map, List<Entity> entities, Player player, (int, int) start, (int, int) goal)
        {
            List<(int, int)> path = new List<(int, int)>();
            PriorityQueue<(int, int), int> queue = new PriorityQueue<(int, int), int>();
            Dictionary<(int, int), (int, int)?> came_from = new Dictionary<(int, int), (int, int)?>();
            Dictionary<(int, int), int> cost = new Dictionary<(int, int), int>();

            bool finished = false;

            queue.Enqueue(start, 0);
            came_from[start] = null;
            cost[start] = 0;

            while (queue.Count > 0)
            {
                (int, int) current = queue.Dequeue();

                if (current == goal)
                {
                    finished = true;
                    break;
                }

                foreach ((int nx, int ny, int weight) in GetNeighbors(map, entities, player, current))
                {
                    (int, int) next = (nx, ny);
                    int newCost = cost[current] + weight;
                    if (!cost.TryGetValue(next, out int oldCost) || newCost < oldCost)
                    {
                        cost[next] = newCost;
                        int priority = newCost + GetDistanseToGoal(next, goal);
                        queue.Enqueue(next, priority);
                        came_from[next] = current;
                    }
                }
            }
            if (finished)
            {
                (int, int) pathPoint = goal;
                path.Add(pathPoint);
                while (pathPoint != start)
                {
                    pathPoint = came_from[pathPoint] ?? start;
                    path.Add(pathPoint);
                }

                path.Reverse();
            }

            return path;
        }
    }
}