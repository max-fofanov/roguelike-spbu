using System;
using System.Drawing;
using System.Text;
using Pastel;

namespace roguelike_spbu
{
    public class Renderer
    {
        static string[,] buffer = {{}};
        static bool GuiIsSet = false;
        static int? PrevX;
        static int? PrevY;
        static int Height;
        static int Width;
        static int InnerX;
        static int InnerY;
        static int InnerHeight;
        static int InnerWidth;
        static bool StatinInnerBox = false;
        public static Guid SelectedEntity = Guid.Empty;
        public Renderer(int height, int width)
        {
            Height = height;
            Width = width;
            buffer = new string[Height, Width];
        }
        public Renderer(int height, int width, int ih, int iw) : this(height, width)
        {
            InnerHeight = ih;
            InnerWidth = iw;
            StatinInnerBox = false;
            AutomaticallySetInnerBoxPosition();
        }
        public Renderer(int height, int width, int ih, int iw, int ix, int iy) : this(height, width, ih, iw)
        {
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
        static string GetAppropriateSymbol(VisualStatus status, Tile tile, Entity? entity = null, bool allVisible = false)
        {
            if (allVisible) status = VisualStatus.isVisible;

            Color entityBGColor = tile.PrimaryBackgroundColor;
            if (entity != null)
                if (entity.ID == SelectedEntity)
                    entityBGColor = Color.FromArgb(191, 0, 255);
                else
                    entityBGColor = entity.PrimaryBackgroundColor ?? tile.PrimaryBackgroundColor;


            switch (status)
            {
                case VisualStatus.isVisible:
                    return entity != null ?
                        entity.Symbol.
                        Pastel(entity.PrimaryForegroundColor).
                        PastelBg(entityBGColor)
                        :
                        tile.Symbol.
                        Pastel(tile.PrimaryForegroundColor).
                        PastelBg(tile.PrimaryBackgroundColor);
                case VisualStatus.wasSeen:
                    return tile.Symbol.
                        Pastel(ChangeColorBrightness(tile.PrimaryForegroundColor, 0.5)).
                        PastelBg(ChangeColorBrightness(tile.PrimaryBackgroundColor, 0.5));
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
        public static string[,] Render(Map map, List<Entity> entities, Player player, int x, int y, bool allVisible) // camera is fixed on coordinates
        {

            SetLastRenderCoordinates(x, y);

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
                        buffer[i, j] = GetAppropriateSymbol(tmp.Status, tmp, null, allVisible);
                    }
                }
            }

            foreach (Entity entity in entities) // place entities on buffer
            {
                if (entity != null && IsInsideBorders(entity.X, entity.Y, x, y, Height, Width))
                {
                    buffer[entity.X - x, entity.Y - y] = GetAppropriateSymbol(map.Tiles[entity.X][entity.Y].Status, map.Tiles[entity.X][entity.Y], entity, allVisible);
                }
            }

            if (IsInsideBorders(player.X, player.Y, x, y, Height, Width)) // place player on buffer
            {
                buffer[player.X - x, player.Y - y] = GetAppropriateSymbol(player.VStatus, map.Tiles[player.X][player.Y], player, allVisible);
            }

            return buffer;

            /*StringBuilder screenBuffer = new StringBuilder();


            for (int i = 0; i < (GuiIsSet ? Gui.Height : Height); i++) // fill screen buffer 
            {
                for (int j = 0; j < (GuiIsSet ? Gui.Width : Width); j++)
                {
                    screenBuffer.Append(buffer[i, j]);
                }
                screenBuffer.AppendLine();
            }

            return screenBuffer;*/
        }
        public static string[,] Render(Map map, List<Entity> entities, Player player, bool allVisible) // camera is fixed on player
        {
            int newX = PrevX ?? 0;
            int newY = PrevY ?? 0;

            if (PrevX == null || PrevY == null) // first frame
            {
                newX = player.X - Height / 2;
                newY = player.Y - Width / 2;
            }

            int InnerAbsoluteX = InnerX + newX;
            int InnerAbsoluteY = InnerY + newY;

            if (!IsInsideBorders(player.X, player.Y, InnerAbsoluteX, InnerAbsoluteY, InnerHeight, InnerWidth))
            {
                if (player.X <= InnerAbsoluteX)
                {
                    newX -= InnerAbsoluteX - player.X;// + 1;
                }
                if (player.Y <= InnerAbsoluteY)
                {
                    newY -= InnerAbsoluteY - player.Y;// + 1;
                }
                if (player.X >= (InnerAbsoluteX + InnerHeight))
                {
                    newX += player.X - (InnerAbsoluteX + InnerHeight) + 1;
                }
                if (player.Y >= (InnerAbsoluteY + InnerWidth))
                {
                    newY += player.Y - (InnerAbsoluteY + InnerWidth) + 1;
                }
            }

            // correcting screen if it is off map
            if ((newX + Height) >= map.Height)
            {
                newX = map.Height - Height; // bottom is off map
            }
            if ((newY + Width) >= map.Width)
            {
                newY = map.Width - Width; // right is off map
            }
            if (newX < 0)
            {
                newX = 0; // top is off map
            }
            if (newY < 0)
            {
                newY = 0; // left is off map
            }
            
            return Render(map, entities, player, newX, newY, allVisible);
        }
    }
}