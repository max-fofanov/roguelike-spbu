﻿using System;
using NetCoreAudio;

namespace roguelike_spbu
{
    public class Program
    {
        static ConsoleColor backgroundColor;
        static ConsoleColor foregroundColor;
        

        public static void MakeConsoleReady()
        {
            Console.Clear();
            Console.CursorVisible = false;
            backgroundColor = Console.BackgroundColor;
            foregroundColor = Console.ForegroundColor;
        }

        public static void NormilizeConsole()
        {
            Console.Clear();
            Console.CursorVisible = true;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            Walkman.Stop();
            Environment.Exit(0);
        }

        static bool IsEqual(double x, double y, double exp = 0.1)
        {
            return Math.Abs(x - y) < exp;
        }
        static void Main(string[] args)
        {
            MakeConsoleReady();
            
            Game game = new Game();
            while (true)
                game.FullTurn();
        }
        static void Main3(string[] args)
        {
            MakeConsoleReady();
            Window mm = new Window(0, 0, 17, 30);
            TextBox stats = new TextBox(16, 0, 36, 30, "Stats", "HP:999\n\nRange:10\n\nEtc: Smth\n\nEtc: Smth\n\nEtc: Smth");
            Window game = new Window(0, 29, 42, 152);
            Window underBar = new Window(41, 29, 11, 152);
            ListBox lb = new ListBox(0, 180, 26, 30, "Inventory", new List<string>() { "Sword", "Potion", "Bat", "Zombie", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49" });
            TextBox tb = new TextBox(25, 180, 25, 30, "Description", "Меч\n\nAtt:12\n\nМеч — вид холодного (белого) оружия с прямым клинком, предназначенный для рубящего и колющего ударов[2].\nЛезвие меча заточено с одной или двух сторон[3]. В самом широком смысле слово «Меч» — собирательное название всего длинного клинкового оружия с прямым клинком.\nВ современном отечественном историческом оружиеведении принято более узкое определение меча — наступательное оружие с обоюдоострым прямым клинком длиной более 60 сантиметров, предназначенное прежде всего для рубящих ударов. Оружие с однолезвийным прямым клинком относят к палашам[2].");
            TextBox mode = new TextBox(49, 180, 3, 30, "Mode", "");
            MenuBox menu = new MenuBox(30, 30, "Menu", new List<string>() { "First", "Second", "Third" });
            menu.TurnOn();
            menu.ScroolDown(true);
            int t = 0;
            while (true)
            {
                List<Window> windows = new List<Window>();

                if (t % 3 == 0)
                    menu.TurnOff();
                else if (t % 3 == 1)
                    menu.TurnOn();
                else
                    menu.ScroolUp();
                t++;

                windows.Add(mm);
                windows.Add(stats);
                windows.Add(game);
                windows.Add(underBar);
                windows.Add(lb);
                windows.Add(tb);
                windows.Add(mode);
                windows.Add(menu);

                GUI g = new GUI(windows);

                g.CalculateSize();
                //Console.WriteLine("size {0} {1}", g.Height, g.Width);
                g.Print();
                Console.SetCursorPosition(0, 0);
                lb.ScroolDown();
                tb.ScroolDown();
                lb.UpdateTitle("Enemies");
                Console.ReadKey(true);
                //tb.ScroolUp();
            }

        }
        static void Main2(string[] args)
        {
            TextBox tb = new TextBox(0, 5, 8, 20, "ДНК123", "Гомологичная рекомбинация — тип генетической рекомбинации, во время которой происходит обмен нуклеотидными последовательностями между двумя похожими или идентичными хромосомами. Это наиболее широко используемый клетками способ устранения двух- или однонитевых повреждений ДНК.");
            //tb.ScroolDown(true);
            string i = "";
            while (true)
            {
                tb.UpdateText(i.ToString());
                i += "a";

                MakeConsoleReady();
                List<Window> windows = new List<Window>();
                windows.Add(new Window(0, 0, 5, 6));
                windows.Add(tb);

                GUI g = new GUI(windows);

                g.CalculateSize();
                //Console.WriteLine("size {0} {1}", g.Height, g.Width);
                g.Print();
                //tb.ScroolUp();
                Console.ReadLine();
            }
        }
    }
}
