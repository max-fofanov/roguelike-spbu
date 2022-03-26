namespace roguelike_spbu
{
    class SwordOneHanded: Item
    {
        public SwordOneHanded()
        {
            Name = "Sword One Handed";
            Damage = 15;
            RangeOfHit = 1;
            Symbol = "/";
            Description = " ";
        }
    }

    class BastardSword: Item
    {
        public BastardSword()
        {
            Name = "Bastard Sword";
            Damage = 20;
            RangeOfHit = 2;
            Symbol = "½";
            Description = " ";
        }
    }

    class TwoHandedSword: Item
    {
        public TwoHandedSword()
        {
            Name = "Two Handed Sword";
            Damage = 26;
            RangeOfHit = 2;
            Symbol = "!";
            Description = " ";
        }
    }

    class Aerondight: Item
    {
        public Aerondight()
        {
            Name = "Aerondight";
            Damage = 60;
            RangeOfHit = 3;
            Symbol = "@";
            Description = " ";
        }
    }
    class Spear: Item
    {
        public Spear()
        {
            Name = "Spear";
            Damage = 16;
            RangeOfHit = 3;
            Symbol = "|";
            Description = " ";
        }
    }
    class MagicBolt: Item
    {
        public MagicBolt()
        {
            Name = "Magic Bolt";
            Damage = 15;
            RangeOfHit = 4;
            Symbol = "~";
            Description = " ";
        }
    }

    class MagicLince: Item
    {
        public MagicLince()
        {
            Name = "Magic Lince";
            Damage = 25;
            RangeOfHit = 4;
            Symbol = "-";
            Description = " ";
        }
    }
    class Bow: Item
    {
        public Bow()
        {
            Name = "Bow";
            Damage = 12;
            RangeOfHit = 4;
            Symbol = ")";
            Description = " ";
        }
    }

    class MagicStrike: Item //all enem
    {
        public MagicStrike()
        {
            Name = "Magic Strike";
            Damage = 25;
            RangeOfHit = 3;
            Symbol = "*";
            Description = " ";
        }
    }
    
    class Mjolnir: Item
    {
        public Mjolnir()
        {
            Name = "Mjolnir";
            Damage = 40;
            RangeOfHit = 4;
            Symbol = "%";
            Description = " ";
        }
    }

    class Frostmourne: Item
    {
        public Frostmourne()
        {
            Name = "Frostmourne";
            Damage = 100;
            RangeOfHit = 1;
            Symbol = "F";
            Description = "For the glory of the whip!";
        }
    }

    class CrossBow: Item
    {
        public CrossBow()
        {
            Name = "Cross Bow";
            Damage = 30;
            RangeOfHit = 4;
            Symbol = "c";
            Description = "";
        }
    }






}