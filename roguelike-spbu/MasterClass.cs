namespace roguelike_spbu
{
    public static class GameInfo
    {
        public static Player player = new Player(0, 0);
        public static List<Entity> entities = new List<Entity>();
        public static List<Map> history = new List<Map>();
        public static int currentMap = 0;
        public static bool allVisible = false;
        public static bool isMusic = true;
    }
    public class Game
    {
        public GUI gui = new GUI();
        public Engine engine;
        public void FullTurn()
        {

        }
    }
}