using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Void : Tile
    {
        public Void(){
            Symbol = " ";
            Impassable = false;
            PrimaryForegroundColor = Color.Black;
            PrimaryBackgroundColor = Color.Black;
        }
    }
    class Border : Tile
    {
        public Border()
        {
            Symbol = "#";
            Impassable = true;
            PrimaryForegroundColor = Color.DimGray;
            PrimaryBackgroundColor = Color.Gray;
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
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
        }

    }

    public class Water : Tile
    {
        public Water()
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Blue;
        }

    }
}
