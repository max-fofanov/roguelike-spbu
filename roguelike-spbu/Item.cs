namespace roguelike_spbu
{
    public enum ItemType
    {
        None,
        Consumable,
        OneHandWeapon,
        TwoHandWeapon,
        Armor
    }
    [Serializable]
    public class Item 
    {
        public Guid ID
        {
            set;
            get;
        }
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
        public int Damage
        {
            get;
            set;
        }
        public int Defence
        {
            get;
            set;
        }
        public int HealthPoints
        {
            get;
            set;
        }
        public float RangeOfHit
        {
            get;
            set;
        }
        private string _symbol = "";
        /*public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value.Length > 0 ? value[0].ToString() : " ";
            }
        }*/
        public ItemType Type = ItemType.None;
        public Item() {
            ID = Guid.NewGuid();
            Damage = 0;
            RangeOfHit = 0;
            Name = "";
        }
        public virtual void Effect() {

        }
    }
}