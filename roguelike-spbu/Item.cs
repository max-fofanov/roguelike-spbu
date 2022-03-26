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

        public Item() {
            ID = Guid.NewGuid();
            Name = "";
        }

        public virtual void Effect() {

        }
    }
}