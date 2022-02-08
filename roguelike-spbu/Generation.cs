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

        public static Map GenerateDungeon(int x, int y) {

            Map dungeon = new Map(x, y);
            Player player = new Player(0, 0);
            Random random = new Random();

            List<Room> rooms = new List<Room>();

            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 5; j++) {
                    int res = Walker.Alias(new float[] { 20, 5 } );
                    
                    int x0 = random.Next(25 * i + 1, 25 * (i + 1) - 10);
                    int y0 = random.Next(30 * j + 1, 30 * (j + 1) - 10);

                    int x1 = 0;
                    int y1 = 0;

                    while (x1 - x0 < 10 || y1 - y0 < 10 || x1 - x0 > 20 || y1 - y0 > 20) {
                        x1 = random.Next(x0 + 1, 25 * (i + 1));
                        y1 = random.Next(y0 + 1, 30 * (j + 1));
                    }
                    
                    rooms.Add(new Room(x0, x1, y0, y1));

                    for (int a = x0; a < x1; a++) 
                    {
                        for (int b = y0; b < y1; b++) 
                        {
                            Tile tmp = new Field();
                            tmp.X = a;
                            tmp.Y = b;
                            dungeon.Tiles[a][b] = tmp;
                        }
                    }

                }
            }

            for (int i = 0; i < rooms.Count; i++) 
            {
                if ((i + 1) % 5 != 0 && Math.Max(rooms[i].X0, rooms[i + 1].X0) < Math.Min(rooms[i].X1, rooms[i + 1].X1)) {
                    
                    int coordinate = random.Next(Math.Max(rooms[i].X0, rooms[i + 1].X0), Math.Min(rooms[i].X1, rooms[i + 1].X1));

                    for (int a = rooms[i].Y1; a < rooms[i + 1].Y0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = coordinate;
                        tmp.Y = a;
                        dungeon.Tiles[coordinate][a] = tmp;
                        
                    }
                }
                if (i < 24 && Math.Max(rooms[i].Y0, rooms[i + 5].Y0) < Math.Min(rooms[i].Y1, rooms[i + 5].Y1)) {
                    int coordinate = new Random().Next(Math.Max(rooms[i].Y0, rooms[i + 5].Y0), Math.Min(rooms[i].Y1, rooms[i + 5].Y1));

                    for (int a = rooms[i].X1; a < rooms[i + 5].X0 + 1; a++) {

                        Tile tmp = new Field();
                        tmp.X = a;
                        tmp.Y = coordinate;
                        dungeon.Tiles[a][coordinate] = tmp;
                        
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