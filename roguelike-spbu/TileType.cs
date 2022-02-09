using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Border : Tile
    {
        public Border(string inode) : base(inode)
        {
            Symbol = "#";
            Impassable = true;
            PrimaryForegroundColor = Color.DimGray;
            PrimaryBackgroundColor = Color.Gray;
        }
    }
    public class Tree : Tile
    {
        public Tree(string inode) : base(inode)
        {
            Symbol = "T";
            PrimaryForegroundColor = Color.Green;
        }

    }

    public class Rock : Tile
    {
        public Rock(string inode) : base(inode)
        {
            Symbol = "R";
            PrimaryForegroundColor = Color.Yellow;
        }

    }

    public class Field : Tile
    {
        public Field(string inode) : base(inode)
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
        }

    }

    public class Water : Tile
    {
        public Water(string inode) : base(inode)
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Blue;
        }

    }
}
