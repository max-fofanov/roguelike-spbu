using System;
using System.Collections.Generic;
using System.Drawing;

namespace roguelike_spbu
{
    [Serializable]
    class Enemy : Entity
    {
        public Enemy(int x, int y) : base(x, y)
        {
            this.RangeOfView = 10;
            this.Symbol = "%";
            this.PrimaryForegroundColor = Color.Yellow;
            this.Name = "Scam";
            //this.Stamina = 1;
            this.ForceType = "";
            this.Damage = 1;
            this.SetHealth(1);
            this.CreatureType = "";
            this.RangeOfHit = 0;
            this.XP = 1;
            this.Description = "somethig";
        }

        public override ActionInfo GetNextMove(Map map, List<Entity> entities, Player player)
        {
            if (Math.Pow(this.X - player.X, 2) + Math.Pow(this.Y - player.Y, 2) <= Math.Pow(this.RangeOfHit, 2)) { return new ActionInfo(Action.Attack, player.ID); }


            if ((this.X - player.X) * (this.X - player.X) + (this.Y - player.Y) * (this.Y - player.Y) <= RangeOfView * RangeOfView)
            {
                List<(int, int)> path = Primitives.AStarSearch(map, entities, player, (X, Y), (player.X, player.Y));

                if (path.Count > 1)
                {
                    (int x, int y) = path[1];
                    if (x > X)
                        return new ActionInfo(Action.Down);
                    if (x < X)
                        return new ActionInfo(Action.Up);
                    if (y > Y)
                        return new ActionInfo(Action.Right);
                    if (y < Y)
                        return new ActionInfo(Action.Left);
                }

            }

            return new ActionInfo(Action.StayInPlace);

        }
    }

}
