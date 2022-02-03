<<<<<<< HEAD
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
                char[][] board = Generation.Generate(45, 180, (22, 0), (22, 179));

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
=======
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

            // Room board = new Room(10, 18);

            // while (turnedOn)
            // {
            //     board.Print();
            //     board.MovePlayer(Console.ReadKey());
            // }
        }
    }
}
>>>>>>> 780df2adc037017609b9ffa6db56e16d9cb2f2b6
