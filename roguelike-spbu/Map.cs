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

        public int Num {
            get;
            set;
        }
        public Map(int height, int width, int num)
        {
            Height = height;
            Width = width;
            Num = num;
            Tiles = new Tile[Height][];

            for (int i = 0; i < Height; i++)
            {
                Tiles[i] = new Tile[width];

                for (int j = 0; j < width; j++)
                {
                    Tiles[i][j] = new Void(i, j);
                }
            }
        }

    }
}