using System;
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

            Room board = new Room(10, 18);

            while (turnedOn)
            {
                board.Print();
                board.MovePlayer(Console.ReadKey());
            }
        }
    }
}
