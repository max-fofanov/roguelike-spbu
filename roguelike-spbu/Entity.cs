using System;
using System.Drawing;

namespace roguelike_spbu
{
    
    public enum Action {
        Up,
        Down,
        Left,
        Right,
        Pass,
        StayInPlace,
        ChangeColor,
        GiveEffect,
        UseItem,
        Attack,
        Quit
    }
    
    public class ActionInfo {
        

        public ActionInfo() {}

        public ActionInfo(Action action) {
            Action = action;
        }

        public ActionInfo(Action action, Color color) {
            Action = action;
            Color = color;
        }

        public ActionInfo(Action action, EntityEffect effect, int time) {
            Action = action;
            Effect = effect;
            Time = time;
        }
        public ActionInfo(Action action, Entity entity, int power) {
            Action = action;
            Entity = entity;
            Power = power;
        }

        public ActionInfo(Action action, int number) {
            Action = action;
            Number = number;
        }

        public ActionInfo(Action action, Guid target, int power) {
            Action = action;
            Target = target;
            Power = power;
        }
        public Action Action {
            get;
            set;
        }

        public Entity? Entity {
            get;
            set;
        }

        public int Power {
            get;
            set;
        }

        public Color Color {
            get;
            set;
        }

        public int Time {
            get;
            set;
        }

        public EntityEffect? Effect {
            get;
            set;
        }

        public int Number {
            get;
            set;
        }

        public Guid? Target {
            get;
            set;
        }

    }
    
    public enum EntityStatus
    {

    }
    public enum EntityAttitude
    {
        friendly,
        passive,
        agressive
    }
    public class Entity
    {
        public Guid id
        {
            set;
            get;
        }
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public int RangeOfView
        {
            get;
            set;
        }
        public int Stamina
        {
            get;
            set;
        }
        private string _symbol = "";
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value.Length > 0 ? value[0].ToString() : " ";
            }
        }
        public Color PrimaryForegroundColor
        {
            get;
            set;
        }
        public Color? PrimaryBackgroundColor
        {
            get;
            set;
        }
        public Color? CurrentForegroundColor
        {
            get;
            set;
        }
        public EntityStatus Status
        {
            get;
            set;
        }
        public int StatusTime
        {
            get;
            set;
        }
        public VisualStatus VStatus
        {
            get;
            set;
        }
        public EntityAttitude Attitude
        {
            get;
            set;
        }
        private Item[] _inventory = Array.Empty<Item>();
        public Item[] Inventory
        {
            get { return _inventory; }
            set { _inventory = value.Length > 0 ? value : Array.Empty<Item>(); }
        }
        public bool IgnoreEngine
        {
            get;
            set;
        }
        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void moveUp() { X--; }
        public void moveDown() { X++; }
        public void moveLeft() { Y--; }
        public void moveRight() { Y++; }
        public void PassTurn() { }
        public void ChangeColor(Color TempColor) { }
        public void GetEffect(EntityEffect effect, int time) { }
        public void UseItem(int number) { }
        public void GetDamage(int damage) { }
        public virtual ActionInfo GetNextMove(Map map, List<Entity> entities, Player player) { return new ActionInfo(); }
    }
}
