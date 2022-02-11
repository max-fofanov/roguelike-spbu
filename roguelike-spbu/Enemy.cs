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
        static int? WeightOfPlace(Map map, Entity[] entities, Player player, int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.Height || y >= map.Width)
                return null;

            if (map.Tiles[x][y].Impassable)
                return null;

            if (entities.Where(e => e.X == x && e.Y == y).Count() > 0)
                return 3;
            
            if (player.X == x && player.Y == y)
                return 1;

            return 1;
        }
        static List<(int, int, int)> GetNeighbors(Map map, Entity[] entities, Player player, (int, int) point)
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
                
                foreach ((int nx, int ny, int weight) in GetNeighbors(map, entities, player, current))
                {
                    (int, int) next = (nx, ny);
                    int newCost = cost[current] + weight;
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

                path.Reverse();
            }

            return path;
        }
        public override ActionInfo GetNextMove(Map map, Entity[] entities, Player player)
        {
            if ((this.X - player.X) * (this.X - player.X) + (this.Y - player.Y) * (this.Y - player.Y) <= RangeOfView * RangeOfView)
            {
                List<(int, int)> path = Enemy.AStarSearch(map, entities, player, (X, Y), (player.X, player.Y));

                if (path.Count > 0)
                {
                    (int x, int y) = path[1];
                    if (x > X)
                        return new ActionInfo(Action.Down, player, 1);
                    if (x < X)
                        return new ActionInfo(Action.Up, player, 1);
                    if (y > Y)
                        return new ActionInfo(Action.Right, player, 1);
                    if (y < Y)
                        return new ActionInfo(Action.Left, player, 1);
                }
                
                return new ActionInfo(Action.Pass, player, 1);
            }

            throw new KeyNotFoundException("not yet implemented");
        }
    }
}
