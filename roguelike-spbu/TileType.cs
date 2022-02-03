using System;
using System.Drawing;

namespace roguelike_spbu
{
    public class TileType
    {
        public string Symbol
        {
            get;
            set;
        }
        public Color PrimaryForegroundColor 
        {
            get;
            set;
        }
        public Color PrimaryBackgroundColor 
        {
            get;
            set;
        }

        /*
        public Color SecondaryForegroundColor 
        {
            get;
            set;
        }
        public Color SecondaryBackgroundColor 
        {
            get;
            set;
        }  
        */    
    }

    class Border : TileType
    {
        public Border()
        {
           Symbol = "#";
           PrimaryForegroundColor = Color.White;
        }
    }
    public class Tree : TileType
    {
        public Tree()
        {
            Symbol = "T";
            PrimaryForegroundColor = Color.Green;
        }

    }

    public class Rock : TileType
    {
        public Rock()
        {
            Symbol = "R";
            PrimaryForegroundColor = Color.Yellow;
        }

    }

    public class Field : TileType
    {
        public Field()
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.White;
        }

    }

    public class Water : TileType
    {
        public Water()
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Blue;
        }

    }
}
