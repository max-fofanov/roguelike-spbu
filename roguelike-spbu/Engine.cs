namespace roguelike_spbu
{
    public class Engine
    {
        Map map;
        Player player;
        List<Entity> entities;
        List<(int, int)> visiblePoints = new List<(int, int)>();
        public Engine(Map map, List<Entity> entities, Player player)
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
                        if (target != null && Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2) <= player.RangeOfHit) target.HealthPoints -= player.Damage;
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
