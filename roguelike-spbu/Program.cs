using System;
using System.Threading;

namespace roguelike_spbu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();
            bool turnedOn = true;
            

            Room board = new Room(10, 18);
            
            while (turnedOn)
            { 
                board.Print_2();
                board.MovePlayer(Console.ReadKey());
            }
        } 
    }
}