using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Enemy : Entity
    {
        public Enemy(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.RangeOfView = 1000;
            this.Symbol = "%";
            this.PrimaryForegroundColor = Color.Yellow;
        }
        static bool IsNewPlaceOK(Map map, Entity[] entities, Player player, int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.Height || y >= map.Width)
                return false;

            if (map.Tiles[x][y].Impassable)
                return false;

            if (entities.Where(e => e.X == x && e.Y == y).Count() > 0)
                return false;

            if (player.X == x && player.Y == y)
                return false;

            return true;
        }
        static List<(int, int)> GetNeighbors(Map map, Entity[] entities, Player player, (int, int) point, (int, int) goal)
        {
            List<(int, int)> neighbors = new List<(int, int)>();

            int x = point.Item1;
            int y = point.Item2;

            if (IsNewPlaceOK(map, entities, player, x + 1, y) || (x + 1, y) == goal)
                neighbors.Add((x + 1, y));
            if (IsNewPlaceOK(map, entities, player, x - 1, y) || (x - 1, y) == goal)
                neighbors.Add((x - 1, y));
            if (IsNewPlaceOK(map, entities, player, x, y + 1) || (x, y + 1) == goal)
                neighbors.Add((x, y + 1));
            if (IsNewPlaceOK(map, entities, player, x, y - 1) || (x, y - 1) == goal)
                neighbors.Add((x, y - 1));

            return neighbors;
        }
        static int GetDistanseToGoal((int, int) start, (int, int) goal)
        {
            return Math.Abs(start.Item1 - goal.Item1) + Math.Abs(start.Item2 - goal.Item2);
        }
        public static List<(int, int)> AStarSearch(Map map, Entity[] entities, Player player, (int, int) start, (int, int) goal)
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
                
                foreach ((int, int) next in GetNeighbors(map, entities, player, current, goal))
                {
                    int newCost = cost[current] + 1;
                    if (!cost.TryGetValue(next, out int oldCost) || newCost < oldCost){
                        cost[next] = newCost;
                        int priority = newCost + GetDistanseToGoal(next, goal);
                        queue.Enqueue(next, priority);
                        came_from[next] = current;
                    }
                }
            }
            if (finished){
                (int, int) pathPoint = goal;
                path.Add(pathPoint);
                while (pathPoint != start){
                    pathPoint = came_from[pathPoint] ?? start;
                    path.Add(pathPoint);
                }
            }

            return path;
        }
        public override ActionInfo GetNextMove(Map map, Entity[] entities, Player player)
        {
            if ((this.X - player.X) * (this.X - player.X) + (this.Y - player.Y) * (this.Y - player.Y) <= RangeOfView * RangeOfView)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        return new ActionInfo(Action.Left, player, 1);

                    case ConsoleKey.RightArrow:
                        return new ActionInfo(Action.Right, player, 1);

                    case ConsoleKey.UpArrow:
                        return new ActionInfo(Action.Up, player, 1);

                    case ConsoleKey.DownArrow:
                        return new ActionInfo(Action.Down, player, 1);

                    case ConsoleKey.Q:
                        return new ActionInfo(Action.Quit, player, 1);

                }
            }

            throw new KeyNotFoundException("not yet implemented");
        }
    }
}
