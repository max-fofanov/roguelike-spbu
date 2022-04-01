namespace roguelike_spbu
{
    public class Engine
    {

        public Map map {
            get {
                if (GameInfo.history.Count() == 0) return new Map(0, 0, 0);
                return GameInfo.history[GameInfo.currentMap].map;
            }
            set {
                GameInfo.history[GameInfo.currentMap].map = value;
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
                return GameInfo.history[GameInfo.currentMap].entities;
            }

            set {
                GameInfo.history[GameInfo.currentMap].entities = value;
            }
        }

        public List<Plane> history {
            get {
                return GameInfo.history;
            }

            set {
                GameInfo.history = value;
            }
        }
        List<(int, int)> visiblePoints = new List<(int, int)>();
        public void ResetVisiblePoints()
        {
            visiblePoints = new List<(int, int)>();
        }
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
            for (int i = 0; i < map.Tiles.Length; i++) {
                for (int j = 0; j < map.Tiles[i].Length; j++) {
                    if (map.Tiles[i][j] is Exit && (map.Tiles[i][j] as Exit)!.Room == num) { 
            /*for (int i = 0; i < this.map.Tiles.Length; i++) {
                for (int j = 0; j < this.map.Tiles[i].Length; j++) {
                    if (this.map.Tiles[i][j] is Exit && (this.map.Tiles[i][j] as Exit).Room == num) { */
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
                int x = rnd.Next(GameInfo.mapHeight);
                int y = rnd.Next(GameInfo.mapWidth);

                List<Entity> monsters = new List<Entity>();
                monsters.Add(new Goblin(x, y));
                monsters.Add(new GoblinFlinger(x, y));
                monsters.Add(new Hobgoblin(x, y));
                monsters.Add(new Skeleton(x, y));
                monsters.Add(new Zombie(x, y));
                monsters.Add(new Lich(x, y));
                monsters.Add(new DeathKnight(x, y));
                monsters.Add(new Devil (x, y));
                monsters.Add(new Archangel(x, y));
                monsters.Add(new Demon(x,y));

                float[] monsterschance = { Math.Max(1, 1000 - 140*(player.LVL + history.Count)),  Math.Max(20, 80 - 8*(player.LVL + history.Count)), 10 + player.LVL + history.Count,  Math.Max(1, 700 - 100* (player.LVL + history.Count)), 30, 20 + player.LVL + history.Count, 1 + 3*(player.LVL + history.Count), 1 + 3*(player.LVL + history.Count), 1 + 4*(player.LVL + history.Count)};
                Entity tmp = monsters[Walker.Alias(monsterschance)];

                while (this.map.Tiles[tmp.X][tmp.Y].Impassable || this.map.Tiles[tmp.X][tmp.Y].GetType() == typeof(Void))
                //TODO check if in new place no other monsters
                //TODO copy code from lore branch and make stating position generation once
                {
                    tmp.X = rnd.Next(GameInfo.mapHeight);
                    tmp.Y = rnd.Next(GameInfo.mapWidth);
                }
                entities.Add(tmp);
            }

            return entities;
        }

        public List<Entity> GetEntitiesInRange()
        {
            return entities.FindAll(target => Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= Math.Pow(player.RangeOfHit, 2));
        }
        public List<Chest> GetChestsInRange()
        {
            return entities.FindAll(target => (Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= Math.Pow(player.RangeOfHit, 2)) && target is Chest).Select(t => (t as Chest)!).ToList();
        }
        public void GenerateMap(Entity entity, Generation.From EnterDirection, bool newMap = false)
        {
            if (newMap || entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                int destinationMapNumber;
                
                if (newMap)
                    destinationMapNumber = 0;
                else
                    destinationMapNumber = ((Exit) map.Tiles[player.X][player.Y]).Room;

                if (destinationMapNumber == -1) {
                    return;
                }
                else if (destinationMapNumber == history.Count()) {
                    // Console.WriteLine("Im creating a new map");

                    Plane tmp = new Plane();
                    tmp.map = Generation.GenerateDungeon(GameInfo.mapHeight, GameInfo.mapWidth, EnterDirection, destinationMapNumber);
                    history.Add(tmp);
                }

                int currentMap = GameInfo.currentMap;
                GameInfo.currentMap = destinationMapNumber;

                if (newMap)
                    PlacePlayer(-1);
                else
                    PlacePlayer(currentMap);

                // Console.WriteLine(history.Count());
                // Console.WriteLine(GameInfo.currentMap);
                // Console.ReadKey(true);

                Random rnd = new Random();
                if (player.LVL + history.Count < 9)
                {
                    this.entities = PlaceEntities(rnd.Next(5, 5 + player.LVL + history.Count));
                }
                else if (player.LVL + history.Count < 20)
                {
                    this.entities = PlaceEntities(rnd.Next(3, 7));
                }
                else{
                    this.entities = PlaceEntities(rnd.Next(7, 15));
                }

                ResetVisiblePoints();
                //visiblePoints = new List<(int, int)>();
            }
        }
        public void Turn(bool renderOnly = false)
        {
            if (!renderOnly)
            {
                ElementaryTurn(player);
                foreach (Entity entity in entities){
                    if (entity.HealthPoints <= 0)
                    {
                        player.XP += entity.XP;
                        Statistics.statistics["killed"] = (int) Statistics.statistics["killed"] + 1;
                    }
                }
                while (player.XP > player.XPToLevelUP)
                {
                    player.LVL++;
                    player.SetHealth(player.MaxHealthPoints + 25, false);
                    player.Damage += 3;
                    player.XP -= player.XPToLevelUP;
                    player.XPToLevelUP += 30;
                }

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
                    entity.PassTurn();
                    break;
                case Action.ChangeColor:
                    break;
                case Action.GiveEffect:
                    break;
                case Action.UseItem:
                    //Console.Beep();
                    entity.UseItem(nextMove.Target, nextMove.Position);
                    break;
                case Action.Attack:
                    if (nextMove.Target == player.ID)
                        entity.Attack(player);
                    else
                        for (int i = 0; i < entities.Count(); i++)
                        {
                            if (entities[i].ID == nextMove.Target)
                            {
                                player.Attack(entities[i]);
                                //entities[i].HealthPoints -= player.Damage;
                                break;
                            }
                        }

                    if (player.HealthPoints <= 0) {
                        Statistics.statistics["deaths"] = (int) Statistics.statistics["deaths"] + 1;
                        Program.NormilizeConsole();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
