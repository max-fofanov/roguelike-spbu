using System;

namespace roguelike_spbu
{
    public class Map
    {
        public int Height
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }

        public Tile[][] Tiles
        {
            get;
            set;
        }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;

            Tiles = new Tile[Height][];

            for (int i = 0; i < Height; i++)
            {

                Tiles[i] = new Tile[width];

                for (int j = 0; j < width; j++)
                {
                    Tile genericTile = new Border();
                    genericTile.X = i;
                    genericTile.Y = j;
                    Tiles[i][j] = genericTile;
                }
            }
        }

    }
}