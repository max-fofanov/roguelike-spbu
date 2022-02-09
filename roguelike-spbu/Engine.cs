namespace roguelike_spbu
{
    public class Engine
    {
        Map map;
        Player player;
        Entity[] entities;
        public Engine(Map map, Entity[] entities, Player player)
        {
            this.map = map;
            this.entities = entities;
            this.player = player;
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
        public void Turn()
        {
            ElementaryTurn(player);

            foreach (Entity entity in entities) 
            {
                while (true) {
                    entity.UsedTiles.Add(map.Tiles[entity.X][entity.Y]);
                    List<Tile> tiles = new List<Tile>();

                    if (IsNewPlaceOK(entity.X - 1, entity.Y) && ! map.Tiles[entity.X - 1][entity.Y].Impassable 
                    && ! entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X - 1][entity.Y])) {
                        tiles.Add(map.Tiles[entity.X - 1][entity.Y]);
                        map.Tiles[entity.X - 1][entity.Y].From = map.Tiles[entity.X][entity.Y];
                    }
                    if (IsNewPlaceOK(entity.X + 1, entity.Y) && ! map.Tiles[entity.X + 1][entity.Y].Impassable 
                    && ! entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X + 1][entity.Y])) {
                        tiles.Add(map.Tiles[entity.X + 1][entity.Y]);
                        map.Tiles[entity.X + 1][entity.Y].From = map.Tiles[entity.X][entity.Y];
                    }
                    if (IsNewPlaceOK(entity.X, entity.Y - 1) && ! map.Tiles[entity.X][entity.Y - 1].Impassable 
                    && ! entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X][entity.Y - 1])) {
                        tiles.Add(map.Tiles[entity.X][entity.Y - 1]);
                        map.Tiles[entity.X][entity.Y - 1].From = map.Tiles[entity.X][entity.Y];
                    }
                    if (IsNewPlaceOK(entity.X, entity.Y + 1) && ! map.Tiles[entity.X][entity.Y + 1].Impassable 
                    && ! entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X][entity.Y + 1])) {
                        tiles.Add(map.Tiles[entity.X][entity.Y + 1]);
                        map.Tiles[entity.X][entity.Y + 1].From = map.Tiles[entity.X][entity.Y];
                    }

                    foreach (Tile tile in tiles) {
                        tile.Path = tile.From.Path + 1;
                        tile.Weight = Math.Abs(entity.X - player.X) + Math.Abs(entity.Y - player.Y) + tile.Path;
                    }

                    tiles.MinBy((o1) => o1.Weight);
                }

            }

            Console.WriteLine(Renderer.Render(map, entities, player));
        }
        void ElementaryTurn(Entity entity)
        {
            ActionInfo nextMove = entity.GetNextMove(map, entities, player);

            switch (nextMove.Action)
            {
                case Action.Up:
                    if (IsNewPlaceOK(entity.X - 1, entity.Y))
                        entity.moveUp();
                    break;
                case Action.Down:
                    if (IsNewPlaceOK(entity.X + 1, entity.Y))
                        entity.moveDown();
                    break;
                case Action.Left:
                    if (IsNewPlaceOK(entity.X, entity.Y - 1))
                        entity.moveLeft();
                    break;
                case Action.Right:
                    if (IsNewPlaceOK(entity.X, entity.Y + 1))
                        entity.moveRight();
                    break;
                case Action.Quit:
                    Program.NormilizeConsole();
                    break;    
                case Action.Pass:
                    break;
                case Action.ChangeColor:
                    break;
                case Action.GiveEffect:
                    break;
                case Action.UseItem:
                    break;
                case Action.Attack:
                    break;
            }
        }  
    }
}

/*
                Random random = new Random();
                int i = random.Next(4);
                
                switch (i) {
                    case 0:
                        if (IsNewPlaceOK(entity.X - 1, entity.Y))
                            entity.SetCoordinates(entity.X - 1, entity.Y);

                        break;
                    case 1:
                        if (IsNewPlaceOK(entity.X + 1, entity.Y))
                            entity.SetCoordinates(entity.X + 1, entity.Y);

                        break;
                    case 2:
                        if (IsNewPlaceOK(entity.X, entity.Y - 1))
                            entity.SetCoordinates(entity.X, entity.Y - 1);

                        break;
                    case 3:
                        if (IsNewPlaceOK(entity.X, entity.Y + 1))
                            entity.SetCoordinates(entity.X, entity.Y + 1);

                        break;        
                }

                */