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

        }

        public virtual void Effect() {

        }
    }
}