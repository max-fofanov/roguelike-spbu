using System;
using System.Drawing;

namespace roguelike_spbu
{
    
    public class Player : Entity
    {
        private int x;
        private int y;
        private Trait trait;
        private string symbol = "@";
        private Color color = Color.Red;

        public Player(int x, int y, Trait trait = Trait.Saber)
        {
            X = x;
            Y = y;
            this.trait = trait;
            Symbol = symbol;
            PrimaryForegroundColor = color;
        }

        public override ActionInfo GetNextMove(Map map, Entity[] entities, Player player) {

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key) {
                case ConsoleKey.LeftArrow:
                    return new ActionInfo(Action.Left, player, 1);

                case ConsoleKey.RightArrow:
                    return new ActionInfo(Action.Right, player, 1);

                case ConsoleKey.UpArrow:
                    return new ActionInfo(Action.Up, player, 1);

                case ConsoleKey.DownArrow:
                    return new ActionInfo(Action.Down, player, 1);        

            }

            throw new KeyNotFoundException("not yet implemented");
        }

    }
}
