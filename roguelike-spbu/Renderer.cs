using System;
using System.Drawing;
using System.Text;
using Pastel;

namespace roguelike_spbu
{
    public class Renderer
    {
        static int? PrevX;
        static int? PrevY;
        static int Height;
        static int Width;
        static int InnerX;
        static int InnerY;
        static int InnerHeight;
        static int InnerWidth;
        static bool StatinInnerBox = false;
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
            StatinInnerBox = false;
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
            StatinInnerBox = true;
        }
        static Color ChangeColorBrightness(Color color, double factor)
        {
            if (factor < 0)
                return Color.Black;

            int Red = (int)(color.R * factor);
            int Green = (int)(color.G * factor);
            int Blue = (int)(color.B * factor);

            if (Red > 255) Red = 255;
            if (Green > 255) Green = 255;
            if (Blue > 255) Blue = 255;

            return Color.FromArgb(Red, Green, Blue);
        }
        static string GetAppropriateSymbol(VisualStatus status, string symbol, Color PFC, Color? PBC, Color? MBC = null)
        {
            switch (status)
            {
                case VisualStatus.isVisible:
                    return symbol.
                        Pastel(PFC).
                        PastelBg(PBC ?? (MBC ?? Color.Black));
                case VisualStatus.wasSeen:
                    return symbol.
                        Pastel(ChangeColorBrightness(PFC, 0.5)).
                        PastelBg(ChangeColorBrightness(PBC ?? (MBC ?? Color.Black), 0.5));
                case VisualStatus.isHidden:
                    return " ".
                        Pastel(Color.Black).
                        PastelBg(Color.Black);
            }

            throw new NotImplementedException("Strange Visual Status");
        }
        static void SetLastRenderCoordinates(int x, int y){
            PrevX = x;
            PrevY = y;
        }
        static void AutomaticallySetInnerBoxPosition()
        {
            InnerX = ((Height - InnerHeight) / 2);
            InnerY = ((Width - InnerWidth) / 2);
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
                        buffer[i, j] = " ".Pastel(Color.Black).PastelBg(Color.Black);
                    else
                    {
                        Tile tmp = map.Tiles[x + i][y + j];
                        buffer[i, j] = GetAppropriateSymbol(tmp.Status, tmp.Symbol, tmp.PrimaryForegroundColor, tmp.PrimaryBackgroundColor);
                    }
                }
            }

            foreach (Entity entity in entities) // place entities on buffer
            {
                if (entity != null && IsInsideBorders(entity.X, entity.Y, x, y, Height, Width))
                {
                    buffer[entity.X - x, entity.Y - y] = GetAppropriateSymbol(entity.VStatus, entity.Symbol,
                        entity.PrimaryForegroundColor,
                        entity.PrimaryBackgroundColor,
                        map.Tiles[entity.X][entity.Y].PrimaryBackgroundColor);
                }
            }

            if (IsInsideBorders(player.X, player.Y, x, y, Height, Width)) // place player on buffer
            {
                buffer[player.X - x, player.Y - y] = GetAppropriateSymbol(player.VStatus, player.Symbol,
                    player.PrimaryForegroundColor,
                    player.PrimaryBackgroundColor,
                    map.Tiles[player.X][player.Y].PrimaryBackgroundColor);
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
        public static StringBuilder Render(Map map, Entity[] entities, Player player) // camera is fixed on player
        {
            // Console.WriteLine("MAP {0} {1}                                                  ", map.Height, map.Width);
            int newX = PrevX ?? 0;
            int newY = PrevY ?? 0;
            // Console.WriteLine("SCREEN1 {0} {1} {2} {3}                                                  ", newX, newY, newX + Height, newY + Width);

            if (PrevX == null || PrevY == null) // first frame
            {
                newX = player.X - Height / 2;
                newY = player.Y - Width / 2;
                // Console.WriteLine("SCREEN2 {0} {1} {2} {3}                                                  ", newX, newY, newX + Height, newY + Width);
            }

            int InnerAbsoluteX = InnerX + newX;
            int InnerAbsoluteY = InnerY + newY;
            // Console.WriteLine("INNERBOX {0} {1} {2} {3} {4} {5}                                                  ", InnerX, InnerY, InnerAbsoluteX, InnerAbsoluteY, InnerHeight, InnerWidth);

            if (!IsInsideBorders(player.X, player.Y, InnerAbsoluteX, InnerAbsoluteY, InnerHeight, InnerWidth))
            {
                if (player.X <= InnerAbsoluteX)
                {
                    newX -= InnerAbsoluteX - player.X;// + 1;
                    // Console.WriteLine("player above box                                                  ");
                }
                if (player.Y <= InnerAbsoluteY)
                {
                    newY -= InnerAbsoluteY - player.Y;// + 1;
                    // Console.WriteLine("player to the left of the box                                                  ");
                }
                if (player.X >= (InnerAbsoluteX + InnerHeight))
                {
                    newX += player.X - (InnerAbsoluteX + InnerHeight) + 1;
                    // Console.WriteLine("player below box                                                  ");
                }
                if (player.Y >= (InnerAbsoluteY + InnerWidth))
                {
                    newY += player.Y - (InnerAbsoluteY + InnerWidth) + 1;
                    // Console.WriteLine("player to the right of the box                                                  ");
                }
                // Console.WriteLine("SCREEN3 {0} {1} {2} {3}                                                  ", newX, newY, newX + Height, newY + Width);
            }

            // correcting screen if it is off map
            if ((newX + Height) >= map.Height)
            {
                newX = map.Height - Height; // bottom is off map
                // Console.WriteLine("bottom is off map                                                  ");
            }
            if ((newY + Width) >= map.Width)
            {
                newY = map.Width - Width; // right is off map
                // Console.WriteLine("right is off map                                                  ");
            }
            if (newX < 0)
            {
                newX = 0; // top is off map
                // Console.WriteLine("top is off map                                                  ");
            }
            if (newY < 0)
            {
                newY = 0; // left is off map
                // Console.WriteLine("left is off map                                                  ");
            }

            // Console.WriteLine("SCREEN4 {0} {1} {2} {3}                                                  ", newX, newY, newX + Height, newY + Width);
            // Console.WriteLine("PLAYER {0} {1}                                                  ", player.X - newX, player.Y - newY);
            // Console.SetCursorPosition(0, 11);
            return Render(map, entities, player, newX, newY);
        }
    }
}