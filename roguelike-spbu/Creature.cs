using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Creature
    {
        private int x;
        private int y;
        private int health;
        private string symbol;
        private Color color;

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public string Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

    }
}
