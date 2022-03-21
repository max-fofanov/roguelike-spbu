namespace roguelike_spbu
{
    public class Engine
    {

        public Map map {
            get {
                if (GameInfo.history.Count() == 0) return new Map(0, 0, 0);
                return GameInfo.history[GameInfo.currentMap];
            }
            set {
                GameInfo.history[GameInfo.currentMap] = value;
            }
        }
        public Player player {
            get {
                return GameInfo.player;
            }

            set {
                GameInfo.player = value;
            }
        }
        public List<Entity> entities {
            get {
                return GameInfo.entities;
            }

            set {
                GameInfo.entities = value;
            }
        }

        public List<Map> history {
            get {
                return GameInfo.history;
            }

            set {
                GameInfo.history = value;
            }
        }
        List<(int, int)> visiblePoints = new List<(int, int)>();
        public bool allVisible {
            get {
                return GameInfo.allVisible;
            }

            set {
                GameInfo.allVisible = value;
            }
        }
        public Engine() { }
        bool IsNewPlaceOK(int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.Height || y >= map.Width)
                return false;

            if (map.Tiles[x][y].Impassable)
                return false;

            if (entities.Where(e => e.X == x && e.Y == y).Count() > 0)
                return false;

            if (player.X == x && player.Y == y)
                return false;

            return true;
        }
        void PlacePlayer(int num) {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < this.map.Tiles.Length; i++) {
                for (int j = 0; j < this.map.Tiles[i].Length; j++) {
                    if (this.map.Tiles[i][j] is Exit)
                        Console.WriteLine("{0} {1}", num, (map.Tiles[i][j] as Exit).Room);
                        //Console.SetCursorPosition(0, 0);
                    if (this.map.Tiles[i][j] is Exit && (this.map.Tiles[i][j] as Exit).Room == num) { 
                        player.X = i;
                        player.Y = j;
                    }
                }     
            }
            
        }
        List<Entity> PlaceEntities(int entityCount) {

            Random rnd = new Random();
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < entityCount; i++)
            {
                Entity tmp = new Devil(rnd.Next(GameInfo.mapHeight), rnd.Next(GameInfo.mapWidth));

                while (this.map.Tiles[tmp.X][tmp.Y].Impassable || this.map.Tiles[tmp.X][tmp.Y].GetType() == typeof(Void)) //TODO
                {
                    tmp.X = rnd.Next(GameInfo.mapHeight);
                    tmp.Y = rnd.Next(GameInfo.mapWidth);
                }
                entities.Add(tmp);
            }

            return entities;
        }
        public void GenerateMap(Entity entity, Generation.From EnterDirection, bool newMap = false)
        {
            if (newMap || entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                int destinationMapNumber;
                
                if (newMap)
                    destinationMapNumber = 0;
                else
                    destinationMapNumber = ((Exit) map.Tiles[player.X][player.Y]).Room;

                Console.Write("{0} {1} {2} ", GameInfo.currentMap, destinationMapNumber, history.Count());

                if (destinationMapNumber == -1) {
                    return;
                }
                else if (destinationMapNumber == history.Count) {
                    history.Add(Generation.GenerateDungeon(GameInfo.mapHeight, GameInfo.mapWidth, EnterDirection, destinationMapNumber));
                }
                
                if (newMap)
                    PlacePlayer(-1);
                else
                {
                    //Console.SetCursorPosition(0, 0);
                    //Console.WriteLine(GameInfo.currentMap);
                    PlacePlayer(GameInfo.currentMap);
                }

                GameInfo.currentMap = destinationMapNumber;
                
                this.entities = PlaceEntities(5);
                
                visiblePoints = new List<(int, int)>();
            }
        }
        public void Turn(bool renderOnly = false)
        {
            if (!renderOnly)
            {
                ElementaryTurn(player);
                entities.RemoveAll(e => e.HealthPoints <= 0);

                foreach (Entity entity in entities){
                    ElementaryTurn(entity);
                }
            }

            foreach ((int, int) point in visiblePoints)
            {
                map.Tiles[point.Item1][point.Item2].Status = VisualStatus.wasSeen;
            }

            visiblePoints = FOV.GetVisibleTiles(map, player, (int)(16 * 1.5), (int)(9 * 1.5));

            foreach ((int, int) point in visiblePoints)
            {
                map.Tiles[point.Item1][point.Item2].Status = VisualStatus.isVisible;
            }
        }
        void ElementaryTurn(Entity entity)
        {
            ActionInfo nextMove = entity.GetNextMove(map, entities, player);

            int n, m;
            switch (nextMove.Action)
            {
                case Action.Up:
                    if (IsNewPlaceOK(entity.X - 1, entity.Y))
                        entity.moveUp();
                    
                    GenerateMap(entity, Generation.From.Up);  
                    break;
                case Action.Down:
                    if (IsNewPlaceOK(entity.X + 1, entity.Y))
                        entity.moveDown();

                    GenerateMap(entity, Generation.From.Down);
                    break;
                case Action.Left:
                    if (IsNewPlaceOK(entity.X, entity.Y - 1))
                        entity.moveLeft();

                    GenerateMap(entity, Generation.From.Left);
                    break;
                case Action.Right:
                    if (IsNewPlaceOK(entity.X, entity.Y + 1))
                        entity.moveRight();

                    GenerateMap(entity, Generation.From.Right); 
                    break;
                case Action.Quit:
                    Program.NormilizeConsole();
                    break;
                case Action.Cheat:
                    CheatConsole.Cheat(this);
                    break;    
                case Action.StayInPlace:
                    break;
                case Action.ChangeColor:
                    break;
                case Action.GiveEffect:
                    break;
                case Action.UseItem:
                    break;
                case Action.Attack:

                    if (entity is Player) { 
                        Entity? target = entities.MinBy(e => Math.Sqrt(Math.Pow(player.X - e.X, 2) + Math.Pow(player.Y - e.Y, 2)));
                        if (target != null && Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= Math.Pow(player.RangeOfHit, 2)) target.HealthPoints -= player.Damage;
                    }
                    else entity.Attack(player);

                    if (player.HealthPoints <= 0) {
                        Program.NormilizeConsole();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
