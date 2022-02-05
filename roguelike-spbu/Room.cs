using System;
using System.Text;
using Pastel;


namespace roguelike_spbu
{
    class Room
    {
        
        public int X0 {
            get;
            set;
        }
        public int X1 {
            get;
            set;
        }
        public int Y0 {
            get;
            set;
        }
        public int Y1 {
            get;
            set;
        }
        
        public Room(int x0, int x1, int y0, int y1) {
            X0 = x0;
            Y0 = y0;
            X1 = x1;
            Y1 = y1;
        }
        
        
        /*private int x;
        private int y;
        private Tile[][] board;
        private Tile[] tiles;
        private Player player;
        private Enemy[] enemies;
        private KeyBoardMaster kM;



        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;

            board = new Tile[this.x][];

            for (int i = 0; i < this.x; i++)
            {
                board[i] = new Tile[this.y];
            }

            this.tiles = Generate3();

            kM = new KeyBoardMaster();
            kM.downPressedEvent += down;
            kM.upPressedEvent += up;
            kM.rightPressedEvent += right;
            kM.leftPressedEvent += left;



            Generate();
        }

        private void Generate()
        {
            foreach (Tile tile in this.tiles)
            {
                this.board[tile.Landscape.X][tile.Landscape.Y] = tile;
            }

        }

        private Tile[] Generate2()
        {
            Random rand = new Random();

            Tile[] temp = new Tile[x * y];

            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    int r = rand.Next(0, 4);

                    switch (r)
                    {
                        case 0:
                            temp[n * y + m] = new Tile(new Field(n, m));
                            break;
                        case 1:
                            temp[n * y + m] = new Tile(new Tree(n, m));
                            break;
                        case 2:
                            temp[n * y + m] = new Tile(new Rock(n, m));
                            break;
                        case 3:
                            temp[n * y + m] = new Tile(new Water(n, m));
                            break;
                    }

                }
            }

            if (this.player != null)
            {
                temp[player.X * this.y + player.Y].Inhabitat = player;
            }
            else
            {
                player = new Player(0, 0);
                temp[0] = new Tile(temp[0].Landscape, player);
            }

            return temp;
        }

        public Tile[] Generate3()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Random rand = new Random();
                    int r = rand.Next(0, 4);


                    if (board[i][j] == null)
                    {
                        switch (r)
                        {
                            case 0:
                                if (j - 1 >= 0)
                                {
                                    if (board[i][j - 1] != null)
                                    {
                                        board[i][j] = new Tile(getValueOfType(i, j, board[i][j - 1].Landscape));
                                    }
                                    else
                                    {
                                        board[i][j] = new Tile(getRandom(i, j));
                                    }

                                }
                                else
                                {
                                    board[i][j] = new Tile(getRandom(i, j));
                                }
                                break;
                            case 1:
                                if (i - 1 >= 0)
                                {
                                    if (board[i - 1][j] != null)
                                    {
                                        board[i][j] = new Tile(getValueOfType(i, j, board[i - 1][j].Landscape));
                                    }
                                    else
                                    {
                                        board[i][j] = new Tile(getRandom(i, j));
                                    }

                                }
                                else
                                {
                                    board[i][j] = new Tile(getRandom(i, j));
                                }
                                break;
                            case 2:
                                if (j + 1 < x)
                                {
                                    if (board[i][j + 1] != null)
                                    {
                                        board[i][j] = new Tile(getValueOfType(i, j, board[i][j + 1].Landscape));
                                    }
                                    else
                                    {
                                        board[i][j] = new Tile(getRandom(i, j));
                                    }

                                }
                                else
                                {
                                    board[i][j] = new Tile(getRandom(i, j));
                                }
                                break;
                            case 3:
                                if (i + 1 < x)
                                {
                                    if (board[i + 1][j] != null)
                                    {
                                        board[i][j] = new Tile(getValueOfType(i, j, board[i + 1][j].Landscape));
                                    }
                                    else
                                    {
                                        board[i][j] = new Tile(getRandom(i, j));
                                    }

                                }
                                else
                                {
                                    board[i][j] = new Tile(getRandom(i, j));
                                }
                                break;

                        }
                    }

                }
            }
            return flatten();
        }

        public Tile[] flatten()
        {
            Tile[] tile = new Tile[x * x];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    tile[x * i + j] = board[i][j];
                }
            }

            if (this.player != null)
            {
                tile[player.X * this.y + player.Y].Inhabitat = player;
            }
            else
            {
                player = new Player(0, 0);
                tile[0] = new Tile(tile[0].Landscape, player);
            }

            return tile;
        }

        private TileType getRandom(int x, int y)
        {
            Random rand = new Random();
            int r = rand.Next(0, 4);

            switch (r)
            {
                case 0:
                    return new Water(x, y);
                case 1:
                    return new Rock(x, y);
                case 2:
                    return new Field(x, y);
                case 3:
                    return new Tree(x, y);

            }

            return new TileType();
        }

        private TileType getValueOfType(int x, int y, TileType TileType)
        {
            if (TileType.GetType() == new Rock(0, 0).GetType())
            {
                return new Rock(x, y);
            }
            else if (TileType.GetType() == new Field(0, 0).GetType())
            {
                return new Field(x, y);
            }
            else if (TileType.GetType() == new Water(0, 0).GetType())
            {
                return new Water(x, y);
            }
            else if (TileType.GetType() == new Tree(0, 0).GetType())
            {
                return new Tree(x, y);
            }

            return new TileType();

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
            Console.SetCursorPosition(0, 0);

            StringBuilder buffer = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Tile tile = board[i][j];

                    if (tile.Inhabitat != null)
                    {

                        buffer.Append(tile.Inhabitat.Symbol.ToString().Pastel(ColorConvertor.FromColor(tile.Inhabitat.Color)));
                        // Console.ForegroundColor = tile.Inhabitat.Color;
                        // Console.Write(tile.Inhabitat.Symbol);
                    }
                    else
                    {
                        buffer.Append(tile.Landscape.Symbol.ToString().Pastel(ColorConvertor.FromColor(tile.Landscape.Color)));
                        // Console.ForegroundColor = tile.Landscape.Color;
                        // Console.Write(tile.Landscape.Symbol);
                    }
                }
                buffer.AppendLine();
                // Console.WriteLine();
            }

            Console.WriteLine(buffer);
        }
        private void left()
        {
            if (player.Y > 0)
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X][player.Y - 1].Inhabitat = player;
                player.Y -= 1;
            }
            else
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X][y - 1].Inhabitat = player;
                player.Y = y - 1;

                this.tiles = Generate2();
                Generate();
            }
        }

        private void right()
        {
            if (player.Y < y - 1)
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X][player.Y + 1].Inhabitat = player;
                player.Y += 1;
            }
            else
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X][0].Inhabitat = player;
                player.Y = 0;

                this.tiles = Generate2();
                Generate();
            }
        }

        private void up()
        {
            if (player.X > 0)
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X - 1][player.Y].Inhabitat = player;
                player.X -= 1;
            }
            else
            {
                board[player.X][player.Y].Inhabitat = null;
                board[x - 1][player.Y].Inhabitat = player;
                player.X = x - 1;

                this.tiles = Generate2();
                Generate();
            }
        }

        private void down()
        {

            if (player.X < x - 1)
            {
                board[player.X][player.Y].Inhabitat = null;
                board[player.X + 1][player.Y].Inhabitat = player;
                player.X += 1;
            }
            else
            {
                board[player.X][player.Y].Inhabitat = null;
                board[0][player.Y].Inhabitat = player;
                player.X = 0;

                this.tiles = Generate2();
                Generate();
            }
        }
        */
    }
}
