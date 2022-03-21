namespace roguelike_spbu
{
    public static class GameInfo
    {
        public static Player player = new Player(0, 0);
        public static List<Entity> entities = new List<Entity>();
        public static List<Map> history = new List<Map>();
        public static int currentMap = 0;
        public static bool allVisible = true;
        public static bool isMusic = true;
        public static int mapHeight = 45;
        public static int mapWidth = 180;
    }
    public class Game
    {
        public GUI gui = new GUI();
        public Engine engine = new Engine();
        public Game()
        {
            new Renderer(40, 150, 20, 20);
            
            engine.GenerateMap(GameInfo.player, Generation.From.Down, true);
            engine.Turn(true);

            Console.SetCursorPosition(0, 0);
            gui.Print();
        }
        public void FullTurn()
        {
            Console.SetCursorPosition(0, 0);
            engine.Turn();
            gui.Print();
        }
    }
}