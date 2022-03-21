using System;
using System.IO;
using System.Text;

namespace roguelike_spbu {
    public enum GUIBorderTypes
    {
        None,
        Above,
        Below,
        OnRight,
        OnLeft
    }
    public static class GUIElements
    {
        public static string cornerBorders = "╔╚╗╝╠╣╩╦╬";
        public static string lineBorders = "║═";
        public static string verticalBorder = lineBorders[0].ToString(); //"║"; //ud
        public static string horizontalBorder = lineBorders[1].ToString(); //"═"; //rl
        public static string downrightBorder = cornerBorders[0].ToString(); //"╔"; //dr
        public static string uprightBorder = cornerBorders[1].ToString(); //"╚"; //ur
        public static string downleftBorder = cornerBorders[2].ToString(); //"╗"; //dl
        public static string upleftBorder = cornerBorders[3].ToString(); //"╝"; //ur
        public static string verticalrightBorder = cornerBorders[4].ToString(); //"╠"; //udr
        public static string verticalleftBorder = cornerBorders[5].ToString(); //"╣"; //udl
        public static string horizontalupBorder = cornerBorders[6].ToString(); //"╩"; //url
        public static string horizontaldownBorder = cornerBorders[7].ToString(); //"╦"; //drl
        public static string crossBorder = cornerBorders[8].ToString(); //"╬"; //udrl

        public static string upBorders = verticalBorder + uprightBorder + upleftBorder + verticalrightBorder + verticalleftBorder + horizontalupBorder + crossBorder;
        public static string downBorders = verticalBorder + downrightBorder + downleftBorder + verticalrightBorder + verticalleftBorder + horizontaldownBorder + crossBorder;
        public static string rightBorders = horizontalBorder + downrightBorder + uprightBorder + verticalrightBorder + horizontalupBorder + horizontaldownBorder + crossBorder;
        public static string leftBorders = horizontalBorder + downleftBorder + upleftBorder + verticalleftBorder + horizontalupBorder + horizontaldownBorder + crossBorder;
        public static bool IsTypeOfBorder(string input, GUIBorderTypes expectedType)
        {
            if (input.Length != 1) return false;
            if (expectedType == GUIBorderTypes.Above) return upBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.Below) return downBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.OnRight) return rightBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.OnLeft) return leftBorders.IndexOf(input) != -1;
            return false;
        }

        public static string upArrow = "▲";
        public static string downArrow = "▼";
        public static string scrollPointer = "▓"; //"◄";
        public static string listPointer = "►";
    }

    public class GUI
    {
        public bool error = false;
        public string errorMessage = "";
        public string[,] layoutMatrix = new string[0, 0];
        public string[,] fullScreenMatrix = new string[0, 0];
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
                fullScreenMatrix = new string[Height, Width];

                EraseLayoutMatrix();
                EraseFullScreenMatrix();

                CreateLayout();
            }
        }
        public void EraseLayoutMatrix()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    layoutMatrix[i, j] = " ";
        }
        public void EraseFullScreenMatrix()
        {
            for (int i = 0; i < Height; i++) // fill screen buffer 
                for (int j = 0; j < Width; j++)
                    fullScreenMatrix[i, j] = " ";
        }
        public void Print()
        {
            if (error)
            {
                Console.WriteLine(errorMessage);
                return;
            }

            bool isActiveFullscreen = false;
            foreach (Window window in windows)
            {
                if (!window.Active) continue;
                string[,] windowBuffer = window.GetInsides();
                if (window.FullSreen)
                {
                    isActiveFullscreen = true;
                    EraseFullScreenMatrix();

                    int startX = (Height - window.Height) / 2 - 1;
                    int startY = (Width - window.Width) / 2 - 1;

                    for (int i = startX + 1; i < startX + window.Height - 1; i++)
                    {
                        fullScreenMatrix[i, startY] = GUIElements.verticalBorder;
                        fullScreenMatrix[i, startY + window.Width - 1] = GUIElements.verticalBorder;
                    }
                    for (int i = startY + 1; i < startY + window.Width - 1; i++)
                    {
                        fullScreenMatrix[startX, i] = GUIElements.horizontalBorder;
                        fullScreenMatrix[startX + window.Height - 1, i] = GUIElements.horizontalBorder;
                    }

                    fullScreenMatrix[startX, startY] = GUIElements.downrightBorder;
                    fullScreenMatrix[startX + window.Height - 1, startY] = GUIElements.uprightBorder;
                    fullScreenMatrix[startX, startY + window.Width - 1] = GUIElements.downleftBorder;
                    fullScreenMatrix[startX + window.Height - 1, startY + window.Width - 1] = GUIElements.upleftBorder;

                    for (int i = 0; i < window.Height - 2; i++) // fill screen buffer 
                    {
                        for (int j = 0; j < window.Width - 2; j++)
                        {
                            fullScreenMatrix[i + startX + 1, j + startY + 1] = windowBuffer[i, j];
                        }
                    }
                }
                else
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
                    if (isActiveFullscreen)
                        screenBuffer.Append(fullScreenMatrix[i, j]);
                    else
                        screenBuffer.Append(layoutMatrix[i, j]);
                }
                screenBuffer.AppendLine();
            }

            Console.WriteLine(screenBuffer);
        }
        public bool IsTypeOfBorder((int, int) borderPoint, GUIBorderTypes expectedType)
        {
            int x = borderPoint.Item1;
            int y = borderPoint.Item2;
            if (x < 0 || y < 0 || x >= Height || y >= Width)
                return false;

            //if (layoutMatrix[x, y] == GUIElements.verticalBorder || layoutMatrix[x, y] == GUIElements.horizontalBorder)
            //Console.WriteLine("{0}:{1} {2}", x, y, GUIElements.IsTypeOfBorder(layoutMatrix[x, y], expectedType));
            return GUIElements.IsTypeOfBorder(layoutMatrix[x, y], expectedType);
        }
        public string GetCornerBorder((int, int) cornerPoint)
        {
            int x = cornerPoint.Item1;
            int y = cornerPoint.Item2;
            bool up = IsTypeOfBorder((x - 1, y), GUIBorderTypes.Below);
            bool down = IsTypeOfBorder((x + 1, y), GUIBorderTypes.Above);
            bool right = IsTypeOfBorder((x, y + 1), GUIBorderTypes.OnLeft);
            bool left = IsTypeOfBorder((x, y - 1), GUIBorderTypes.OnRight);

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

            EraseFullScreenMatrix();

            foreach (Window window in windows)
            {
                if (window.FullSreen) continue;
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
                layoutMatrix[x, y] = GUIElements.crossBorder;
            }

            foreach ((int, int) cornerPoint in cornerPoints)
            {
                int x = cornerPoint.Item1;
                int y = cornerPoint.Item2;

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
        public bool Active = true;
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

        public Window(int x, int y, int h, int w, bool fullSreen = false, bool active = true)
        {
            X = x;
            Y = y;
            Height = h;
            Width = w;
            FullSreen = fullSreen;
            Active = active;
        }
        public virtual void TurnOn()
        {
            Active = true;
        }
        public virtual void TurnOff()
        {
            Active = false;
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
        public TextBox(int x, int y, int h, int w, string title, string text, bool fullSreen = false) : base(x, y, h, w)
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
            if ((Height - 2 - 2) > textLines.Count()) lastLine = 0;
            else lastLine = textLines.Count() - Height + 2 + 2; // two for borders, two for title
        }
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

            if (Text.Length != 0)
            {
                if (currentLine > 0)
                    innerText[0, Width - 2 - 1] = GUIElements.upArrow;

                if (currentLine < lastLine)
                    innerText[Height - 2 - 1, Width - 2 - 1] = GUIElements.downArrow;

                if (lastLine != 0)
                {
                    int midPoint = (int)Math.Round(((double)(Height - 2 - 2 - 1) * currentLine) / lastLine, MidpointRounding.AwayFromZero);
                    innerText[1 + midPoint, Width - 2 - 1] = GUIElements.scrollPointer;
                }
            }

            int titleStaringPoint;
            if (Text.Length != 0)
                titleStaringPoint = (Width - 2 - title.Length - 1) / 2;
            else
                titleStaringPoint = (Width - 2 - title.Length) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x++)
            {
                if ((currentLine + x - 2) >= textLines.Count()) break;
                for (int y = 0; y < textLines[currentLine + x - 2].Length; y++)
                {
                    innerText[x, y] = textLines[currentLine + x - 2][y].ToString();
                }
            }

            return innerText;
        }
        override public void GenerateInsides()
        {
            textLines = new List<string>();
            string lineBuffer = "";
            int currentLength = 0;

            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n')
                {
                    textLines.Add(lineBuffer);
                    lineBuffer = "";
                    currentLength = 0;
                    continue;
                }
                if (currentLength < (Width - 4)) // from left border to gap between text and scrollbar
                {
                    lineBuffer += Text[i];
                    currentLength++;
                }
                else if (currentLength == (Width - 4))
                {
                    if (lineBuffer[currentLength - 1] != ' ' && i != (Text.Length - 2) && Text[i] != ' ')
                    {
                        lineBuffer += '-';
                    }

                    textLines.Add(lineBuffer);

                    lineBuffer = Text[i].ToString();
                    currentLength = 1;
                }
            }
            if (lineBuffer != "")
                textLines.Add(lineBuffer);

            // foreach (string line in textLines)
            //     Console.WriteLine("> {0}", line);
        }
    }
    public class ListBox : Window
    {
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
        public ListBox(int x, int y, int h, int w, string title, List<string> textLines) : base(x, y, h, w)
        {
            UpdateTitle(title);
            UpdateList(textLines);
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateList(List<string> textLines)
        {
            this.textLines = new List<string>();
            foreach (string line in textLines)
            {
                if (line.Length < (Width - 4))
                    this.textLines.Add(line);
                else
                    this.textLines.Add(line.Substring(0, Width - 4));
            }
            if (this.textLines.Count() != 0)
                lastLine = this.textLines.Count() - 1;
            else lastLine = 0;
        }
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

            if (textLines.Count() > 0)
                innerText[2, 0] = GUIElements.listPointer;

            if (currentLine > 0)
                innerText[0, Width - 2 - 1] = GUIElements.upArrow;

            if (currentLine < lastLine)
                innerText[Height - 2 - 1, Width - 2 - 1] = GUIElements.downArrow;

            if (lastLine != 0)
            {
                int midPoint = (int)Math.Round(((double)(Height - 2 - 2 - 1) * currentLine) / lastLine, MidpointRounding.AwayFromZero);
                innerText[1 + midPoint, Width - 2 - 1] = GUIElements.scrollPointer;
            }

            int titleStaringPoint = (Width - 2 - title.Length - 1) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x += 2)
            {
                if ((currentLine + x / 2 - 1) >= textLines.Count()) break;
                for (int y = 0; y < textLines[currentLine + x / 2 - 1].Length; y++)
                {
                    innerText[x, y + 1] = textLines[currentLine + x / 2 - 1][y].ToString();
                }
            }

            return innerText;
        }
    }
    public class MenuBox : Window
    {
        string title = "";
        public string Title
        {
            set
            {
                if (value.Length < (Width - 2))
                    title = value;
                else
                    title = value.Substring(0, Width - 2);
            }
            get { return title; }
        }
        public string Text = "";
        List<string> textLines = new List<string>();
        int currentLine = 0;
        int lastLine = 0;
        public MenuBox(int h, int w, string title, List<string> textLines) : base(0, 0, h, w, true, false)
        {
            UpdateTitle(title);
            UpdateList(textLines);
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateList(List<string> textLines)
        {
            this.textLines = new List<string>();
            foreach (string line in textLines)
            {
                if (line.Length < (Width - 3))
                    this.textLines.Add(line);
                else
                    this.textLines.Add(line.Substring(0, Width - 3));
            }
            if (this.textLines.Count() != 0)
                lastLine = this.textLines.Count() - 1;
            else lastLine = 0;
        }
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

            if (textLines.Count() > 0)
                innerText[2 + currentLine * 2, 0] = GUIElements.listPointer;

            int titleStaringPoint = (Width - 2 - title.Length) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x += 2)
            {
                if ((x / 2 - 1) >= textLines.Count()) break;
                int lineStaringPoint = (Width - 2 - textLines[x / 2 - 1].Length) / 2;
                for (int y = 0; y < textLines[x / 2 - 1].Length; y++)
                {
                    innerText[x, y + lineStaringPoint] = textLines[x / 2 - 1][y].ToString();
                }
            }

            return innerText;
        }
    }
}