namespace roguelike_spbu
{
    public class Engine
    {
        Map map;
        public Player player;
        public List<Entity> entities;

        public List<Map> history = new List<Map>();
        List<(int, int)> visiblePoints = new List<(int, int)>();
        public bool allVisible = false; 
        public Engine(Map map, List<Entity> entities, Player player)
        {
            this.map = map;
            this.entities = entities;
            this.player = player;
            history.Add(map);
        }
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
                    if (map.Tiles[i][j] is Exit && (map.Tiles[i][j] as Exit).Room == num) { 
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
                Entity tmp = new Devil(rnd.Next(45), rnd.Next(180));

                while (this.map.Tiles[tmp.X][tmp.Y].Impassable || this.map.Tiles[tmp.X][tmp.Y].GetType() == typeof(Void)) //TODO
                {
                    tmp.X = rnd.Next(45);
                    tmp.Y = rnd.Next(180);
                }
                entities.Add(tmp);
            }

            return entities;
        }
        public (Map, List<Entity>) Turn(bool renderOnly = false)
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

            return (map, entities);
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
                    
                    if (entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                        n = ((Exit) map.Tiles[player.X][player.Y]).Room;
                        m = map.Num;

                        if (n == -1) {
                            break;
                        }
                        else if (n == history.Count) {
                            this.map = Generation.GenerateDungeon(45, 180, Generation.From.Up, m + 1);
                            history.Add(this.map);
                            
                        }
                        else {
                            this.map = history[n];
                        }

                        PlacePlayer(m);
                        this.entities = PlaceEntities(5);
                        
                        visiblePoints = new List<(int, int)>();
                    }    
                    break;
                case Action.Down:
                    if (IsNewPlaceOK(entity.X + 1, entity.Y))
                        entity.moveDown();

                    if (entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                        n = ((Exit) map.Tiles[player.X][player.Y]).Room;
                        m = map.Num;

                        if (n == -1) {
                            break;
                        }
                        else if (n == history.Count) {
                            this.map = Generation.GenerateDungeon(45, 180, Generation.From.Down, m + 1);
                            history.Add(this.map);
                        }
                        else {
                            this.map = history[n];
                        }
                        this.entities = PlaceEntities(5);
                        PlacePlayer(m);
                        visiblePoints = new List<(int, int)>();
                    }  
                    break;
                case Action.Left:
                    if (IsNewPlaceOK(entity.X, entity.Y - 1))
                        entity.moveLeft();

                    if (entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                        n = ((Exit) map.Tiles[player.X][player.Y]).Room;
                        m = map.Num;

                        if (n == -1) {
                            break;
                        }
                        else if (n == history.Count) {
                            this.map = Generation.GenerateDungeon(45, 180, Generation.From.Left, m + 1);
                            history.Add(this.map);
                        }
                        else {
                            this.map = history[n];
                        }
                        this.entities = PlaceEntities(5);
                        PlacePlayer(m);
                        visiblePoints = new List<(int, int)>();
                    }  
                    break;
                case Action.Right:
                    if (IsNewPlaceOK(entity.X, entity.Y + 1))
                        entity.moveRight();

                    if (entity is Player && map.Tiles[player.X][player.Y] is Exit) {
                        n = ((Exit) map.Tiles[player.X][player.Y]).Room;
                        m = map.Num;

                        if (n == -1) {
                            break;
                        }
                        else if (n == history.Count) {
                            this.map = Generation.GenerateDungeon(45, 180, Generation.From.Right, m + 1);
                            history.Add(this.map);
                        }
                        else {
                            this.map = history[n];
                        }
                        this.entities = PlaceEntities(5);
                        PlacePlayer(m);
                        visiblePoints = new List<(int, int)>();
                    }  
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
