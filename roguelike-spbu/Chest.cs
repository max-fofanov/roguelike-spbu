using System;
using System.Drawing;

namespace roguelike_spbu
{
    public class Chest : Entity
    {
        public Chest(int x, int y) : base(x, y)
        {
            PrimaryForegroundColor = Color.White;
            Symbol = "⊞";
        }
    }
}