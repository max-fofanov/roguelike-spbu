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
        ChangeColor,
        GiveEffect,
        UseItem,
        Attack
    }
    
    public struct ActionInfo {
        
        public Action Action {
            get;
            set;
        }

        public Entity Entity {
            get;
            set;
        }

        public int Power {
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
        public int id
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
        public void GiveEffect(EntityEffect effect, int time) { }
        public void UseItem(int number) { }
        public void GetDamage(int damage) { }
        public ActionInfo GetNextMove(Map map, Entity[] entities, Player player) { return new ActionInfo(); }
    }
}
