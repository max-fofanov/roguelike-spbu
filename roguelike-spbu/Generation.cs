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

        public static Map GenerateDungeon(int x, int y, int minsize = 10, int maxsize = 20) {

            Map dungeon = new Map(x, y);
            Player player = new Player(0, 0);
            Random random = new Random();

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
                    }
                }
            }

            
            
            /*
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
                        Tile tmp = new Field();
                        tmp.X = a;
                        tmp.Y = b;
                        dungeon.Tiles[a][b] = tmp;
                    }
                }

            }

            rooms.Sort(new RoomSorter());
            
            for (int i = 0; i < rooms.Count - 1; i++) 
            {
                if (rooms[i + 1].Y0 > rooms[i].Y0 && rooms[i + 1].Y0 < rooms[i].Y1 && rooms[i].X0 < rooms[i + 1].X0) 
                {
                    int coordinate = new Random().Next(rooms[i + 1].Y0, rooms[i].Y1);

                    for (int a = rooms[i].X1; a < rooms[i + 1].X0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = a;
                        tmp.Y = coordinate;
                        dungeon.Tiles[a][coordinate] = tmp;
                        
                    }
                }
                else if (rooms[i + 1].Y0 > rooms[i].Y0 && rooms[i + 1].Y0 < rooms[i].Y1 && rooms[i].X0 > rooms[i + 1].X0) 
                {
                    int coordinate = new Random().Next(rooms[i + 1].Y0, rooms[i].Y1);

                    for (int a = rooms[i + 1].X1; a < rooms[i].X0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = a;
                        tmp.Y = coordinate;
                        dungeon.Tiles[a][coordinate] = tmp;
                        
                    }
                }

                else if (rooms[i + 1].X0 > rooms[i].X0 && rooms[i + 1].X0 < rooms[i].X1 && rooms[i].Y0 < rooms[i + 1].Y0) 
                {
                    int coordinate = new Random().Next(rooms[i + 1].X0, rooms[i].X1);

                    for (int a = rooms[i].Y1; a < rooms[i + 1].Y0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = coordinate;
                        tmp.Y = a;
                        dungeon.Tiles[coordinate][a] = tmp;
                        
                    }
                }
                else if (rooms[i + 1].X0 > rooms[i].X0 && rooms[i + 1].X0 < rooms[i].X1 && rooms[i].Y0 > rooms[i + 1].Y0) 
                {
                    int coordinate = new Random().Next(rooms[i + 1].X0, rooms[i].X1);

                    for (int a = rooms[i + 1].Y1; a < rooms[i].Y0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = coordinate;
                        tmp.Y = a;
                        dungeon.Tiles[coordinate][a] = tmp;
                        
                    }
                }

            }
            */
            return dungeon;
        }


    }

    class RoomSorter : IComparer<Room> {
        
        public int Compare(Room? room1, Room? room2) {

            if (room1.Y0 > room2.Y0) 
            {
                return 1;
            }
            else if (room1.Y0 < room2.Y0) 
            {
                return -1;
            }
            else if (room1.X0 < room2.X0) 
            {
                return 1;
            }
            else 
            {
                return -1;
            }

        }
        
    }
}