using System;
using System.Drawing;
using System.Text;
using Pastel;

namespace roguelike_spbu
{
    public class Renderer
    {
        static int PrevX;
        static int PrevY;
        static int Height;
        static int Width;
        static int InnerX;
        static int InnerY;
        static int InnerHeight;
        static int InnerWidth;
        public Renderer(int height, int width)
        {
            Height = height;
            Width = width;
        }
        public Renderer(int height, int width, int ih, int iw)
        {
            Height = height;
            Width = width;
            InnerHeight = ih;
            InnerWidth = iw;
            AutomaticallySetInnerBoxPosition();
        }
        public Renderer(int height, int width, int ih, int iw, int ix, int iy)
        {
            Height = height;
            Width = width;
            InnerHeight = ih;
            InnerWidth = iw;
            InnerX = ix;
            InnerY = iy;
        }
        static void SetLastRenderCoordinates(int x, int y){
            PrevX = x;
            PrevY = y;
        }
        static void AutomaticallySetInnerBoxPosition()
        {
            InnerX = PrevX + ((Height - InnerHeight) / 2);
            InnerY = PrevY + ((Width - InnerWidth) / 2);
        }
        static bool IsInsideBorders(int posx, int posy, int x, int y, int height, int width)
        {
            return (posx >= x && posy >= y && posx < (x + height) && posy < (y + width));
        }
        public static StringBuilder Render(Map map, Entity[] entities, Player player, int x, int y) // camera is fixed on coordinates
        {
            SetLastRenderCoordinates(x, y);

            string[,] buffer = new string[Height, Width];

            if (Height <= 0 || Width <= 0)
                throw new ArgumentOutOfRangeException("Invalid arguments");

            for (int i = 0; i < Height; i++) // fill buffer with Map
            {
                for (int j = 0; j < Width; j++)
                {
                    if ((x + i) >= map.Height || (y + j) >= map.Width ||
                        (x + i) < 0 || (y + j) < 0)
                        buffer[i, j] = " ".Pastel(Color.White).PastelBg(Color.Black);
                    else
                    {
                        Tile tmp = map.Tiles[x + i][y + j];
                        buffer[i, j] = tmp.Symbol.
                            Pastel(tmp.PrimaryForegroundColor).
                            PastelBg(tmp.PrimaryBackgroundColor);
                    }
                }
            }

            foreach (Entity entity in entities) // place entities on buffer
            {
                if (entity != null && IsInsideBorders(entity.X, entity.Y, x, y, Height, Width))
                {
                    buffer[entity.X - x, entity.Y - y] = entity.Symbol
                        .Pastel(entity.PrimaryForegroundColor)
                        .PastelBg(entity.PrimaryBackgroundColor ?? map.Tiles[entity.X][entity.Y].PrimaryBackgroundColor);
                }
            }

            if (IsInsideBorders(player.X, player.Y, x, y, Height, Width)) // place player on buffer
            {
                buffer[player.X - x, player.Y - y] = player.Symbol
                    .Pastel(player.PrimaryForegroundColor)
                    .PastelBg(player.PrimaryBackgroundColor ?? map.Tiles[player.X][player.Y].PrimaryBackgroundColor);
            }

            StringBuilder screenBuffer = new StringBuilder();

            for (int i = 0; i < Height; i++) // fill screen buffer 
            {
                for (int j = 0; j < Width; j++)
                {
                    screenBuffer.Append(buffer[i, j]);
                }
                screenBuffer.AppendLine();
            }

            return screenBuffer;
        }

        public StringBuilder Render() // camera is fixed on player
        {
            return new StringBuilder();
        }
    }
}