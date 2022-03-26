
namespace roguelike_spbu {
    public class MiniMap {
        
        public Map miniMap;
        public int prev = -1;

        public MiniMap() {
            miniMap = new Map(GameInfo.history[GameInfo.currentMap].Height / 2, GameInfo.history[GameInfo.currentMap].Width / 2, -2);
            for (int i = 0; i < GameInfo.history[GameInfo.currentMap].Height; i++) {
                if (i % 2 == 0) {
                    for (int j = 0; j < GameInfo.history[GameInfo.currentMap].Width; j++) {
                        if (j % 2 == 0) {
                            miniMap.Tiles[i / 2][j / 2] = GameInfo.history[GameInfo.currentMap].Tiles[i][j];
                        }
                    }
                }
            }      
        }

        public void Update() {
            if (prev != -1) {
                for (int i = -1; i <= 1; i++) {
                    for (int j = -1; j <= 1; j++) {
                        miniMap.Tiles[2 + prev / (GameInfo.mapWidth / 30) * 4 + i][2 + prev % (GameInfo.mapWidth / 30) * 4 + j].PrimaryBackgroundColor = System.Drawing.Color.DarkGreen;
                        miniMap.Tiles[2 + prev / (GameInfo.mapWidth / 30) * 4 + i][2 + prev % (GameInfo.mapWidth / 30) * 4 + j].PrimaryForegroundColor = System.Drawing.Color.Green;
                    }
                }
            }
            int num = GameInfo.player.X % (GameInfo.mapHeight / 20) * (GameInfo.mapWidth / 30) + GameInfo.player.Y % (GameInfo.mapWidth / 30);
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    miniMap.Tiles[2 + num / (GameInfo.mapWidth / 30) * 4 + i][2 + num % (GameInfo.mapWidth / 30) * 4 + j].PrimaryBackgroundColor = System.Drawing.Color.DarkRed;
                    miniMap.Tiles[2 + num / (GameInfo.mapWidth / 30) * 4 + i][2 + num % (GameInfo.mapWidth / 30) * 4 + j].PrimaryForegroundColor = System.Drawing.Color.Red;
                }
            }

        }

    }

}