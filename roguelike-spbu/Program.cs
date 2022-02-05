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
            bool turnedOn = true;

            /*
            Room board = new Room(10, 18);

            while (turnedOn)
            {
                board.Print();
                board.MovePlayer(Console.ReadKey());
            }
            */
            for (int a = 0; a < 10; a++) {
                char[][] board = Generation.GenerateDungeon(45, 180);

                for (int i = 0; i < 45; i++) {
                    for (int j = 0; j < 180; j++) {
                        Console.Write(board[i][j]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
            }

        }
    }
}
