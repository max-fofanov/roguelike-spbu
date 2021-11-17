using System;

namespace roguelike_spbu
{
    class Tile
    {
        private Entity landscape;
        private Creature inhabitat;

        public Entity Landscape
        {
            get { return this.landscape; }
            set { this.landscape = value; }
        }

        public Creature Inhabitat
        {
            get { return this.inhabitat; }
            set { this.inhabitat = value; }
        }

        public Tile(Entity landscape, Creature inhabitat = null)
        {
            this.Landscape = landscape;
            this.Inhabitat = inhabitat;
        }
    }
}
