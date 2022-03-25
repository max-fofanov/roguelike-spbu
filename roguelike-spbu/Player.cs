using System;
using System.Drawing;

namespace roguelike_spbu
{
    
    public class Player : Entity
    {
        private Trait trait;
        private string symbol = "@";
        private Color color = Color.White;
        
        public int LVL {
            get;
            set;
        }

        public Color Color {
            get;
            set;
        }

        public int Position {
            get;
            set;
        }

         public  int XPToLevelUP {
            get;
            set;
        }
        public  int PlayerExperiencePoints {
            get;
            set;
        }

        public int maxCapacity = 5;
        

        
        public Player(int x, int y, Trait trait = Trait.Saber)
        {
            X = x;
            Y = y;
            this.trait = trait;
            HealthPoints = 100;
            Damage = 1000;
            RangeOfHit = 2;
            PlayerExperiencePoints = 0;
            XPToLevelUP = 15;
            VStatus = VisualStatus.isVisible;
            Symbol = symbol;
            PrimaryForegroundColor = color;
            LVL = 1;
        }

        public void AddToInventory(Item item) {
            if (Inventory.Count < maxCapacity) {
                Inventory.Add(item);
            }
        }

        public override ActionInfo GetNextMove(Map map, List<Entity> entities, Player player) {

            return SystemInfo.gui.GetAction();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (Console.KeyAvailable) {
                Console.ReadKey(true);
            }

            switch (keyInfo.Key) {
                case ConsoleKey.LeftArrow:
                    return new ActionInfo(Action.Left, player, 1);
                case ConsoleKey.RightArrow:
                    return new ActionInfo(Action.Right, player, 1);
                case ConsoleKey.UpArrow:
                    return new ActionInfo(Action.Up, player, 1);
                case ConsoleKey.DownArrow:
                    return new ActionInfo(Action.Down, player, 1);
                case ConsoleKey.Spacebar:
                    return new ActionInfo(Action.Pass, player, 1);
                case ConsoleKey.Q:
                    return new ActionInfo(Action.Quit, player, 1);
                case ConsoleKey.A:
                    return new ActionInfo(Action.Attack, player, 1);
                case ConsoleKey.C:
                    return new ActionInfo(Action.Cheat, player, 1);
                default:
                    return new ActionInfo(Action.StayInPlace, player, 1);
            }

            throw new KeyNotFoundException("not yet implemented");
        }

    }
}
