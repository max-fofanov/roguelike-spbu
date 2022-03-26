namespace roguelike_spbu
{
    ////swords
    class SwordOneHanded: Item
    {
        public SwordOneHanded()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Sword One Handed";
            Damage = 10;
            RangeOfHit = 1;
            //Symbol = "/";
            Description = " ";
        }
    }

    class BastardSword: Item
    {
        public BastardSword()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Bastard Sword";
            Damage = 18;
            RangeOfHit = 1;
            //Symbol = "½";
            Description = " ";
        }
    }

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
            Description = " ";
        }
    }
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
    class Dager: Item
    {
        public Dager()
        {
            Type = ItemType.OneHandWeapon;
            Name = "Dager";
            Damage = 20;
            RangeOfHit = 1;
            Description = "";
        }
    }
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