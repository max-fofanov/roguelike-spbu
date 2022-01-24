using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike_spbu
{
    class Creature
    {
        private int x;
        private int y;
        private int health;
        private char symbol;
        private ConsoleColor color;

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

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public char Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

    }
}
