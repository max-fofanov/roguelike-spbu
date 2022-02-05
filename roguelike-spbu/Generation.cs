using System;

namespace roguelike_spbu {
    
    public class Generation {



        public Generation() {

        }

        public static Map GenerateCave(int x, int y, (int, int) startingPosition, (int, int) endingPosition) {

            Map dungeon = new Map(x, y);
            Player player = new Player(startingPosition.Item1, startingPosition.Item2);
            Random random = new Random();
            int freeSpace = 1;

            while (player.X != endingPosition.Item1 || player.Y != endingPosition.Item2)
            {
                if (dungeon.Tiles[player.X][player.Y] is Border)
                {
                    freeSpace += 1;
                }
                dungeon.Tiles[player.X][player.Y] = new Field();

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

            dungeon.Tiles[player.X][player.Y] = new Field();

            if (freeSpace * 2 > x * y)
            {
                return GenerateCave(x, y, startingPosition, endingPosition);
            }
            else
            {
                return dungeon;
            }

        }

        public static char[][] GenerateDungeon(int x, int y) {

            char[][] dungeon = new char[x][];
            Player player = new Player(0, 0);
            Random random = new Random();

            List<Room> rooms = new List<Room>();
            

            for (int i = 0; i < x; i++) {
                
                dungeon[i] = new char[y];

                for (int j = 0; j < y; j++) {
                    dungeon[i][j] = '#';
                }
            }

            for (int i = 0; i < 15; i++) {

                bool flag = false;
                int x0 = random.Next(x - 15);
                int y0 = random.Next(y - 30);
                int x1, y1;

                do {
                    x1 = x0 + random.Next(7, 15);
                    y1 = y0 + random.Next(20, 30);
                } while (x1 > x - 1 || y1 > y - 1);

                foreach (Room room in rooms) {

                    if (x0 >= room.X0 && x1 <= room.X0 && y0 >= room.Y0 && y1 <= room.Y1) 
                    {
                        flag = true;
                        break;
                    }
                    else if (x0 <= room.X0 && x1 >= room.X0 && y0 <= room.Y0 && y1 >= room.Y1) 
                    {
                        flag = true;
                        break;
                    }
                    else if (x0 >= room.X0 && x0 <= room.X1 && room.Y0 >= y0 && room.Y0 <= y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (x0 >= room.X0 && x0 <= room.X1 && room.Y1 >= y0 && room.Y1 <= y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (x1 >= room.X0 && x1 <= room.X1 && room.Y0 >= y0 && room.Y0 <= y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (x1 >= room.X0 && x1 <= room.X1 && room.Y1 >= y0 && room.Y1 <= y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (room.X0 >= x0 && room.X0 <= x1 && y0 >= room.Y0 && y0 <= room.Y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (room.X0 >= x0 && room.X0 <= x1 && y1 >= room.Y0 && y1 <= room.Y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (room.X1 >= x0 && room.X1 <= x1 && y0 >= room.Y0 && y0 <= room.Y1)
                    {
                        flag = true;
                        break;
                    }
                    else if (room.X1 >= x0 && room.X1 <= x1 && y1 >= room.Y0 && y1 <= room.Y1)
                    {
                        flag = true;
                        break;
                    }

                    
                }

                if (flag) { i -= 1; continue; }
                else { rooms.Add(new Room(x0, x1, y0, y1)); }

                for (int a = x0; a < x1; a++) {
                    for (int b = y0; b < y1; b++) {
                        dungeon[a][b] = '.';
                    }
                }
            }

            return dungeon;
        }


    }
}
/*
                    if (x0 >= room.X0 && y0 >= room.Y0 && x0 <= room.X1 && y0 <= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x1 >= room.X0 && y1 >= room.Y0 && x1 <= room.X1 && y1 <= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x0 >= room.X0 && y1 >= room.Y0 && x0 <= room.X1 && y1 <= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x1 >= room.X0 && y0 >= room.Y0 && x1 <= room.X1 && y0 <= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x0 <= room.X0 && y0 <= room.Y0 && x0 >= room.X1 && y0 >= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x1 <= room.X0 && y1 <= room.Y0 && x1 >= room.X1 && y1 >= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x0 <= room.X0 && y1 <= room.Y0 && x0 >= room.X1 && y1 >= room.Y1) {
                        flag = true;
                        break;
                    }
                    else if (x1 <= room.X0 && y0 <= room.Y0 && x1 >= room.X1 && y0 >= room.Y1) {
                        flag = true;
                        break;
                    }
*/