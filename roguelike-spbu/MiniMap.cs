
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
        public (int, int) GetMiniCoordinates() {
            int num = GameInfo.player.X % (GameInfo.mapHeight / 20) * (GameInfo.mapWidth / 30) + GameInfo.player.Y % (GameInfo.mapWidth / 30);

            return (2 + num / (GameInfo.mapWidth / 30) * 4 , 2 + num % (GameInfo.mapWidth / 30) * 4);
        }

    }

}