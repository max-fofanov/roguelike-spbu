using System;

namespace roguelike_spbu
{
    public enum TileStatus {
        isHidden, 
        isVisible,
        wasSeen
    }
    public class Tile
    {
        
        public int X {
            get;
            set;
        }
        public int Y {
            get;
            set;
        }
        public TileType Type {
            get;
            set;
        }

        public TileStatus Status 
        {
            get;
            set;
        }

        public Tile(int x, int y, TileType type,  TileStatus status)
        {
            X = x;
            Y = y;
            Type = type;
            Status = status;
        }

        
    }
}
