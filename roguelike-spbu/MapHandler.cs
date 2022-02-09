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
                    //Random todetele = new Random();

                    Tile genericTile = new Border(i.ToString() + "/" + j.ToString());

                    /*if (todetele.Next(2) == 0)
                        genericTile = new Field();
                    else
                        genericTile = new Water();*/
                    Tiles[i][j] = genericTile;
                }
            }
        }

    }
}