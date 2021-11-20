using System;
using System.Threading;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool turnedOn = true;

            Room board = new Room(3, 5);
            
            while (turnedOn)
            { 
                board.Print();
                board.MovePlayer(Console.ReadKey());
            }
        }
    }
}
