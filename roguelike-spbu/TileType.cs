using System;
using System.Drawing;

namespace roguelike_spbu
{
    [Serializable]
    class Void : Tile
    {
        public Void(int x, int y) : base(x, y) {
            Symbol = " ";
            Impassable = false;
            PrimaryForegroundColor = Color.Black;
            PrimaryBackgroundColor = Color.Red;
        }
    }
    [Serializable]
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
    [Serializable]
    public class Rock : Tile
    {
        public Rock(int x, int y) : base(x, y)
        {
            Symbol = "R";
            PrimaryForegroundColor = Color.Yellow;
        }

    }

    [Serializable]
    public class Field : Tile
    {
        public Field(int x, int y) : base(x, y)
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
        }

    }

    [Serializable]
    public class Exit : Tile
    {
        public int Room {
            get;
            set;
        }
        public Exit(int x, int y, int num) : base(x, y)
        {
            
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
            Room = num;
            Symbol = Room.ToString(); // "/";
        }

    }

    [Serializable]
    public class Door : Tile
    {
        public Door(int x, int y) : base(x, y)
        {
            
            Symbol = "/";
            PrimaryForegroundColor = Color.DarkGreen;
            PrimaryBackgroundColor = Color.Green;
            Impassable = true;
            
        }

    }

    [Serializable]
    public class Water : Tile
    {
        public Water(int x, int y) : base(x, y)
        {
            Symbol = "W";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Blue;
        }

    }
    [Serializable]
    public class DungeonEntry : Tile
    {
        public DungeonEntry(int x, int y) : base(x, y)
        {
            Symbol = "D";
            PrimaryForegroundColor = Color.Red;
            PrimaryBackgroundColor = Color.Black;
        }

    }
    [Serializable]
    public class House : Tile
    {
        public House(int x, int y) : base(x, y)
        {
            Symbol = "⌂";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
     [Serializable]
    public class Alchemy : Tile
    {
        public Alchemy(int x, int y) : base(x, y)
        {
            Symbol = "☺";
            PrimaryForegroundColor = Color.Blue;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
    [Serializable]
    public class TownHall : Tile
    {
        public TownHall(int x, int y) : base(x, y)
        {
            Symbol = "♦";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
    [Serializable]
    public class Shop : Tile
    {
        public Shop(int x, int y) : base(x, y)
        {
            Symbol = "☻";
            PrimaryForegroundColor = Color.Green;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
    [Serializable]
    public class Road : Tile
    {
        public Road(int x, int y) : base(x, y)
        {
            Symbol = "░";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Black;
        }

    }
    [Serializable]
    public class HubField : Tile
    {
        public HubField(int x, int y) : base(x, y)
        {
            Symbol = ".";
            PrimaryForegroundColor = Color.Gray;
            PrimaryBackgroundColor = Color.Black;
        }

    }
    [Serializable]
    public class Mountain : Tile
    {
        public Mountain(int x, int y) : base(x, y)
        {
            Symbol = "▲";
            PrimaryForegroundColor = Color.WhiteSmoke;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
    [Serializable]
    public class Tree : Tile
    {
        public Tree(int x, int y) : base(x, y)
        {
            Symbol = "♣";
            PrimaryForegroundColor = Color.WhiteSmoke;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
    
    [Serializable]
    public class Shrine : Tile
    {
        public Shrine(int x, int y) : base(x, y)
        {
            Symbol = "♥";
            PrimaryForegroundColor = Color.WhiteSmoke;
            PrimaryBackgroundColor = Color.Black;
            Impassable = true;
        }

    }
}
