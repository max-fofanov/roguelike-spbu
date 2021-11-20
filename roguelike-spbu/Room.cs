using System;


namespace roguelike_spbu
{       
    class Room
    {
        private int length;
        private int width;
        private Tile[][] board;
        private Tile[] tiles;
        private Player player;
        private KeyBoardMaster kM;



        public Room(int width, int length/*, Tile[] tiles*/)
        {
            this.width = width;
            this.length = length;
            this.tiles = Generate2(width, length);

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
                this.board[tile.Landscape.Y][tile.Landscape.X] = tile;
            }

        }

        private Tile[] Generate2(int i, int j)
        {
            Random rand = new Random();
            
            Tile[] temp = new Tile[i * j];
            
            for (int n = 0; n < i; n++)
            {
                for (int m = 0; m < j; m++)
                {
                    int t = rand.Next(0, 4);
                    
                    switch (t)
                    {
                        case 0:
                            temp[n * j + m] = new Tile(new Field(m, n));
                            break;
                        case 1:
                            temp[n * j + m] = new Tile(new Tree(m, n));
                            break;
                        case 2:
                            temp[n * j + m] = new Tile(new Rock(m, n));
                            break;
                        case 3:
                            temp[n * j + m] = new Tile(new Water(m, n));
                            break;
                    }
                }
            }

            if (this.player != null)
            {
                temp[player.Y * length + player.X].Inhabitat = player;
            }
            else
            {
                player = new Player(0, 0);
                temp[0] = new Tile(temp[0].Landscape, player);
            }

            return temp;
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
            else
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y][length - 1].Inhabitat = player;
                player.X = length - 1;

                this.tiles = Generate2(width, length);
                Generate();
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
            else
            {
                board[player.Y][player.X].Inhabitat = null;
                board[player.Y][0].Inhabitat = player;
                player.X = 0;

                this.tiles = Generate2(width, length);
                Generate();
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
            else
            {
                board[player.Y][player.X].Inhabitat = null;
                board[width - 1][player.X].Inhabitat = player;
                player.Y = width - 1;

                this.tiles = Generate2(width, length);
                Generate();
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
            else
            {
                board[player.Y][player.X].Inhabitat = null;
                board[0][player.X].Inhabitat = player;
                player.Y = 0;

                this.tiles = Generate2(width, length);
                Generate();
            }
        }
    }
}
