﻿using System.Collections.Generic;
using System.Text;
using Pastel;
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
            Environment.Exit(0);
        }

        static bool IsEqual(double x, double y, double exp = 0.1)
        {
            return Math.Abs(x - y) < exp;
        }

        static void Main(string[] args)
        {
            MakeConsoleReady();

            Random rnd = new Random();
            var chars = "$%!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Player player = new Player(0, 0);
            Entity[] entities = new Entity[3];
            Renderer renderer = new Renderer(40, 150, 20, 20);



            bool finished = false;
            Map board = Generation.GenerateDungeon(45, 180);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity tmp = new Entity();

                tmp.X = rnd.Next(45);
                tmp.Y = rnd.Next(180);

                while (board.Tiles[tmp.X][tmp.Y].Impassable || board.Tiles[tmp.X][tmp.Y].GetType() == typeof(Void)) //TODO
                {
                    tmp.X = rnd.Next(45);
                    tmp.Y = rnd.Next(180);
                }

                tmp.VStatus = VisualStatus.wasSeen;
                tmp.Symbol = chars[rnd.Next(chars.Length)].ToString();
                tmp.PrimaryForegroundColor = System.Drawing.Color.White;
                entities[i] = tmp;
            }

            for (int i = 0; i < 45 && !finished; i++)
            {
                for (int j = 0; j < 180 && !finished; j++)
                {
                    if (!board.Tiles[i][j].Impassable && board.Tiles[i][j].GetType() != typeof(Void))
                    {
                        player = new Player(i, j);
                        finished = true;
                    }
                }
            }
            //Map board = Generation.GenerateCave(45, 180, (0, 0), (22, 179));
            //Map board = new Map(100, 180);

            //List<List<(int, int)>> Rays = FOV.GetRaysInEllipse((int)(16 * 1.5), (int)(9 * 1.5));\

            Engine engine = new Engine(board, entities, player);
            Console.SetCursorPosition(0, 0);
            engine.Turn(true);

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                engine.Turn();
            }
        }
    }
}
