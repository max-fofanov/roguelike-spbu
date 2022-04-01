using System;
using System.Drawing;

namespace roguelike_spbu
{
    [Serializable]  
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
        public int XPToLevelUP
        {
            get;
            set;
        }
        public Player(int x, int y, Trait trait = Trait.Saber) : base(x, y)
        {
            this.trait = trait;
            SetHealth(100);
            Damage = 3;
            RangeOfHit = 2;
            XP = 0;
            XPToLevelUP = 15;
            VStatus = VisualStatus.isVisible;
            Symbol = symbol;
            PrimaryForegroundColor = color;
            LVL = 1;
            Inventory = new List<Item>() {  };
        }
        public override ActionInfo GetNextMove(Map map, List<Entity> entities, Player player) {

            return SystemInfo.gui.GetAction();
        }

    }
}
