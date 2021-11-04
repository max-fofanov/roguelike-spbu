using System;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(10, 10);

            board.Generate();
            board.Print();
        }
    }
}
