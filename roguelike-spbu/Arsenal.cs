namespace roguelike_spbu
{
    class Helmet : Item
    {
        public Helmet()
        {
            Type = ItemType.Armor;
            Name = "Helmet";
            Defence = 999;
            Symbol = "п";
            Description = "it's a helmet, lol";
        }
    }
    class SwordOneHanded: Item
    {
        public SwordOneHanded()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Sword One Handed";
            Damage = 15;
            RangeOfHit = 1; // 1
            Symbol = "/";
            Description = " ";
        }
    }

    class BastardSword: Item
    {
        public BastardSword()
        {
            Type = ItemType.TwoHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
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
            Type = ItemType.OneHandWeapon;
            Name = "Cross Bow";
            Damage = 30;
            RangeOfHit = 4;
            Symbol = "c";
            Description = "";
        }
    }






}