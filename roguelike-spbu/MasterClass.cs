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
        public static int mapHeight = 45;
        public static int mapWidth = 180;
    }
    public static class SystemInfo
    {
        public static int RenderHeight = 40;
        public static int RenderWidth = 150;
        public static GUI gui = new GUI();
        public static Engine engine = new Engine();
    }

    public class Game
    {
        GUI gui {
            get { return SystemInfo.gui; }
            set { SystemInfo.gui = value; }
        }
        Engine engine {
            get { return SystemInfo.engine; }
            set { SystemInfo.engine = value; }
        }
        public Game()
        {
            // engine.GetEntitiesInRange();
            Walkman.Play();
            new Renderer(SystemInfo.RenderHeight, SystemInfo.RenderWidth, 20, 20);
            
            engine.GenerateMap(GameInfo.player, Generation.From.Left, true);
            engine.Turn(true);

            
            gui.Print();
        }
        public void FullTurn()
        {
            engine.Turn();
            
            gui.Print();
        }
    }
}