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

            GameBoard board = new GameBoard(3, 2, new Tile[] { new Tile(new Rock(0, 0), new Player(0, 0)),
                new Tile(new Field(0, 1)), 
                new Tile(new Tree(1, 0)), 
                new Tile(new Rock(1, 1)),
                new Tile(new Tree(2, 0)),
                new Tile(new Tree(2, 1))
            });

            
            while (turnedOn)
            { 
                board.Print();
                board.MovePlayer(Console.ReadKey());
            }
        }
    }
}
