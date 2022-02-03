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
            this.x = x;
            this.y = y;
            this.trait = trait;
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;

        }



    }
}
