namespace roguelike_spbu
{
    [Serializable]
    class LeatherArmor : Item
    {
        public LeatherArmor()
        {
            Type = ItemType.Armor;
            Name = "Leather Armor";
            Defence = 2;
            Description = "The most common riveted leather armor protects the torso well, but it's a pity that the legs and arms are almost not covered.";
        }
    }
    [Serializable]
    class ChainArmor : Item
    {
        public ChainArmor()
        {
            Type = ItemType.Armor;
            Name = "ChainArmor";
            Defence = 5;
            Description = "The chain mail is strong and protects well from piercing blows, but it is unlikely to withstand the blow of a hobgoblin's club.";
        }
    }
    [Serializable]
    class PlateArmor : Item
    {
        public PlateArmor()
        {
            Type = ItemType.Armor;
            Name = "Plate Armor";
            Defence = 10;
            Description = "Excellent knightly armor, strong and reliable in some families, it serves several generations, which means that they successfully survived in it.";
        }
    }
    [Serializable]
    class EnchantedMithrilArmor : Item
    {
        public EnchantedMithrilArmor()
        {
            Type = ItemType.Armor;
            Name = "Enchanted Mithril Armor";
            Defence = 20;
            Description = "A magnificent creation of the dwarves who mined the extremely rare mithril metal, which has a huge magical and combat potential. Only dwarves know how to process it, but in addition, powerful spells were cast on her.";
        }
    }
    [Serializable]
    class MoonguardPlate : Item
    {
        public MoonguardPlate()
        {
            Type = ItemType.Armor;
            Name = "Moonguard Plate";
            Defence = 30;
            Description = "Armor of the Order of the Knights of the Defenders of the Moon. It is unknown what it is made of, it is assumed that it is a pure creation of mana, created by the Vicar of God himself.";
        }
    }
}