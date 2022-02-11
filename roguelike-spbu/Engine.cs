namespace roguelike_spbu
{
    public class Engine
    {
        Map map;
        Player player;
        Entity[] entities;
        List<(int, int)> visiblePoints = new List<(int, int)>();
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
        public void Turn(bool renderOnly = false)
        {
            if (!renderOnly)
            {
                ElementaryTurn(player);

                foreach (Entity entity in entities)
                {
                    
                    foreach (Tile[] tiles1 in map.Tiles) {
                        foreach (Tile tile in tiles1) {
                            tile.From = null;
                        }

                    }
                    
                    entity.UsedTiles = new List<Tile>();
                    int startX = entity.X;
                    int startY = entity.Y;

                    while (Math.Abs(entity.X - player.X) > 1 || Math.Abs(entity.Y - player.Y) > 1)
                    {

                        List<Tile> tiles = new List<Tile>();
                        entity.UsedTiles.Add(map.Tiles[entity.X][entity.Y]);

                        if (IsNewPlaceOK(entity.X - 1, entity.Y) && !map.Tiles[entity.X - 1][entity.Y].Impassable
                        && !entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X - 1][entity.Y]))
                        {
                            tiles.Add(map.Tiles[entity.X - 1][entity.Y]);
                            map.Tiles[entity.X - 1][entity.Y].From = map.Tiles[entity.X][entity.Y];
                            map.Tiles[entity.X - 1][entity.Y].Path = map.Tiles[entity.X][entity.Y].Path + 1;
                            map.Tiles[entity.X - 1][entity.Y].Weight = Math.Abs(entity.X - 1 - player.X) + Math.Abs(entity.Y - player.Y) + map.Tiles[entity.X - 1][entity.Y].Path;
                        }
                        if (IsNewPlaceOK(entity.X + 1, entity.Y) && !map.Tiles[entity.X + 1][entity.Y].Impassable
                        && !entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X + 1][entity.Y]))
                        {
                            tiles.Add(map.Tiles[entity.X + 1][entity.Y]);
                            map.Tiles[entity.X + 1][entity.Y].From = map.Tiles[entity.X][entity.Y];
                            map.Tiles[entity.X + 1][entity.Y].Path = map.Tiles[entity.X][entity.Y].Path + 1;
                            map.Tiles[entity.X + 1][entity.Y].Weight = Math.Abs(entity.X + 1 - player.X) + Math.Abs(entity.Y - player.Y) + map.Tiles[entity.X + 1][entity.Y].Path;
                        }
                        if (IsNewPlaceOK(entity.X, entity.Y - 1) && !map.Tiles[entity.X][entity.Y - 1].Impassable
                        && !entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X][entity.Y - 1]))
                        {
                            tiles.Add(map.Tiles[entity.X][entity.Y - 1]);
                            map.Tiles[entity.X][entity.Y - 1].From = map.Tiles[entity.X][entity.Y];
                            map.Tiles[entity.X][entity.Y - 1].Path = map.Tiles[entity.X][entity.Y].Path + 1;
                            map.Tiles[entity.X][entity.Y - 1].Weight = Math.Abs(entity.X - player.X) + Math.Abs(entity.Y - 1 - player.Y) + map.Tiles[entity.X][entity.Y - 1].Path;
                        }
                        if (IsNewPlaceOK(entity.X, entity.Y + 1) && !map.Tiles[entity.X][entity.Y + 1].Impassable
                        && !entity.UsedTiles.Contains<Tile>(map.Tiles[entity.X][entity.Y + 1]))
                        {
                            tiles.Add(map.Tiles[entity.X][entity.Y + 1]);
                            map.Tiles[entity.X][entity.Y + 1].From = map.Tiles[entity.X][entity.Y];
                            map.Tiles[entity.X][entity.Y + 1].Path = map.Tiles[entity.X][entity.Y].Path + 1;
                            map.Tiles[entity.X][entity.Y + 1].Weight = Math.Abs(entity.X - player.X) + Math.Abs(entity.Y + 1 - player.Y) + map.Tiles[entity.X][entity.Y + 1].Path;
                        }

                        if (tiles.Count == 0)
                        {
                            entity.X = map.Tiles[entity.X][entity.Y].From.X;
                            entity.Y = map.Tiles[entity.X][entity.Y].From.Y;
                        }
                        else
                        {
                            entity.X = tiles.MinBy((tile) => tile.Weight).X;
                            entity.Y = tiles.MinBy((tile) => tile.Weight).Y;
                        }

                    }

                    while (map.Tiles[entity.X][entity.Y].From != null && (map.Tiles[entity.X][entity.Y].From.X != startX || map.Tiles[entity.X][entity.Y].From.Y != startY)) {
                        entity.X = map.Tiles[entity.X][entity.Y].From.X;
                        entity.Y = map.Tiles[entity.X][entity.Y].From.Y;
                    }

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

            Console.WriteLine(Renderer.Render(map, entities, player));
            // entities = Array.Empty<Entity>(); // REMOVE
        }

        void PrintPath(Tile tile)
        {
            if (tile.From == null)
            {
                return;
            }
            else
            {
                tile.PrimaryBackgroundColor = System.Drawing.Color.OrangeRed;
                PrintPath(tile.From);
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
            foreach (Entity entity in entities) 
            {
                entity.UsedTiles = new List<Tile>();
                List<Tile> tiles;
                int startX = entity.X;
                int startY = entity.Y;

                while (Math.Abs(entity.X - player.X) > 1 || Math.Abs(entity.Y - player.Y) > 1) {
                    
                    entity.UsedTiles.Add(map.Tiles[entity.X][entity.Y]);
                    tiles = new List<Tile>();

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

                    if (tiles.Count > 0) {
                        entity.X = tiles.MinBy((o1) => o1.Weight).X;
                        entity.Y = tiles.MinBy((o1) => o1.Weight).Y;
                    }
                    else {
                        entity.X = map.Tiles[entity.X][entity.Y].From.X;
                        entity.Y = map.Tiles[entity.X][entity.Y].From.Y;
                    }
                }

                while (entity.X != startX || entity.Y != startY) {
                    map.Tiles[entity.X][entity.Y].PrimaryBackgroundColor = System.Drawing.Color.Blue;
                    entity.X = map.Tiles[entity.X][entity.Y].From.X;
                    entity.Y = map.Tiles[entity.X][entity.Y].From.Y; 
                }

            }
            */

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