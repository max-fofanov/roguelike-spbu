﻿using System;
using System.Drawing;

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

        public TileStatus Status 
        {
            get;
            set;
        }

        public Tile(int x = 0, int y = 0, string symbol = " ", Color? PFC = null, Color? PBC = null, TileStatus status = TileStatus.isVisible)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            PrimaryForegroundColor = PFC ?? Color.White;
            PrimaryBackgroundColor = PBC ?? Color.Black;
            Status = status;
        }

        
    }
}
