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

            return true;
        }
        public void Turn()
        {
            ElementaryTurn(player);
            foreach (Entity entity in entities) {
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