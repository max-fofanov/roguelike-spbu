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

            GameBoard board = new GameBoard(15, 15);
            
            while (turnedOn)
            { 
                board.Print();
                board.MovePlayer(Console.ReadKey());
            }
        }
    }
}
