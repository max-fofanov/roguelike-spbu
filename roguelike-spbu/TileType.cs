using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Void : Tile
    {
        public Void(int x, int y) : base(x, y) {
            Symbol = " ";
            Impassable = false;
            PrimaryForegroundColor = Color.Black;
            PrimaryBackgroundColor = Color.Red;
        }
    }
    class Border : Tile
    {
        public Border(int x, int y) : base(x, y)
        {
            Symbol = "#";
            Impassable = true;
            PrimaryForegroundColor = Color.DimGray;
            PrimaryBackgroundColor = Color.Gray;
            
        }
    }
    public class Tree : Tile
    {
        public Tree(int x, int y) : base(x, y)
        {
            Symbol = "T";
            PrimaryForegroundColor = Color.Green;
        }

    }

    public class Rock : Tile
    {
        public Rock(int x, int y) : base(x, y)
        {
            Symbol = "R";
            PrimaryForegroundColor = Color.Yellow;
        }

    }

    public class Field : Tile
    {
        public Field(int x, int y) : base(x, y)
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
        }

    }

    public class Exit : Tile
    {
        public int Room {
            get;
            set;
        }
        public Exit(int x, int y, int num) : base(x, y)
        {
            Symbol = "/";
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
            Room = num;
        }

    }

    public class Water : Tile
    {
        public Water(int x, int y) : base(x, y)
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Blue;
        }

    }
}
