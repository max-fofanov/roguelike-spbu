using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Enemy : Creature
    {

        public Enemy(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Symbol = "%";
            this.Color = Color.Yellow;
        }

    }
}
