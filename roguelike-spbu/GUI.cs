using System;
using System.IO;
using System.Text;

namespace roguelike_spbu {
    public static class GUIElements
    {
        public static string allBorders = "║═╔╚╗╝╠╣╩╦╬";
        public static string verticalBorder = allBorders[0].ToString(); //"║";
        public static string horizontalBorder = allBorders[1].ToString(); //"═";
        public static string downrightBorder = allBorders[2].ToString(); //"╔";
        public static string uprightBorder = allBorders[3].ToString(); //"╚";
        public static string downleftBorder = allBorders[4].ToString(); //"╗";
        public static string upleftBorder = allBorders[5].ToString(); //"╝";
        public static string verticalrightBorder = allBorders[6].ToString(); //"╠";
        public static string verticalleftBorder = allBorders[7].ToString(); //"╣";
        public static string horizontalupBorder = allBorders[8].ToString(); //"╩";
        public static string horizontaldownBorder = allBorders[9].ToString(); //"╦";
        public static string crossBorder = allBorders[10].ToString(); //"╬";

        public static bool IsBorder(string input)
        {
            if (input.Length != 1) return false;
            return allBorders.IndexOf(input) != -1;
        }

        public static string upArrow = "▲";
        public static string downArrow = "▼";
    }

    public class GUI
    {
        public bool error = false;
        public string errorMessage = "";
        public string[,] layoutMatrix = new string[0, 0];
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
        public void Print()
        {
            foreach (Window window in windows)
            {
                string[,] windowBuffer = window.GetInsides();
                for (int i = 0; i < window.Height - 2; i++) // fill screen buffer 
                {
                    for (int j = 0; j < window.Width - 2; j++)
                    {
                        layoutMatrix[i + window.X + 1, j + window.Y + 1] = windowBuffer[i, j];
                    }
                }
            }

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

            //if (layoutMatrix[x, y] == GUIElements.verticalBorder || layoutMatrix[x, y] == GUIElements.horizontalBorder)
            if (GUIElements.IsBorder(layoutMatrix[x, y]))
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
                        return GUIElements.crossBorder;
                    if (right)
                        return GUIElements.verticalrightBorder;
                    if (left)
                        return GUIElements.verticalleftBorder;
                }
                else
                {
                    if (right && left)
                        return GUIElements.horizontalupBorder;
                    if (right)
                        return GUIElements.uprightBorder;
                    if (left)
                        return GUIElements.upleftBorder;
                }
            }
            else
            {
                if (right && left)
                    return GUIElements.horizontaldownBorder;
                if (right)
                    return GUIElements.downrightBorder;
                if (left)
                    return GUIElements.downleftBorder;
            }

            return "?";
        }
        public void CreateLayout()
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
                    layoutMatrix[i, y] = GUIElements.verticalBorder;
                    layoutMatrix[i, y + width - 1] = GUIElements.verticalBorder;
                }
                for (int i = y + 1; i < y + width - 1; i++)
                {
                    layoutMatrix[x, i] = GUIElements.horizontalBorder;
                    layoutMatrix[x + height - 1, i] = GUIElements.horizontalBorder;
                }
            }

            foreach ((int x, int y) in cornerPoints)
            {
                layoutMatrix[x, y] = GUIElements.horizontalBorder;
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

        public virtual void GenerateInsides()
        {

        }
        public virtual string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            return innerText;
        }
    }

    public class TextBox : Window
    {
        public TextBox(int x, int y, int h, int w, string title, string text) : base(x, y, h, w)
        {
            UpdateTitle(title);
            UpdateText(text);
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateText(string text)
        {
            Text = text;
            currentLine = 0;
            GenerateInsides();
            lastLine = textLines.Count() - Height + 2 + 2; // two for borders, two for title
        }
        string title = "";
        public string Title
        {
            set
            {
                if (value.Length < (Width - 3))
                    title = value;
                else
                    title = value.Substring(0, Width - 3);
            }
            get { return title; }
        }
        public string Text = "";
        List<string> textLines = new List<string>();
        int currentLine = 0;
        int lastLine = 0;
        public void ScroolUp(bool scrollAll = false)
        {
            if (scrollAll) currentLine = 0;
            else if (currentLine > 0) currentLine--;
        }
        public void ScroolDown(bool scrollAll = false)
        {
            if (scrollAll) currentLine = lastLine;
            else if (currentLine < lastLine) currentLine++;
        }
        override public string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            Console.WriteLine("{0} / {1}", currentLine, lastLine);
            //Console.WriteLine(textLines.Count());
            // Console.WriteLine(Height - 2 - 2);
            // Console.WriteLine(currentLine > 0);
            // Console.WriteLine(currentLine < lastLine);
            if (currentLine > 0)
            {
                innerText[0, Width - 2 - 1] = GUIElements.upArrow;
            }
            if (currentLine < (textLines.Count() - Height + 2 - 1))
            {
                innerText[Height - 2 - 1, Width - 2 - 1] = GUIElements.downArrow;
            }

            int titleStaringPoint = (Width - 2 - title.Length - 1) / 2;
            //Console.WriteLine(titleStaringPoint);

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x++)
            {
                if ((currentLine + x - 2) >= textLines.Count()) break;
                for (int y = 0; y < textLines[currentLine + x - 2].Length; y++)
                {
                    //Console.WriteLine("{0}, {1}", x, y);
                    innerText[x, y] = textLines[currentLine + x - 2][y].ToString();
                }
            }

            return innerText;
        }
        override public void GenerateInsides()
        {
            string lineBuffer = "";
            int currentLength = 0;

            for (int i = 0; i < Text.Length; i++)
            {
                if (currentLength < (Width - 4)) // from left border to gap between text and scrollbar
                {
                    lineBuffer += Text[i];
                    currentLength++;
                }
                else if (currentLength == (Width - 4))
                {
                    if (lineBuffer[currentLength - 1] != ' ')
                    {
                        lineBuffer += '-';
                    }

                    textLines.Add(lineBuffer);

                    lineBuffer = Text[i].ToString();
                    currentLength = 1;
                }
            }

            // foreach (string line in textLines)
            // Console.WriteLine(line);
        }
    }
}