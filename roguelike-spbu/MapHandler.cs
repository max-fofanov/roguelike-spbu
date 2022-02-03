using System;

namespace roguelike_spbu 
{
    class Map
    {
        public int Height {
            get;
            set;
        }
        public int Width {
            get;
            set;
        }

        public Tile[][] Tiles {
            get;
            set;
        }

        public Map(int height, int width) {
            Height = height;
            Width = width;

            Tiles = new Tile[Height][];
            TileType genericType = new Field();

            for (int i = 0; i < Height; i++) {
                
                Tiles[i] = new Tile[width];

                for (int j = 0; j < width; j++) {
                    Tiles[i][j] = new Tile(i, j, genericType, TileStatus.isVisible);
                }
            }
        }

    }    
}