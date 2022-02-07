﻿using System;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Clear();
            Console.CursorVisible = false;
            Random rdn = new Random();
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Player player = new Player(22, 0);
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

            Map board1 = Generation.GenerateCave(45, 180, (22, 0), (22, 179));
            Console.WriteLine(Renderer.Render(board1, entities, player, 0, 0));

            for (int i = 0; i < entities.Length; i++)
            {
                Entity tmp = entities[i];
                Console.WriteLine("{0} {1} {2}", tmp.Symbol, tmp.X, tmp.Y);
            }
<<<<<<< HEAD
            */
            float [] prob = {0.5F, 1, 1};
            int [] res = new int[prob.Length];
            for (int i = 0; i < 10000; i++)
            {
                res[Walker.Alias(prob)] ++;
            }

            foreach (int n in res)
                Console.WriteLine(n);

=======
>>>>>>> 85519d0a295996c56ed35229807532fb80af56cf
        }
    }
}
