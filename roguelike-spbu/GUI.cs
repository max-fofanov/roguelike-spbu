using System;
using System.IO;
using System.Text;

namespace roguelike_spbu {
    public static class GUIBorder
    {
        public static string verticalBorder = "║";
        public static string horizontalBorder = "═";
        public static string downrightBorder = "╔";
        public static string uprightBorder = "╚";
        public static string downleftBorder = "╗";
        public static string upleftBorder = "╝";
        public static string verticalrightBorder = "╠";
        public static string verticalleftBorder = "╣";
        public static string horizontalupBorder = "╩";
        public static string horizontaldownBorder = "╦";
        public static string crossBorder = "╬";
    }

    public class GUI
    {
        public bool error = false;
        public string errorMessage = "";
        public string[,] layoutMatrix;
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
        public List<Window> windows = new List<Window>();
        public GUI(List<Window> windows)
        {
            this.windows = windows;
            if (CheckIntersection(out (int, int) overlappingWindows))
            {
                error = true;
                errorMessage = $"There are overlapping windows: ({overlappingWindows.Item1}, {overlappingWindows.Item2})";
            }
            else
            {
                CalculateSize();
                layoutMatrix = new string[Height, Width];
                CreateLayout();
            }
        }
        public async void Print()
        {
            StringBuilder screenBuffer = new StringBuilder();

            for (int i = 0; i < Height; i++) // fill screen buffer 
            {
                for (int j = 0; j < Width; j++)
                {
                    screenBuffer.Append(layoutMatrix[i, j]);
                }
                screenBuffer.AppendLine();
            }

            Console.WriteLine(screenBuffer);
        }
        public bool IsThereBorder((int, int) borderPoint)
        {
            int x = borderPoint.Item1;
            int y = borderPoint.Item2;
            if (x < 0 || y < 0 || x >= Height || y >= Width)
                return false;

            if (layoutMatrix[x, y] == GUIBorder.verticalBorder || layoutMatrix[x, y] == GUIBorder.horizontalBorder)
                return true;

            return false;
        }
        public string GetCornerBorder((int, int) cornerPoint)
        {
            int x = cornerPoint.Item1;
            int y = cornerPoint.Item2;
            bool up = IsThereBorder((x - 1, y));
            bool down = IsThereBorder((x + 1, y));
            bool right = IsThereBorder((x, y + 1));
            bool left = IsThereBorder((x, y - 1));

            if (up)
            {
                if (down)
                {
                    if (right && left)
                        return GUIBorder.crossBorder;
                    if (right)
                        return GUIBorder.verticalrightBorder;
                    if (left)
                        return GUIBorder.verticalleftBorder;
                }
                else
                {
                    if (right && left)
                        return GUIBorder.horizontalupBorder;
                    if (right)
                        return GUIBorder.uprightBorder;
                    if (left)
                        return GUIBorder.upleftBorder;
                }
            }
            else
            {
                if (right && left)
                    return GUIBorder.horizontaldownBorder;
                if (right)
                    return GUIBorder.downrightBorder;
                if (left)
                    return GUIBorder.downleftBorder;
            }

            return "?";
        }
        public async void CreateLayout()
        {
            List<(int, int)> cornerPoints = new List<(int, int)>();

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    layoutMatrix[i, j] = " ";

            foreach (Window window in windows)
            {
                int x = window.X;
                int y = window.Y;
                int height = window.Height;
                int width = window.Width;
                cornerPoints.Add((x, y));
                cornerPoints.Add((x + height - 1, y));
                cornerPoints.Add((x, y + width - 1));
                cornerPoints.Add((x + height - 1, y + width - 1));
                for (int i = x + 1; i < x + height - 1; i++)
                {
                    layoutMatrix[i, y] = GUIBorder.verticalBorder;
                    layoutMatrix[i, y + width - 1] = GUIBorder.verticalBorder;
                }
                for (int i = y + 1; i < y + width - 1; i++)
                {
                    layoutMatrix[x, i] = GUIBorder.horizontalBorder;
                    layoutMatrix[x + height - 1, i] = GUIBorder.horizontalBorder;
                }
            }

            foreach ((int, int) cornerPoint in cornerPoints)
            {
                int x = cornerPoint.Item1;
                int y = cornerPoint.Item2;
                //Console.WriteLine("{0} {1}", x, y);

                layoutMatrix[x, y] = GetCornerBorder(cornerPoint);
            }
        }
        public void CalculateSize()
        {
            Height = windows.Max(w => (w.X + w.Height));
            Width = windows.Max(w => (w.Y + w.Width));
        }
        public bool CheckIntersection(out (int, int) overlappingWindows)
        {
            overlappingWindows = (-1, -1);
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].FullSreen) continue;
                for (int j = i + 1; j < windows.Count; j++)
                {
                    if (windows[j].FullSreen) continue;
                    int minX;
                    int minHeight;
                    int maxX;

                    if (windows[i].X < windows[j].X)
                    {
                        minX = windows[i].X;
                        minHeight = windows[i].Height;
                        maxX = windows[j].X;
                    }
                    else
                    {
                        minX = windows[j].X;
                        minHeight = windows[j].Height;
                        maxX = windows[i].X;
                    }

                    bool Xintersection = (minX + minHeight - 1) > maxX;

                    int minY;
                    int minWidth;
                    int maxY;

                    if (windows[i].Y < windows[j].Y)
                    {
                        minY = windows[i].Y;
                        minWidth = windows[i].Width;
                        maxY = windows[j].Y;
                    }
                    else
                    {
                        minY = windows[j].Y;
                        minWidth = windows[j].Width;
                        maxY = windows[i].Y;
                    }

                    bool Yintersection = (minY + minWidth - 1) > maxY;
                    overlappingWindows = (i, j);
                    if (Xintersection && Yintersection) return true;
                }
            }

            return false;
        }
    }
    public class Window
    {
        public bool FullSreen = false;
        public bool Transparent = false;
        public bool Static = false;
        public int X;
        public int Y;
        public int Height;
        public int Width;

        public Window()
        {
            X = 0;
            Y = 0;
            Height = 0;
            Width = 0;
        }

        public Window(int x, int y, int h, int w)
        {
            X = x;
            Y = y;
            Height = h;
            Width = w;
        }
    }
}