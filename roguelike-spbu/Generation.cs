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
        public static Map GenerateDungeon(int x, int y, From from = From.Down, int num = 0, int roomX = 20, int roomY = 30) {

            Map dungeon = new Map(x, y, num);
            Player player = new Player(0, 0);
            Random random = new Random();
            //Map miniMap = new Map(2 + (x / roomX) * 3 + (x / roomX - 1), 2 + (y / roomY) * 3 + (y / roomY - 1), num);

            List<Room> rooms = new List<Room>();

            for (int i = 0; i < x / roomX; i++) {
                for (int j = 0; j < y / roomY; j++) {
                    
                    int x0 = random.Next(20 * i + 1, 20 * (i + 1) - 12);
                    int y0 = random.Next(30 * j + 1, 30 * (j + 1) - 17);

                    int x1 = 0;
                    int y1 = 0;

                    while (x1 - x0 < 12 || y1 - y0 < 17 || x1 - x0 > 12 || y1 - y0 > 17) {
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
                            //miniMap.Tiles[2 + i * 4 + a][2 + j * 4 + b] = new Field(2 + i * 4 + a, 2 + j * 4 + b);
                        }
                    }

                }
            }

            
            dungeon = ConnectRooms(x, y, dungeon, from, rooms, num, roomX, roomY);


            if (Primitives.AStarSearch(dungeon, new List<Entity>(), new Player(0, 0), (rooms[0].X0 + 2, rooms[0].Y0 + 2), (rooms[rooms.Count - 1].X1 - 2, rooms[rooms.Count - 1].Y1 - 2)).Count == 0)
                return GenerateDungeon(x, y, from, num, roomX, roomY);
            
            //dungeon.MiniMap = miniMap;
            return dungeon;
        }

        public static Map GenerateBossRoom(int x, int y, From from = From.Left, int num = 0) {

            Map dungeon = new Map(x, y, num);
            Random random = new Random();
            int x0, y0, x1, y1;
            
            do {
                x0  = random.Next(x / 2);
                y0 = random.Next(y / 3);
                x1 = random.Next(x0 + 1, x);
                y1 = random.Next(y - y / 3, y);

            } while (x1 - x0 <  25 && y1 - y0 < 60);
            
            x0 = 10;
            x1 = 30;
            y0 = 50;
            y1 = 120;

            Room entrance = new Room(x0, x1, y0, y1);

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

            int coord;
            From real = from;
            
            switch (from) {

                case From.Down:

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);


                    dungeon.Tiles[0][coord] = new Exit(0, coord, num - 1);
                                            
                    dungeon.Tiles[0][coord - 1] = new Border(0, coord - 1);

                    dungeon.Tiles[0][coord + 1] = new Border(0, coord + 1);
                    

                    for (int i = 1; i <= entrance.X0; i++) {    

                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                    }

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);

                    dungeon.Tiles[x - 1][coord] = new Exit(x - 1, coord, num + 1);
                                            
                    dungeon.Tiles[x - 1][coord - 1] = new Border(x - 1, coord - 1);

                    dungeon.Tiles[x - 1][coord + 1] = new Border(x - 1, coord + 1);
                    
                    dungeon.Tiles[entrance.X1 - 1][coord] = new Door(entrance.X1 - 1, coord);
                                            
                    dungeon.Tiles[entrance.X1 - 1][coord - 1] = new Border(entrance.X1 - 1, coord - 1);

                    dungeon.Tiles[entrance.X1 - 1][coord + 1] = new Border(entrance.X1 - 1, coord + 1);

                    for (int i = entrance.X1; i < x - 1; i++) {
                        
                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);

                    }

                    //miniMap.Tiles[0][2 + r % (y / roomY) * 4] = new Field(0, 2 + r % (y / roomY) * 4);
                    break;
                case From.Up:

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);

                    dungeon.Tiles[x - 1][coord] = new Exit(x - 1, coord, num - 1);
                                            
                    dungeon.Tiles[x - 1][coord - 1] = new Border(x - 1, coord - 1);

                    dungeon.Tiles[x - 1][coord + 1] = new Border(x - 1, coord + 1);
                    

                    for (int i = entrance.X1 - 1; i < x - 1; i++) {
                        
                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);

                    }

                    coord = random.Next(entrance.Y0 + 1, entrance.Y1 - 1);


                    dungeon.Tiles[0][coord] = new Exit(0, coord, num + 1);
                                            
                    dungeon.Tiles[0][coord - 1] = new Border(0, coord - 1);

                    dungeon.Tiles[0][coord + 1] = new Border(0, coord + 1);

                    dungeon.Tiles[entrance.X0][coord] = new Door(entrance.X0, coord);
                                            
                    dungeon.Tiles[entrance.X0][coord - 1] = new Border(entrance.X0, coord - 1);

                    dungeon.Tiles[entrance.X0][coord + 1] = new Border(entrance.X0, coord + 1);

                    
                    

                    for (int i = 1; i < entrance.X0; i++) {    

                        dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                        dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                        dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                    }

                    //miniMap.Tiles[//miniMap.Height - 1][2 + r % (y / roomY)  * 4] = new Field(//miniMap.Height - 1, 2 + r % (y / roomY)  * 4);
                    break;
                case From.Left:

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);

                    dungeon.Tiles[coord][y - 1] = new Exit(coord, y - 1, num - 1);
                                            
                    dungeon.Tiles[coord - 1][y - 1] = new Border(coord - 1, y - 1);

                    dungeon.Tiles[coord + 1][y - 1] = new Border(coord + 1, y - 1);

                    for (int i = entrance.Y1 - 1; i < y - 1; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);
                    
                    dungeon.Tiles[coord][0] = new Exit(coord, 0, num + 1);
                                            
                    dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                    dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);

                    dungeon.Tiles[coord][entrance.Y0] = new Door(coord, entrance.Y0);
                                            
                    dungeon.Tiles[coord - 1][entrance.Y0] = new Border(coord - 1, entrance.Y0);

                    dungeon.Tiles[coord + 1][entrance.Y0] = new Border(coord + 1, entrance.Y0);
                    

                    for (int i = 1; i < entrance.Y0; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    //miniMap.Tiles[2 + r / (y / roomY) * 4][//miniMap.Width - 1] = new Field(2 + r / (y / roomY) * 4, //miniMap.Width - 1);
                    break;
                case From.Right:

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);
                    
                    dungeon.Tiles[coord][0] = new Exit(coord, 0, num - 1);
                                            
                    dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                    dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);
                    

                    for (int i = 1; i <= entrance.Y0; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);

                    dungeon.Tiles[coord][y - 1] = new Exit(coord, y - 1, num + 1);
                                            
                    dungeon.Tiles[coord - 1][y - 1] = new Border(coord - 1, y - 1);

                    dungeon.Tiles[coord + 1][y - 1] = new Border(coord + 1, y - 1);

                    dungeon.Tiles[coord][entrance.Y1 - 1] = new Door(coord, entrance.Y1 - 1);
                                            
                    dungeon.Tiles[coord - 1][entrance.Y1 - 1] = new Border(coord - 1, entrance.Y1 - 1);

                    dungeon.Tiles[coord + 1][entrance.Y1 - 1] = new Border(coord + 1, entrance.Y1 - 1);

                    for (int i = entrance.Y1; i < y - 1; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    //miniMap.Tiles[2 + r / (y / roomY) * 4][0] = new Field(2 + r / (y / roomY) * 4, 0);
                    break;
            }




            return dungeon;

        }
         public static Map GenerateHub() {

            Map hub = new Map(40, 150, -1);
            Random random = new Random();
            //start pos (20 80)

            for (int i = 0; i < 21; i++)
            {
                hub.Tiles[10][70+i] = new Mountain (10, 70+i);
            }
            int k = 2;
            for (int i = 1; i < 6; i++)
            {
                hub.Tiles[10+i][72-k] = new Mountain (10+i, 72-k);
                hub.Tiles[10+i][71-k] = new Mountain (10+i, 71-k);
                hub.Tiles[10+i][70-k] = new Mountain (10+i, 70-k);
                hub.Tiles[10+i][91+k] = new Mountain (10+i, 91+k);
                hub.Tiles[10+i][90+k] = new Mountain (10+i, 90+k);
                hub.Tiles[10+i][89+k] = new Mountain (10+i, 89+k);
                k+=2;
            }
            hub.Tiles[11][71] = new House (11, 71);
            hub.Tiles[11][72] = new Tree (11, 72);
            hub.Tiles[12][71] = new House (12, 71);
            hub.Tiles[12][70] = new House (12, 70);
            hub.Tiles[12][69] = new House (12, 69);
            for (int i = 0; i < 7; i++)
            {
                hub.Tiles[16+i][60] = new Mountain (16+i, 60);
                hub.Tiles[16+i][61] = new Mountain (16+i, 61);
                hub.Tiles[16+i][62] = new Mountain (16+i, 62);
                hub.Tiles[16+i][63] = new Tree (16+i, 63);
            }
            for (int i = 0; i < 103-64; i++)
            {
                hub.Tiles[20][64+i] = new Road (20, 64+i);
            }
            for (int i = 0; i < 12; i++)
            {
                hub.Tiles[12+i][70] = new Road (12+i, 70);
                hub.Tiles[13+i][80] = new Road (13+i,  80);
            }
            hub.Tiles[23][62] = new Mountain (23, 62);
            for (int i = 0; i < 7; i++)
            {
                hub.Tiles[23][63+i] = new Tree (23, 63+i);
                hub.Tiles[24][63+i] = new Mountain (24, 63+i);
                hub.Tiles[25][68+i] = new Mountain (25, 68+i);
                hub.Tiles[26][72+i] = new Mountain (26, 72+i);
            }
            for (int i = 0; i < 82-69; i++)
            {
                hub.Tiles[27][69+i] = new Mountain (27, 69+i);
            }
            for (int i = 0; i < 2; i++)
            {
                hub.Tiles[23][84+i] = new Tree (23, 84+i);
                hub.Tiles[24][83+i] = new Mountain (24, 83+i);
                hub.Tiles[25][82+i] = new Mountain (25, 82+i);
                hub.Tiles[26][81+i] = new Mountain (26, 81+i);
            }
            for (int i = 0; i < 4; i++)
            {
                hub.Tiles[22][85+i] = new Mountain (22, 85+i);   
            }
            for (int i = 0; i < 6; i++)
            {
                hub.Tiles[21][88+i] = new Mountain (21, 88+i);   
            }
            hub.Tiles[20][103] = new Exit (20, 103, 0);
            for (int i = 0; i < 4; i++)
            {
                hub.Tiles[16+i][101] = new Mountain (16+i, 101);   
            }
            hub.Tiles[19][102] = new Mountain (19, 102);
            hub.Tiles[19][103] = new Mountain (19, 103);
            for (int i = 0; i < 3; i++)
            {
                hub.Tiles[21][77+i] = new House (21, 77+i);   
            }
            for (int i = 0; i < 3; i++)
            {
                hub.Tiles[21][64+i] = new House (21, 64+i);   
            }
            for (int i = 0; i < 3; i++)
            {
                hub.Tiles[19][62+i] = new House (19, 62+i);   
            }
             for (int i = 0; i < 3; i++)
            {
                hub.Tiles[19][81+i] = new House (19, 81+i);   
            }
            hub.Tiles[21][71] = new TownHall (21, 71); 
            hub.Tiles[19][69] = new Shop (19, 69);
            hub.Tiles[19][69] = new Alchemy (19, 69);
            for (int i = 0; i < 16; i++)
            {
                bool flag = false;
                bool flag2 = false;

                k = 0;
                while(true)
                {
                    if (hub.Tiles[10+i][60+k].Symbol == "▲")
                    {
                        flag = true;
                    }
                    if (flag && hub.Tiles[10+i][60+k].Symbol != "▲")
                    {
                        flag2 = true;

                    }
                    if(flag2 &&  hub.Tiles[10+i][60+k].Symbol == "▲")
                    {
                        continue;
                    }
                    if (flag2 && hub.Tiles[10+i][60+k].Symbol == " ")
                    {
                        hub.Tiles[10+i][60+k] = new HubField (10+i, 60+k);
                    }
                }
            }
            return hub;
        }

        static Map ConnectRooms(int x, int y, Map dungeon, /* Map /miniMap,*/ From from, List<Room> rooms, int num, int roomX, int roomY) {

            Random random = new Random();
            for (int i = 0; i < rooms.Count - 1; i++) 
            {
                if ((i + 1) % (y / roomY) != 0 && Math.Max(rooms[i].X0, rooms[i + 1].X0) < Math.Min(rooms[i].X1, rooms[i + 1].X1)) {
                    
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
                        
                        //miniMap.Tiles[2 + (i / (y / roomY) * 4)][4 + (i % (y / roomY) * 4)] = new Field(2 + (i / (y / roomY) * 4), 4 + (i % (y / roomY) * 4));
                        
                    }
                }
                if (i < (x / roomX) * (y / roomY) - (y / roomY) && Math.Max(rooms[i].Y0, rooms[i + (y / roomY)].Y0) < Math.Min(rooms[i].Y1, rooms[i + (y / roomY)].Y1)) {

                    if (Math.Max(rooms[i].Y0, rooms[i + (y / roomY)].Y0) + 1 < Math.Min(rooms[i].Y1, rooms[i + (y / roomY)].Y1) - 1) {
                        int coordinate = random.Next(Math.Max(rooms[i].Y0, rooms[i + (y / roomY)].Y0) + 1, Math.Min(rooms[i].Y1, rooms[i + (y / roomY)].Y1) - 1);

                        for (int a = rooms[i].X1 - 1; a < rooms[i + (y / roomY)].X0 + 1; a++) {

                            if (a < rooms[i + (y / roomY)].X0) {
                            
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

                        //miniMap.Tiles[4 + (i / (y / roomY) * 4)][2 + (i % (y / roomY) * 4)] = new Field(4 + (i / (y / roomY) * 4), 2 + (i % (y / roomY) * 4));
                    }

                }

            }

            int r = 0, coord;
            Room entrance;

            switch (from) {
                case From.Down:
                    r = random.Next(y / roomY);

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

                    //miniMap.Tiles[0][2 + r % (y / roomY) * 4] = new Field(0, 2 + r % (y / roomY) * 4);
                    break;
                case From.Up:
                    r = random.Next((y / roomY) * (x / roomX) - (y / roomY), (y / roomY) * (x / roomX));

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

                    //miniMap.Tiles[//miniMap.Height - 1][2 + r % (y / roomY)  * 4] = new Field(//miniMap.Height - 1, 2 + r % (y / roomY)  * 4);
                    break;
                case From.Left:
                    r = random.Next(x / roomX);

                    entrance = rooms[((y / roomY) - 1) + r * (y / roomY)];

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);

                    dungeon.Tiles[coord][y - 1] = new Exit(coord, y - 1, num - 1);
                                            
                    dungeon.Tiles[coord - 1][y - 1] = new Border(coord - 1, y - 1);

                    dungeon.Tiles[coord + 1][y - 1] = new Border(coord + 1, y - 1);

                    for (int i = entrance.Y1 - 1; i < y - 1; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    //miniMap.Tiles[2 + r / (y / roomY) * 4][//miniMap.Width - 1] = new Field(2 + r / (y / roomY) * 4, //miniMap.Width - 1);
                    break;
                case From.Right:

                    r = random.Next(x / roomX);

                    entrance = rooms[r * (y / roomY)];

                    coord = random.Next(entrance.X0 + 1, entrance.X1 - 1);
                    
                    dungeon.Tiles[coord][0] = new Exit(coord, 0, num - 1);
                                            
                    dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                    dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);
                    

                    for (int i = 1; i <= entrance.Y0; i++) {
                        

                        dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                        dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                        dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);

                    }

                    //miniMap.Tiles[2 + r / (y / roomY) * 4][0] = new Field(2 + r / (y / roomY) * 4, 0);
                    break;
            }
            int r1;

            r1 = random.Next(rooms.Count);
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

                //miniMap.Tiles[2 + r1  / (y / roomY) * 4][//miniMap.Width - 1] = new Field(2 + r1 / (y / roomY) * 4, //miniMap.Width - 1);

            }
            else if (r1 % (y / roomY) == 0 && random.Next(2) == 0) {

                coord = random.Next(exit.X0 + 1, exit.X1 - 1);

                dungeon.Tiles[coord][0] = new Exit(coord, 0, num + 1);
                                            
                dungeon.Tiles[coord - 1][0] = new Border(coord - 1, 0);

                dungeon.Tiles[coord + 1][0] = new Border(coord + 1, 0);

                for (int i = 1; i <= exit.Y0; i++) {                

                    dungeon.Tiles[coord][i] = new Field(coord, i);
                                        
                    dungeon.Tiles[coord - 1][i] = new Border(coord - 1, i);

                    dungeon.Tiles[coord + 1][i] = new Border(coord + 1, i);
                }

                //miniMap.Tiles[2 + r1 / (y / roomY) * 4][0] = new Field(2 + r1 / (y / roomY) * 4, 0);
            }
            else if (r1 < y / roomY) {
                
                coord = random.Next(exit.Y0 + 1, exit.Y1 - 1);

                dungeon.Tiles[0][coord] = new Exit(0, coord, num + 1);
                                            
                dungeon.Tiles[0][coord - 1] = new Border(0, coord - 1);

                dungeon.Tiles[0][coord + 1] = new Border(0, coord + 1);

                for (int i = 1; i <= exit.X0; i++) {    

                    dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                    dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                    dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                }

                //miniMap.Tiles[0][2 + r1 % (y / roomY) * 4] = new Field(0, 2 + r1 % (y / roomY) * 4);

            }
            else if (r1 >= (y / roomY) * (x / roomX) - (y / roomY)) {

                coord = random.Next(exit.Y0 + 1, exit.Y1 - 1);

                dungeon.Tiles[x - 1][coord] = new Exit(x - 1, coord, num + 1);
                                            
                dungeon.Tiles[x - 1][coord - 1] = new Border(x - 1, coord - 1);

                dungeon.Tiles[x - 1][coord + 1] = new Border(x - 1, coord + 1);

                for (int i = exit.X1 - 1; i < x - 1; i++) {
                        
                    dungeon.Tiles[i][coord] = new Field(i, coord);
                                        
                    dungeon.Tiles[i][coord - 1] = new Border(i, coord - 1);

                    dungeon.Tiles[i][coord + 1] = new Border(i, coord + 1);
                }

                //miniMap.Tiles[//miniMap.Height - 1][2 + r1 % (y / roomY) * 4] = new Field(//miniMap.Height - 1, 2 + r1 % (y / roomY) * 4);
            }

            return dungeon;
        }
    

    }

}