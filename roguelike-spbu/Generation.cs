using System;

namespace roguelike_spbu {
    
    public class Generation {

        public static Map GenerateCave(int x, int y, (int, int) startingPosition, (int, int) endingPosition) {

            Map dungeon = new Map(x, y, 0);
            Player player = new Player(startingPosition.Item1, startingPosition.Item2);
            Random random = new Random();
            int freeSpace = 1;

            while (player.X != endingPosition.Item1 || player.Y != endingPosition.Item2)
            {
                if (dungeon.Tiles[player.X][player.Y] is Border)
                {
                    freeSpace += 1;
                }
                dungeon.Tiles[player.X][player.Y] = new Field(player.X, player.Y);

                int i = random.Next(x + y);

                if (i >= 0 && i <= x - 1)
                {
                    player.X += random.Next(3) - 1;
                    if (player.X < 0)
                    {
                        player.X += 1;
                    }
                    else if (player.X > x - 1)
                    {
                        player.X -= 1;
                    }
                }
                else if (i >= x && i <= x + y)
                {
                    player.Y += random.Next(3) - 1;
                    if (player.Y < 0)
                    {
                        player.Y += 1;
                    }
                    else if (player.Y > y - 1)
                    {
                        player.Y -= 1;
                    }
                }

            }

            dungeon.Tiles[player.X][player.Y] = new Field(player.X, player.Y);

            if (freeSpace * 2 > x * y)
            {
                return GenerateCave(x, y, startingPosition, endingPosition);
            }
            else
            {
                return dungeon;
            }

        }

        public enum From {
            Left, Right, Up, Down
        }
        public static Map GenerateDungeon(int x, int y, From from = From.Down, int num = 0, int minsize = 10, int maxsize = 20) {

            Map dungeon = new Map(x, y, num);
            Player player = new Player(0, 0);
            Random random = new Random();
            Map miniMap = new Map(2 + (x / 20) * 3 + (x / 20 - 1), 2 + (y / 30) * 3 + (y / 30 - 1), num);

            List<Room> rooms = new List<Room>();

            for (int i = 0; i < x / 20; i++) {
                for (int j = 0; j < y / 30; j++) {
                    
                    int x0 = random.Next(20 * i + 1, 20 * (i + 1) - minsize);
                    int y0 = random.Next(30 * j + 1, 30 * (j + 1) - minsize);

                    int x1 = 0;
                    int y1 = 0;

                    while (x1 - x0 < minsize || y1 - y0 < minsize || x1 - x0 > maxsize || y1 - y0 > maxsize) {
                        x1 = random.Next(x0 + 1, 20 * (i + 1));
                        y1 = random.Next(y0 + 1, 30 * (j + 1));
                    }
                    
                    rooms.Add(new Room(x0, x1, y0, y1));

                    for (int a = x0; a < x1; a++) 
                    {
                        for (int b = y0; b < y1; b++) 
                        {
                            if (a == x0 || b == y0 || a == x1 - 1 || b == y1 - 1) {
                                Tile tmp = new Border(a, b);
                                dungeon.Tiles[a][b] = tmp;
                            }
                            else {
                                Tile tmp = new Field(a, b);
                                dungeon.Tiles[a][b] = tmp;
                            }
                            
                        }
                    }
                    for (int a = -1; a <= 1; a++) {
                        for (int b = -1; b <= 1; b++) {
                            miniMap.Tiles[2 + i * 3 + a][2 + j * 3 + b] = new Field(2 + i * 3 + a, 2 + j * 3 + b);
                        }
                    }

                }
            }

            for (int i = 0; i < rooms.Count - 1; i++) 
            {
                if ((i + 1) % (y / 30) != 0 && Math.Max(rooms[i].X0, rooms[i + 1].X0) < Math.Min(rooms[i].X1, rooms[i + 1].X1)) {
                    
                    if (Math.Max(rooms[i].X0, rooms[i + 1].X0) + 1 < Math.Min(rooms[i].X1, rooms[i + 1].X1) - 1) {
                        int coordinate = random.Next(Math.Max(rooms[i].X0, rooms[i + 1].X0) + 1, Math.Min(rooms[i].X1, rooms[i + 1].X1) - 1);

                        for (int a = rooms[i].Y1 - 1; a < rooms[i + 1].Y0 + 1; a++) {

                            if (a < rooms[i + 1].Y0) {
                                Tile tmp = new Field(coordinate, a);
                                dungeon.Tiles[coordinate][a] = tmp;

                                Tile tmp1 = new Border(coordinate - 1, a);
                                dungeon.Tiles[coordinate - 1][a] = tmp1;

                                Tile tmp2 = new Border(coordinate + 1, a);
                                dungeon.Tiles[coordinate + 1][a] = tmp2;
                            }
                            else {
                                Tile tmp = new Field(coordinate, a);
                                dungeon.Tiles[coordinate][a] = tmp;
                            }
                        }

                        miniMap.Tiles[2 + (i / (x / 20) * 4)][4 + (i / (x / 20) * 4)] = new Field(2 + (i / (x / 20) * 4), 4 + (i / (y / 30) * 4));
                    }
                }
                if (i < (x / 20) * (y / 30) - (y / 30) && Math.Max(rooms[i].Y0, rooms[i + (y / 30)].Y0) < Math.Min(rooms[i].Y1, rooms[i + (y / 30)].Y1)) {

                    if (Math.Max(rooms[i].Y0, rooms[i + (y / 30)].Y0) + 1 < Math.Min(rooms[i].Y1, rooms[i + (y / 30)].Y1) - 1) {
                        int coordinate = random.Next(Math.Max(rooms[i].Y0, rooms[i + (y / 30)].Y0) + 1, Math.Min(rooms[i].Y1, rooms[i + (y / 30)].Y1) - 1);

                        for (int a = rooms[i].X1 - 1; a < rooms[i + (y / 30)].X0 + 1; a++) {

                            if (a < rooms[i + (y / 30)].X0) {
                            
                                Tile tmp = new Field(a, coordinate);
                                dungeon.Tiles[a][coordinate] = tmp;
                                
                                Tile tmp1 = new Border(a, coordinate - 1);
                                dungeon.Tiles[a][coordinate - 1] = tmp1;

                                Tile tmp2 = new Border(a, coordinate + 1);
                                dungeon.Tiles[a][coordinate + 1] = tmp2;
                            }
                            else {
                                Tile tmp = new Field(a, coordinate);
                                dungeon.Tiles[a][coordinate] = tmp;
                            }

                            
                        }
                        miniMap.Tiles[4 + (i / (x / 20) * 4)][2 + (i / (x / 20) * 4)] = new Field(4 + (i / (x / 20) * 4), 2 + (i / (y / 30) * 4));
                    }

                }

            }
            
            int r = 0, coord;
            Room entrance;

            switch (from) {
                case From.Down:
                    r = random.Next(y / 30);

                    entrance = rooms[r];

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);


                    dungeon.Tiles[0][coord] = new Exit(0, coord, num - 1);
                                            
                    dungeon.Tiles[0][coord - 1] = new Border(0, coord - 1);

                    dungeon.Tiles[0][coord + 1] = new Border(0, coord + 1);
                    

                    for (int i = 1; i <= entrance.X0; i++) {    

                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                    }

                    miniMap.Tiles[0][2 + r * 4] = new Field(0, 2 + r * 4);
                    break;
                case From.Up:
                    r = random.Next((y / 30) * (x / 20) - (y / 30), (y / 30) * (x / 20));

                    entrance = rooms[r];

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);

                    dungeon.Tiles[x - 1][coord] = new Exit(x - 1, coord, num - 1);
                                            
                    dungeon.Tiles[x - 1][coord - 1] = new Border(x - 1, coord - 1);

                    dungeon.Tiles[x - 1][coord + 1] = new Border(x - 1, coord + 1);
                    

                    for (int i = entrance.X1 - 1; i < x - 1; i++) {
                        
                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);

                    }

                    miniMap.Tiles[miniMap.Height - 1][2 + r * 4] = new Field(miniMap.Height - 1, 2 + r * 4);
                    break;
                case From.Left:
                    r = random.Next(x / 20);

                    entrance = rooms[((y / 30) - 1) + r * (y / 30)];

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);

                    dungeon.Tiles[coord][y - 1] = new Exit(coord, y - 1, num - 1);
                                            
                    dungeon.Tiles[coord - 1][y - 1] = new Border(coord - 1, y - 1);

                    dungeon.Tiles[coord + 1][y - 1] = new Border(coord + 1, y - 1);

                    for (int i = entrance.Y1 - 1; i < y - 1; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    miniMap.Tiles[2 + r * 4][miniMap.Width - 1] = new Field(2 + r * 4, miniMap.Width - 1);
                    break;
                case From.Right:

                    r = random.Next(x / 20);

                    entrance = rooms[r * (y / 30)];

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);
                    
                    dungeon.Tiles[coord][0] = new Exit(coord, 0, num - 1);
                                            
                    dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                    dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);
                    

                    for (int i = 1; i <= entrance.Y0; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    miniMap.Tiles[2 + r * 4][0] = new Field(2 + r * 4, 0);
                    break;
            }

            int r1 = random.Next(rooms.Count);
            while (r1 == r) { r1 = random.Next(rooms.Count); }
            Room exit = rooms[r1];

            if ((r1 + 1) % (y/ 30) == 0 && random.Next(2) == 0) {

                coord = random.Next(exit.X0 + 1, exit.X1 - 1);

                dungeon.Tiles[coord][y - 1] = new Exit(coord, y - 1, num + 1);
                                            
                dungeon.Tiles[coord - 1][y - 1] = new Border(coord - 1, y - 1);

                dungeon.Tiles[coord + 1][y - 1] = new Border(coord + 1, y - 1);

                for (int i = exit.Y1 - 1; i < y - 1; i++) {
                        
                    dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                    dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                    dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);
                }

                miniMap.Tiles[2 + r1 * 4][miniMap.Width - 1] = new Field(2 + r1 * 4, miniMap.Width - 1);

            }
            else if (r1 % (y / 30) == 0 && random.Next(2) == 0) {

                coord = random.Next(exit.X0 + 1, exit.X1 - 1);

                dungeon.Tiles[coord][0] = new Exit(coord, 0, num + 1);
                                            
                dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);

                for (int i = 1; i <= exit.Y0; i++) {                

                    dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                    dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                    dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);
                }

                miniMap.Tiles[2 + r1 * 4][0] = new Field(2 + r1 * 4, 0);
            }
            else if (r1 < y / 30) {
                
                coord = random.Next(exit.Y0 + 1, exit.Y1 - 1);

                dungeon.Tiles[0][coord] = new Exit(0, coord, num + 1);
                                            
                dungeon.Tiles[0][coord - 1] = new Border(0, coord - 1);

                dungeon.Tiles[0][coord + 1] = new Border(0, coord + 1);

                for (int i = 1; i <= exit.X0; i++) {    

                    dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                    dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                    dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                }

                miniMap.Tiles[0][2 + r1 * 4] = new Field(0, 2 + r1 * 4);

            }
            else if (r1 >= (y / 30) * (x / 20) - (y / 30)) {

                coord = random.Next(exit.Y0 + 1, exit.Y1 - 1);

                dungeon.Tiles[x - 1][coord] = new Exit(x - 1, coord, num + 1);
                                            
                dungeon.Tiles[x - 1][coord - 1] = new Border(x - 1, coord - 1);

                dungeon.Tiles[x - 1][coord + 1] = new Border(x - 1, coord + 1);

                for (int i = exit.X1 - 1; i < x - 1; i++) {
                        
                    dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                    dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                    dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                }

                miniMap.Tiles[miniMap.Height - 1][2 + r1 * 4] = new Field(miniMap.Height - 1, 2 + r1 * 4);
            }



            if (Primitives.AStarSearch(dungeon, new List<Entity>(), new Player(0, 0), (rooms[0].X0 + 2, rooms[0].Y0 + 2), (rooms[rooms.Count - 1].X1 - 2, rooms[rooms.Count - 1].Y1 - 2)).Count == 0)
                return GenerateDungeon(x, y, from, num, minsize, maxsize);
            
            dungeon.MiniMap = miniMap;
            return dungeon;

            


    
        }


    }

}