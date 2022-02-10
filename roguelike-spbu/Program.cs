﻿using System.Text;
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
        
        static void Main(string[] args)
        {
            MakeConsoleReady();

            Random rnd = new Random();
            var chars = "$%!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Player player = new Player(0, 0);
            Entity[] entities = new Entity[1];
            Renderer renderer = new Renderer(40, 150, 20, 20);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity tmp = new Entity();
                tmp.X = rnd.Next(45);
                tmp.Y = rnd.Next(180);
                tmp.Symbol = chars[rnd.Next(chars.Length)].ToString();
                tmp.PrimaryForegroundColor = System.Drawing.Color.White;
                entities[i] = tmp;
            }

            //Map board = Generation.GenerateDungeon(90, 180);
            Map board = Generation.GenerateCave(45, 180, (0, 0), (22, 179));
            //Map board = new Map(45, 180);
            Engine engine = new Engine(board, entities, player);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Renderer.Render(board, entities, player));

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                engine.Turn();
            }

            /*for (int i = 0; i < 180; i++)
            {
                board.Tiles[0][i].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
                board.Tiles[44][i].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
            }
            for (int i = 0; i < 45; i++)
            {
                board.Tiles[i][0].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
                board.Tiles[i][179].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
            }*/

            /*for (int i = 0; i < 180; i++)
            {
                player.X = i % 45;
                player.Y = i % 180;
                Console.WriteLine("@ {0} {1}", player.X, player.Y);
                StringBuilder bf = Renderer.Render(board, entities, player);
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(bf);

                // for (int j = 0; j < entities.Length; j++)
                // {
                //     Entity tmp = entities[j];
                //     Console.WriteLine("{0} {1} {2}", tmp.Symbol, tmp.X, tmp.Y);
                // }
                Console.ReadLine();
                Console.SetCursorPosition(0, 0);
                Console.Clear();
            }*/

        }
    }
}
