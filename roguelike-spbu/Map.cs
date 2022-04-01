using System;

namespace roguelike_spbu
{
    
    [Serializable]
    public class Plane {
        public Map? map;
        public List<Entity>? entities;

        public Plane() {}

        public Plane(Map map, List<Entity> entities) {
            this.map = map;
            this.entities = entities;
        }


    }
    
    
    [Serializable]
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

        public Map? MiniMap {
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

        public static (int, int) GetMiniCoordinates() {
            
            int num = GameInfo.player.X % (GameInfo.mapHeight / 20) * (GameInfo.mapWidth / 30) + GameInfo.player.Y % (GameInfo.mapWidth / 30);

            return (2 + num / (GameInfo.mapWidth / 30) * 4 , 2 + num % (GameInfo.mapWidth / 30) * 4);
        }

    }
}