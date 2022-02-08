﻿using System;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Random rdn = new Random();
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Player player = new Player(0, 0);
            Renderer renderer = new Renderer(150, 150, 10, 10);

            Map board = Generation.GenerateDungeon(150, 150);
            Console.WriteLine(Renderer.Render(board, Array.Empty<Entity>(), player, 0, 0));
        }
    }
}
