using System;


namespace roguelike_spbu
{
    class GameBoard
    {
        private int length;
        private int width;
        private char[][] board;

        public int Length
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public char[][] Board
        {
            get; set;
        }

        public GameBoard(int width, int length)
        {
            this.Width = width;
            this.Length = length;
            this.Board = new char[Width][];
        }

        public void Generate()
        {
            for (int i = 0; i < this.Length; i++)
            {
                char[] temp = new char[Width];

                for (int j = 0; j < this.Width; j++)
                {
                    temp[j] = '.';
                }

                this.Board[i] = temp;
            }
        }

        public void Print()
        {
            foreach (char[] temp in this.Board)
            {
                string res = "";

                foreach (char ch in temp)
                {
                    res += ch + " ";
                }

                Console.WriteLine(res.Trim());
            }
        }

    }
}
