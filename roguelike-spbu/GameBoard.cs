using System;


namespace roguelike_spbu
{       
    class GameBoard
    {
        private int length;
        private int width;
        private Tile[][] board;
        private Tile[] tiles;
        private Player player;
        private KeyBoardMaster kM;



        public GameBoard(int width, int length, Tile[] tiles)
        {
            this.width = width;
            this.length = length;
            this.tiles = tiles;

            kM = new KeyBoardMaster();
            kM.downPressedEvent += down;
            kM.upPressedEvent += up;
            kM.rightPressedEvent += right;
            kM.leftPressedEvent += left;

            Tile[][] board = new Tile[width][];

            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new Tile[length];
            }

            this.board = board;

            Generate();
        }

        private void Generate()
        {
            foreach (Tile tile in this.tiles)
            {
                this.board[tile.Landscape.X][tile.Landscape.Y] = tile;
            }

        }

        public void MovePlayer(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow)
                kM.UpPressedEvent();
            else if (key.Key == ConsoleKey.DownArrow)
                kM.DownPressedEvent();
            else if (key.Key == ConsoleKey.RightArrow)
                kM.RightPressedEvent();
            else if (key.Key == ConsoleKey.LeftArrow)
                kM.LeftPressedEvent();


        }


        public void Print()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Tile tile = board[i][j];

                    if (tile.Inhabitat != null)
                    {
                        if (tile.Inhabitat is Player)
                        {
                            player = (Player) tile.Inhabitat;
                        }
                        Console.ForegroundColor = tile.Inhabitat.Color;
                        Console.Write(tile.Inhabitat.Symbol);
                    }
                    else
                    {
                        Console.ForegroundColor = tile.Landscape.Color;
                        Console.Write(tile.Landscape.Symbol);
                    }
                    
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
        }
        private void left()
        {
            if (player.X > 0)
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y][player.X - 1].Inhabitat = player;
                player.X -= 1;
            }
        }

        private void right()
        {
            if (player.X < length - 1)
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y][player.X + 1].Inhabitat = player;
                player.X += 1;
            }
        }

        private void up()
        {
            if (player.Y > 0)
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y - 1][player.X].Inhabitat = player;
                player.Y -= 1;
            }
        }

        private void down()
        {

            if (player.Y < width - 1)
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y + 1][player.X].Inhabitat = player;
                player.Y += 1;
            }
        }
    }
}
