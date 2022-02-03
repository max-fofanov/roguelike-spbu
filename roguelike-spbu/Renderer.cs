using System;
using System.Drawing;
using System.Text;
using Pastel;

namespace roguelike_spbu
{
    class Renderer
    {
        public Map map;

        public Player player;

        public Renderer(Map map, Player player)
        {
            this.map = map;
            this.player = player;
        }

        public void SetMap(Map map)
        {
            this.map = map;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public StringBuilder Render(int x, int y, int height, int width) // camera is fixed on coordinates
        {
            string[,] buffer = new string[height, width];

            if (height <= 0 || width <= 0)
                throw new ArgumentOutOfRangeException("Invalid arguments");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((x + i) >= map.Height || (y + j) >= map.Width ||
                        (x + i) < 0 || (y + j) < 0)
                        buffer[i, j] = " ".Pastel(Color.White).PastelBg(Color.Black);
                    else
                    {
                        Tile tmp = map.Tiles[x + i][y + j];
                        buffer[i, j] = tmp.Type.Symbol.
                            Pastel(tmp.Type.PrimaryForegroundColor).
                            PastelBg(tmp.Type.PrimaryBackgroundColor);
                    }
                }
            }

            buffer[player.X - x, player.Y - y] = player.Symbol.Pastel(player.Color);

            StringBuilder screenBuffer = new StringBuilder();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    screenBuffer.Append(buffer[i, j]);
                }
                screenBuffer.AppendLine();
            }

            return screenBuffer;
        }

        public StringBuilder Render(int height, int width) // camera is fixed on player
        {
            return new StringBuilder();
        }
    }
}