using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Player : Creature
    {
        private int x;
        private int y;
        private Trait trait;
        private string symbol = "@";
        private Color color = Color.Red;

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
