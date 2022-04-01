using System;
using System.Collections.Generic;
using System.Drawing;

namespace roguelike_spbu
{
    public enum Action {
        Up,
        Down,
        Left,
        Right,
        StayInPlace,
        ChangeColor,
        GiveEffect,
        UseItem,
        Attack,
        Quit,
        Cheat
    }
    
    public class ActionInfo {
        public ActionInfo(Action? action = null, int? time = null, EntityEffect? effect = null, Guid? target = null, int? position = null) {
            Action = action ?? Action.StayInPlace;
            Effect = effect ?? new EntityEffect();
            Time = time ?? 0;
            Target = target ?? Guid.Empty;
            Position = position ?? -1;
        }

        public ActionInfo(Action action) : this(action, null, null, null, null) { }
        public ActionInfo(Action action, EntityEffect effect, int time) : this(action, time, effect, null, null) { }
        public ActionInfo(Action action, Guid target) : this(action, null, null, target, null) { }
        public ActionInfo(Action action, Guid target, int position) : this(action, null, null, target, position) { }
        public Action Action {
            get;
            set;
        }
        public int Time {
            get;
            set;
        }
        public EntityEffect Effect {
            get;
            set;
        }
        public Guid Target {
            get;
            set;
        }
        public int Position {
            get;
            set;
        }
    }
    public enum EntityAttitude
    {
        friendly,
        passive,
        agressive
    }

    [Serializable]
    public class Entity
    {
        public Guid ID
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
        public Item? LeftHand;
        public Item? RightHand;
        public Item? Body;
        /*public int Stamina
        {
            get;
            set;
        }*/
        public string Name
        {
            get;
            set;
        }
        public string? Description
        {
            get;
            set;
        }
        public string? CreatureType
        {
            get;
            set;
        }
        public string? ForceType
        {
            get;
            set;
        }
        public int Damage
        {
            get;
            set;
        }
        int _hp = 0;
        int _mhp = 0;
        public int HealthPoints
        {
            get { return _hp; }
            set { _hp = Math.Min(value, MaxHealthPoints); }
        }
        public int MaxHealthPoints
        {
            get { return _mhp; }
            set { _mhp = value; }
        }
        public void SetHealth(int hp, bool regen = true)
        {
            MaxHealthPoints = hp;
            if (regen)
                HealthPoints = hp;
        }
        public int XP
        {
            get;
            set;
        }
        float _rangeOfHit = 0;
        public float RangeOfHit
        {
            get { return Math.Max(_rangeOfHit, Math.Max((LeftHand ?? new Item()).RangeOfHit, (RightHand ?? new Item()).RangeOfHit)); }
            set { _rangeOfHit = value; }
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

        public int  Range {
            get;
            set;
        }
        public int MaxInventoryCapacity = 5;
        private List<Item> _inventory = new List<Item>();
        public List<Item> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; } //.Length > 0 ? value : Array.Empty<Item>(); }
        }
        public bool IgnoreEngine
        {
            get;
            set;
        }
        public Entity(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
            Name = "NoName";
            ID = Guid.NewGuid();
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
        public void PassTurn()
        {
            HealthPoints++;
        }
        public void ChangeColor(Color TempColor) { }
        public void GetEffect(EntityEffect effect, int time) { }
        public bool IsTwoHandWeaponEquiped()
        {
            return (LeftHand ?? new Item()).Type == ItemType.TwoHandWeapon ||
                    (RightHand ?? new Item()).Type == ItemType.TwoHandWeapon;
        }
        public bool IsItemAlreadyEquiped(Guid ID)
        {
            return (LeftHand ?? new Item()).ID == ID ||
                    (RightHand ?? new Item()).ID == ID ||
                    (Body ?? new Item()).ID == ID;
        }
        void PutItemOnEntity(Item item, int place = 0)
        {
            if (place != -1)
            {
                if (item.Type == ItemType.OneHandWeapon)
                {
                    //Console.Beep();
                    if (place == 0)
                    {
                        if (IsTwoHandWeaponEquiped())
                        {
                            LeftHand = null;
                        }
                        if (item.ID == (LeftHand ?? new Item()).ID)
                        {
                            //LeftHand = null;
                            LeftHand = RightHand;
                        }
                        RightHand = item;
                    }
                    else if (place == 1)
                    {
                        if (IsTwoHandWeaponEquiped())
                        {
                            RightHand = null;
                        }
                        if (item.ID == (RightHand ?? new Item()).ID)
                        {
                            //RightHand = null;
                            RightHand = LeftHand;
                        }
                        LeftHand = item;
                    }
                    else if (place == -2)
                    {
                        if (!IsItemAlreadyEquiped(item.ID))
                            if (IsTwoHandWeaponEquiped())
                            {
                                RightHand = item;
                                LeftHand = null;
                            }
                            else if (RightHand == null)
                            {
                                RightHand = item;
                            }
                            else if (LeftHand == null)
                            {
                                LeftHand = item;
                            }
                    }
                }
                else if (item.Type == ItemType.TwoHandWeapon)
                {
                    if (!IsItemAlreadyEquiped(item.ID))
                    {
                        LeftHand = item;
                        RightHand = item;
                    }
                }
                else if (item.Type == ItemType.Armor)
                {
                    if (!IsItemAlreadyEquiped(item.ID))
                        Body = item;
                }
                else if (item.Type == ItemType.Consumable)
                {

                }
            }
            else if (IsItemAlreadyEquiped(item.ID) && place == -1)
            {
                //Console.Beep();
                if (item.Type == ItemType.OneHandWeapon)
                {
                    if (item.ID == (LeftHand ?? new Item()).ID)
                    {
                        LeftHand = null;
                    }
                    else if (item.ID == (RightHand ?? new Item()).ID)
                    {
                        RightHand = null;
                    }
                }
                else if (item.Type == ItemType.TwoHandWeapon)
                {
                    LeftHand = null;
                    RightHand = null;
                }
                else if (item.Type == ItemType.Armor)
                {
                    Body = null;
                }
            }
        }
        public void UseItem(Guid itemID, int place = 0) {
            // TODO: make possible to remove items from hand
            foreach (Item item in Inventory)
            {
                if (item.ID == itemID)
                {
                    //Console.Beep();
                    PutItemOnEntity(item, place);
                    break;
                }
            }
        }
        public void AddToInventory(Item item)
        {
            if (Inventory.Count < MaxInventoryCapacity)
            {
                Inventory.Add(item);
            }
        }
        public void RemoveFromInventory(Guid ID)
        {
            Inventory.RemoveAll(t => t.ID == ID);
        }
        public int GetTotalAttack()
        {
            int totalDamage = Damage + (RightHand ?? new Item()).Damage;

            if (!IsTwoHandWeaponEquiped())
                totalDamage += (LeftHand ?? new Item()).Damage;

            return totalDamage;
        }
        public int GetTotalDefence()
        {
            int totalDefence = (Body ?? new Item()).Defence + (RightHand ?? new Item()).Defence;

            if (!IsTwoHandWeaponEquiped())
                totalDefence += (LeftHand ?? new Item()).Damage;

            return totalDefence;
        }
        public virtual void Attack(Entity target)
        {
            target.GetDamage(GetTotalAttack());
        }
        public virtual void GetDamage(int damage)
        {
            this.HealthPoints -= Math.Max(damage - GetTotalDefence(), 0);
        }
        public virtual ActionInfo GetNextMove(Map map, List<Entity> entities, Player player) { return new ActionInfo(); }
    }
}
