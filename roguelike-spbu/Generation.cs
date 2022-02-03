using System;

namespace roguelike_spbu {
    
    public class Generation {

        public static char[][] Generate(int x, int y, (int, int) startingPosition, (int, int) endingPosition) {

            char[][] dungeon = new char[x][];
            Player player = new Player(startingPosition.Item1, startingPosition.Item2);
            Random random = new Random();
            int freeSpace = 1;

            for (int i = 0; i < x; i++) {
                
                dungeon[i] = new char[y];

                for (int j = 0; j < y; j++) {
                    dungeon[i][j] = '#';
                }
            }

            
            while (player.X != endingPosition.Item1 || player.Y != endingPosition.Item2)
            {       
                    if (dungeon[player.X][player.Y] == '#') {
                        freeSpace += 1;
                    }
                    dungeon[player.X][player.Y] = '.';
                    
                    int i = random.Next(x + y);

                    if (i >= 0 && i <= x - 1) {
                        player.X += random.Next(3) - 1;
                        if (player.X < 0) {
                            player.X += 1;
                        }
                        else if (player.X > x - 1) {
                            player.X -= 1;
                        }
                    }
                    else if (i >= x && i <= x + y) {
                        player.Y += random.Next(3) - 1;
                        if (player.Y < 0) {
                            player.Y += 1;
                        }
                        else if (player.Y > y - 1) {
                            player.Y -= 1;
                        }
                    }

            }

            dungeon[player.X][player.Y] = '.';

            if (freeSpace * 2  > x * y) {

                return Generate(x, y, startingPosition, endingPosition);
            }
            else {

                return dungeon;
            }
            
        }
    }
}