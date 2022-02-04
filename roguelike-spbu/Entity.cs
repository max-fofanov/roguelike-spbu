using System;
using System.Drawing;

namespace roguelike_spbu
{
    public class Entity
    {
        public int X
        {
            get;
            set;
        }

        public int Y
        {
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
        public Color? PrimaryBackgroundColor
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

        public TileStatus Status
        {
            get;
            set;
        }
    }
}
