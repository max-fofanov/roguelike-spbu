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
            Console.Clear();
            Console.CursorVisible = false;
            Player player = new Player(22, 0);

            for (int a = 0; a < 1000; a++)
            {
                Map board1 = Generation.GenerateCave(45, 180, (22, 0), (22, 179));
                Console.WriteLine(Renderer.Render(board1, player, 0, 0, 45, 180));
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
