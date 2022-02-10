﻿using System.Collections.Generic;
using System.Text;
using Pastel;
namespace roguelike_spbu
{
    public class Program
    {
        static ConsoleColor backgroundColor;
        static ConsoleColor foregroundColor;


        public static void MakeConsoleReady() {
            Console.Clear();
            Console.CursorVisible = false;
            backgroundColor = Console.BackgroundColor;
            foregroundColor = Console.ForegroundColor;
        }

        public static void NormilizeConsole() {
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

            Player player = new Player(41, 41);
            Entity[] entities = new Entity[5];
            Renderer renderer = new Renderer(40, 150, 20, 20);


            for (int i = 0; i < entities.Length; i++)
            {
                Entity tmp = new Entity();
                tmp.X = rnd.Next(45);
                tmp.Y = rnd.Next(180);
                tmp.VStatus = VisualStatus.wasSeen;
                tmp.Symbol = chars[rnd.Next(chars.Length)].ToString();
                tmp.PrimaryForegroundColor = System.Drawing.Color.White;
                entities[i] = tmp;
            }

            // Map board = Generation.GenerateDungeon(45, 180);
            // Map board = Generation.GenerateCave(45, 180, (0, 0), (22, 179));
            Map board = new Map(100, 180);

            List<List<(int, int)>> Rays = FOV.GetRaysInEllipse((int)(16 * 1.5), (int)(9 * 1.5));
            foreach (List<(int, int)> ray in Rays)
            {
                foreach ((int, int) point in ray)
                {
                    board.Tiles[point.Item1 + 41][point.Item2 + 41] = new Rock();
                }
            }


            Engine engine = new Engine(board, entities, player);
            Console.WriteLine(Renderer.Render(board, entities, player));

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                engine.Turn();
            }
        }
    }
}
