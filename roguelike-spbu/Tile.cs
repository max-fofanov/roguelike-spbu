using System;
using System.Drawing;

namespace roguelike_spbu
{
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
        
        private string _symbol = "";
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value.Length > 0 ? value[0].ToString() : " ";
            }
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

        public VisualStatus Status
        {
            get;
            set;
        }
        public bool Impassable 
        {
            get;
            set;
        }

        public int Weight 
        {
            get;
            set;
        }

        public string Inode
        {
            get {
                return X.ToString() + "/" + Y.ToString();
            }
        }

        public Tile From {
            get;
            set;
        }

        public Tile Where {
            get;
            set;
        }

        public int Path {
            get;
            set;
        }
        public Tile(/*int x = 0, int y = 0, */string symbol = " ", Color? PFC = null, Color? PBC = null, VisualStatus status = VisualStatus.isVisible, bool impassable = false)
        {
            // X = x;
            // Y = y;
            Symbol = symbol;
            PrimaryForegroundColor = PFC ?? Color.White;
            PrimaryBackgroundColor = PBC ?? Color.Black;
            Status = status;
            Impassable = impassable;
        }

        public Tile(int x, int y) {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Tile) {
                return this.Inode.Equals((obj as Tile).Inode);
            }
            else
            {
                return false;
            }
            
        }
    }
}
