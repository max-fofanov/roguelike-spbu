namespace roguelike_spbu
{
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



        public Item() {
            ID = Guid.NewGuid();
            Name = "";
        }

        public virtual void Effect() {

        }
    }
}