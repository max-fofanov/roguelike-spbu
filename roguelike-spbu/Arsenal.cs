namespace roguelike_spbu
{
    [Serializable]
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
    [Serializable]
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
            Description = "The rapier is certainly not the best weapon for war, but certainly better than a rusty sword.";
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
            Description = "A sword obtained from a powerful water spirit, which was called the Lady of the Lake. I wonder what the White Wolf has to do with it ...";
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
            Description = "The weapon of robbers and assassins, but in skillful hands, has not only a good attack, but also provides a good opportunity to protect.";
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
            Description = "<A hammer that only the worthy will lift>, they said, and in the end this thing had to be taken from the hobgoblin before he killed me with it.";
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
            Description = "A crossbow is almost like a bow, only more powerful, easier to handle, and it’s also easier for them to learn how to control it... - in general, it’s better.";
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
            Description = "The bow is an extremely common weapon on the new and old continent, only if it slowly dies off in the old one, then in the new one it does not plan to lose ground. The elves are especially good with him, the dwarves don’t really use this, and no one saw the invaders from the north (demons) with him at all.";
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
            Description = "Vijaya, the bow of the legendary prince of the wood elves, it is even difficult to say that he forgot here, but they say a sylph lives inside him.";
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
            Description = "Magic bolt, hits weakly, but far - basic magic.";
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
            Description = "A powerful and extremely dangerous spell that brings many problems to the founders of competitions and fighters in the arenas of the Southern Empire. \n<Flamestrike will leave the arena, the spell will be rare!>";
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
            Description = "A middle-class spell, somewhat reminiscent of a spear, may have aspects of different elements, but it is said to be the most dangerous in its pure form, although magicians capable of this can be counted on the fingers of an unborn thief.";
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
            Description = "A powerful spell with a circular attack that hits all enemies in the radius of destruction, weaker than high magic focused on one target, but not inferior to the average one.";
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
            Description = "An enchanted ax from the northern lands of the old mainland, it is difficult to say what kind of symbols are engraved on it. You can see the sign of the moon, but the old religion does not recognize the influence of Thealnebris...";
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
            Description = "A good weapon that can kill even a knight in armor.";
        }
    }
    [Serializable]
    class Spear: Item
    {
        public Spear()
        {
            Type = ItemType.TwoHandWeapon;
            Name = "Spear";
            Damage = 22;
            RangeOfHit = 3;
            Defence = 5;
            //Symbol = "|";
            Description = "The most common and dangerous weapon in the army, with good discipline and the right order, capable of breaking the onslaught of any army.";
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
            Description = "A legendary weapon sent to the hero of the New Faith to protect her, a long, ornate spear that oozes massive amounts of mana.";
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
            Description = "The most common one-handed shield, relatively comfortable and relatively good protection, moreover, it is quite possible to hit with it.";
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
            Description = "A good shield for duels or active combat against piercing weapons, allowing for good maneuverability and moderate protection.";
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
            Description = "A large two-handed shield covering most of the body.";
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
            Description = "A huge shield that completely protects the body, but is so heavy that it is impossible to hit them.";
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
            Description = "The shield of the ancient sagas of people who once lived on the southern islands of the old world.";
        }
    }

    








}