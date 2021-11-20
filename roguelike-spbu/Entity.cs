using System;

namespace roguelike_spbu
{
    class Entity
    {
        private int x;
        private int y;
        private char symbol;
        private ConsoleColor color;

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public char Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }
        
    }

    class Tree : Entity
    {
        private int x;
        private int y;
        private ConsoleColor color = ConsoleColor.Green;
        private char symbol = 'T';

        public Tree(int x, int y)
        {
            X = x;
            Y = y;
            Color = this.color;
            Symbol = this.symbol;
        }

    }

    class Rock : Entity
    {
        private int x;
        private int y;
        private ConsoleColor color = ConsoleColor.Gray;
        private char symbol = 'R';

        public Rock(int x, int y)
        {
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
        }

    }

    class Field : Entity
    {
        private int x;
        private int y;
        private ConsoleColor color = ConsoleColor.White;
        private char symbol = '.';

        public Field(int x, int y)
        {
            X = x;
            Y = y;
            Color = this.color;
            Symbol = this.symbol;
        }

    }

    class Water : Entity
    {
        private int x;
        private int y;
        private ConsoleColor color = ConsoleColor.Blue;
        private char symbol = 'W';

        public Water(int x, int y)
        {
            X = x;
            Y = y;
            Color = this.color;
            Symbol = this.symbol;
        }

    }
}
