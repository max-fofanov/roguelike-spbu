
namespace roguelike_spbu {
    public class MiniMap {
        
        public Map miniMap;

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

    }

}