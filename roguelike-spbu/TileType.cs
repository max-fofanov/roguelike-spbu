using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Border : Tile
    {
        public Border()
        {
            Symbol = "#";
            PrimaryForegroundColor = Color.DimGray;
        }
    }
    public class Tree : Tile
    {
        public Tree()
        {
            Symbol = "T";
            PrimaryForegroundColor = Color.Green;
        }

    }

    public class Rock : Tile
    {
        public Rock()
        {
            Symbol = "R";
            PrimaryForegroundColor = Color.Yellow;
        }

    }

    public class Field : Tile
    {
        public Field()
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.Green;
            PrimaryBackgroundColor = Color.DarkGreen;
        }

    }

    public class Water : Tile
    {
        public Water()
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Blue;
        }

    }
}
