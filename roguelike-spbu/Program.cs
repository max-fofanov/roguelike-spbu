﻿using System;
using System.Drawing;
using System.Threading;
using Pastel;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Clear();
            Console.CursorVisible = false;

            Map board = Generation.GenerateCave(45, 180, (22, 0), (22, 179));
            Player player = new Player(22, 0);
            Renderer renderer = new Renderer(board, player);
            // Console.WriteLine(renderer.Render(0, 0, 45, 180));

            for (int a = 0; a < 1000; a++)
            {
                Map board1 = Generation.GenerateCave(45, 180, (22, 0), (22, 179));
                renderer.SetMap(board1);
                Console.WriteLine(renderer.Render(0, 0, 45, 180));
                Console.SetCursorPosition(0, 0);

            }
            */
            float [] prob = {0.5F, 1, 1};
            int [] res = new int[prob.Length];
            for (int i = 0; i < 10000; i++)
            {
                res[Walker.Alias(prob)] ++;
            }

            foreach (int n in res)
                Console.WriteLine(n);

        }
    }
}
