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

                float[] monsterschance = { Math.Max(1, 60 - player.LVL - history.Count), 12, 10 + player.LVL + history.Count,  Math.Max(1, 40 - player.LVL), 20, 6 + player.LVL + history.Count, 1 + 3*(player.LVL + history.Count), 1 + 3*(player.LVL + history.Count), 1 + 4*(player.LVL + history.Count)};
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

        public List<Entity> GetEntitiesInRange() {
            return entities.FindAll(target => Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= Math.Pow(player.RangeOfHit, 2));
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
                    history.Add(Generation.GenerateDungeon(GameInfo.mapHeight, GameInfo.mapWidth, EnterDirection, destinationMapNumber));
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
                this.entities = PlaceEntities(rnd.Next(5, 7 + player.LVL + history.Count));
                
                visiblePoints = new List<(int, int)>();
            }
        }
        public void Turn(bool renderOnly = false)
        {
            if (!renderOnly)
            {
                ElementaryTurn(player);
                foreach (Entity entity in entities){
                    if (entity.HealthPoints <0)
                    {
                        player.XP += entity.XP;
                        while (player.PlayerExperiencePoints > player.XPToLevelUP)
                        {
                            int n = player.PlayerExperiencePoints / player.XPToLevelUP;
                            player.LVL += player.PlayerExperiencePoints % player.XPToLevelUP;
                            player.HealthPoints += 50;
                            player.Damage += 20;
                            player.PlayerExperiencePoints -= player.XPToLevelUP;
                            player.XPToLevelUP += 30;
                        }
                        
                    }
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
                    break;
                case Action.ChangeColor:
                    break;
                case Action.GiveEffect:
                    break;
                case Action.UseItem:
                    break;
                case Action.Attack:

                    if (entity is Player) {
                        for (int i = 0; i < entities.Count(); i++)
                        {
                            if (entities[i].ID == nextMove.Target)
                            {
                                entities[i].HealthPoints -= player.Damage;
                            }
                        }
                        
                        // List<Entity> enemies = GetEntitiesInRange();
                        // if (nextMove.Number >= 0 && nextMove.Number < enemies.Count())
                            // enemies[nextMove.Number].HealthPoints -= player.Damage;
                        // Entity? target = entities.MinBy(e => Math.Sqrt(Math.Pow(player.X - e.X, 2) + Math.Pow(player.Y - e.Y, 2)));
                        //if (target != null && Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= Math.Pow(player.RangeOfHit, 2)) target.HealthPoints -= player.Damage;
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
