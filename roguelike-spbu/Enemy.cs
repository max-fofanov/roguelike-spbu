using System;
using System.Drawing;
using System.Collections.Generic;

namespace roguelike_spbu
{
    class Enemy : Entity
    {
        public Enemy(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.RangeOfView = 10;
            this.Symbol = "%";
            this.PrimaryForegroundColor = Color.Yellow;
            this.Name = "Scam";
            this.Stamina = 1;
            this.ForceType = "";
            this.Damage = 1;
            this.HealthPoints = 1;
            this.CreatureType ="";
            this.RangeOfHit = 0;
        }

        public override ActionInfo GetNextMove(Map map, List<Entity> entities, Player player)
        {
            if ((this.X - player.X) * (this.X - player.X) + (this.Y - player.Y) * (this.Y - player.Y) <= RangeOfView * RangeOfView)
            {
                List<(int, int)> path = Primitives.AStarSearch(map, entities, player, (X, Y), (player.X, player.Y));

                if (path.Count > 1)
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
                
                return new ActionInfo(Action.StayInPlace, player, 1);
            }

            return new ActionInfo(Action.StayInPlace, player, 1);

            throw new KeyNotFoundException("not yet implemented");
        }
    }
}
