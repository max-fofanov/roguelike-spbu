namespace roguelike_spbu
{
    class Helmet : Item
    {
        public Helmet()
        {
            Type = ItemType.Armor;
            Name = "Helmet";
            Defence = 999;
            //Symbol = "п";
            Description = "it's a helmet, lol";
        }
    }
    //sword
    class SwordOneHanded: Item
    {
        
        public SwordOneHanded()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Sword One Handed";
            Damage = 10;
            RangeOfHit = 1;
            //Symbol = "/";
            Description = "The most ordinary, unremarkable cheap sword. Yes, it is almost useless, but without it it would be even worse.";
        }
    }
    [Serializable]
    class BastardSword: Item
    {
        public BastardSword()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Bastard Sword";
            Damage = 16;
            RangeOfHit = 1;
            Defence = 1;
            //Symbol = "½";
            Description = "A bastard sword is already much better than usual, though you need to be able to handle it at least a little, you know - to stab with a sharp end.";
        }
    }
    [Serializable]
    class TwoHandedSword: Item
    {
        public TwoHandedSword()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Two Handed Sword";
            Damage = 35;
            RangeOfHit = 2;
            //Symbol = "!";
            Defence = 2;
            Description = "A good two-handed sword, a weapon for good or large warriors. Why does it bring back memories...";
        }
    }
    [Serializable]
    class Rapier: Item
    {
        public Rapier()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Rapier";
            Damage = 23;
            RangeOfHit = 2;
            Description = "";
        }
    }
    [Serializable]
    class Aerondight: Item
    {
        public Aerondight()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Aerondight";
            Damage = 60;
            RangeOfHit = 3;
            Defence = 5;
            //Symbol = "@";
            Description = " ";
        }
    }
    [Serializable]
    class Dager: Item
    {
        public Dager()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Dager";
            Damage = 20;
            RangeOfHit = 1;
            Description = "";
            Defence = 2;
        }
    }
    [Serializable]
    class Frostmourne: Item
    {
        public Frostmourne()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Frostmourne";
            Damage = 100;
            RangeOfHit = 1;
            //Symbol = "F";
            Description = "For the glory of the whip!";
        }
    }
    //////Ranged weapon
    [Serializable]
    class Mjolnir: Item
    {
        public Mjolnir()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Mjolnir";
            Damage = 40;
            RangeOfHit = 4;
            //Symbol = "%";
            Description = " ";
        }
    }
    [Serializable]
    class CrossBow: Item
    {
        public CrossBow()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Cross Bow";
            Damage = 30;
            RangeOfHit = 4;
            //Symbol = "c";
            Description = "";
        }
    }
    [Serializable]
    class Bow: Item
    {
        public Bow()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Bow";
            Damage = 12;
            RangeOfHit = 4;
            //Symbol = ")";
            Description = " ";
        }
    }
    [Serializable]
    class Vijaya: Item
    {
        public Vijaya()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Vijaya";
            Damage = 88;
            RangeOfHit = 4;
            Description = " ";
        }
    }
    /// Magic
    [Serializable]
    class MagicBolt: Item
    {
        public MagicBolt()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Magic Bolt";
            Damage = 15;
            RangeOfHit = 4;
            //Symbol = "~";
            Description = " ";
        }
    }
    [Serializable]
    class Flamestrike: Item //all enemy
    {
        public Flamestrike()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Flamestrike";
            Damage = 40;
            RangeOfHit = 4;
            Description = " ";
        }
    }
    [Serializable]

    class MagicLince: Item
    {
        public MagicLince()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Magic Lince";
            Damage = 25;
            RangeOfHit = 4;
            //Symbol = "-";
            Description = " ";
        }
    }
    [Serializable]
    class MagicStrike: Item //all enem
    {
        public MagicStrike()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Magic Strike";
            Damage = 25;
            RangeOfHit = 3;
            //Symbol = "*";
            Description = " ";
        }
    }
    
    //Different
    [Serializable]
    class EnchantedAxe: Item
    {
        public EnchantedAxe()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Enchanted Axe";
            Damage = 45;
            RangeOfHit = 1;
            Description = "";
        }
    }
    [Serializable]
    class Mace: Item
    {
        public Mace()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Mace";
            Damage = 35;
            RangeOfHit = 1;
            Description = "";
        }
    }
    [Serializable]
    class Spear: Item
    {
        public Spear()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Spear";
            Damage = 23;
            RangeOfHit = 3;
            Defence = 8;
            //Symbol = "|";
            Description = " ";
        }
    }
    [Serializable]
    class LanceOfLonginus: Item
    {
        public LanceOfLonginus()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Lance of Longinus";
            Damage = 66;
            RangeOfHit = 3;
            Defence = 6;
            Description = "";
        }
    }
    ///shields
    [Serializable]
    class Shield: Item
    {
        public Shield()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Shield";
            Damage = 8;
            RangeOfHit = 1;
            Defence = 10;
            Description = "";
        }
    }
    [Serializable]
    class Buckler: Item
    {
        public Buckler()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Buckler";
            Damage = 15;
            RangeOfHit = 2;
            Defence = 5;
            Description = "";
        }
    }
    [Serializable]
    class BigShield: Item
    {
        public BigShield()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Big Shield";
            Damage = 5;
            RangeOfHit = 1;
            Defence = 20;
            Description = "";
        }
    }
    [Serializable]
    class UltimateShield: Item
    {
        public UltimateShield()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Ultimate Shield";
            Damage = 0;
            RangeOfHit = 1;
            Defence = 35;
            Description = "";
        }
    }
    [Serializable]
    class Aegis: Item
    {
        public Aegis()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Aegis";
            Damage = 25;
            RangeOfHit = 2;
            Defence = 50;
            Description = "";
        }
    }

    








}