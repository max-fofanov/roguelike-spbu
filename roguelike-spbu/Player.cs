using System;

namespace roguelike_spbu
{
    class Player : Creature
    {
        private int x;
        private int y;
        private Trait trait;
        private char symbol = '@';
        private ConsoleColor color = ConsoleColor.Red;

        public int X {
            get;
            set;
        }

        public int Y {
            get;
            set;
        }
        
        public Player(int x, int y, Trait trait = Trait.Saber)
        {
            X = x;
            Y = y;
            this.trait = trait;

            Symbol = symbol;
            Color = color;
        }


        
    }
}
