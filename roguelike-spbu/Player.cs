using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Player : Creature
    {
        private int x;
        private int y;
        private Trait trait;
<<<<<<< HEAD
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
        
=======
        private string symbol = "@";
        private Color color = Color.Red;

>>>>>>> 780df2adc037017609b9ffa6db56e16d9cb2f2b6
        public Player(int x, int y, Trait trait = Trait.Saber)
        {
            X = x;
            Y = y;
            this.trait = trait;

            Symbol = symbol;
            Color = color;
<<<<<<< HEAD
=======

>>>>>>> 780df2adc037017609b9ffa6db56e16d9cb2f2b6
        }



    }
}
