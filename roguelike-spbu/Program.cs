﻿using System.Text;
namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Random rdn = new Random();
            var chars = "$%!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Player player = new Player(0, 0);
            Entity[] entities = new Entity[5];
            Renderer renderer = new Renderer(40, 150, 10, 10);


            for (int i = 0; i < entities.Length; i++)
            {
                Entity tmp = new Entity();
                tmp.X = rdn.Next(45);
                tmp.Y = rdn.Next(180);
                tmp.Symbol = chars[rdn.Next(chars.Length)].ToString();
                tmp.PrimaryForegroundColor = System.Drawing.Color.White;
                entities[i] = tmp;
            }

            Map board = Generation.GenerateDungeon(45, 180);
            for (int i = 0; i < 180; i++)
            {
                board.Tiles[0][i].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
                board.Tiles[44][i].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
            }
            for (int i = 0; i < 45; i++)
            {
                board.Tiles[i][0].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
                board.Tiles[i][179].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
            }
            for (int i = 0; i < 180; i++)
            {
                player.X = i % 45;
                player.Y = i % 180;
                Console.WriteLine("@ {0} {1}", player.X, player.Y);
                StringBuilder bf = Renderer.Render(board, entities, player);
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(bf);

                /*for (int j = 0; j < entities.Length; j++)
                {
                    Entity tmp = entities[j];
                    Console.WriteLine("{0} {1} {2}", tmp.Symbol, tmp.X, tmp.Y);
                }*/
                Console.ReadLine();
                Console.SetCursorPosition(0, 0);
                Console.Clear();
            }
        }
    }
}
