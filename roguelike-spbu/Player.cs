using System;

namespace roguelike_spbu
{
    class Player : IMovable
    {
        private int x;
        private int y;
        private Trait trait;

        public int X
        {
            get { return this.x;  }
            set { this.x = value;  }
        }

        public int Y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }

        public Player(int x = 0, int y = 0, Trait trait = Trait.Saber)
        {
            this.x = x;
            this.y = y;
            this.trait = trait;
        }

        
    }
}
