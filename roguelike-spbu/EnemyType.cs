using System;
using System.Drawing;

namespace roguelike_spbu
{
    [Serializable]
    class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "g";
            PrimaryForegroundColor = Color.DarkGreen;
            Name = "Goblin";
            //Stamina = 1;
            ForceType = "neutral";
            Damage = 10;
            SetHealth(12);
            CreatureType = "light";
            RangeOfHit = 1;
            XP = 1;
            Description = "1) Goblins live underground, some of them sometimes got out, but since there are no living people next to them in the underworld, they also switched to dwarves and began to descend deeper, following them. \n2) Greedy for gold, but unable to mine it or trade, which is why they attack other sentient beings. \n3) Once the goblins were more reasonable, but in the absence of the king they turned into miserable creatures who forgot everything except robbery.\n4) Now they serve the underground king, who he is and where he came from is not clear, but he is definitely the embodiment of vice, otherwise he would never have been able to attract goblins to his side.";
        }
    }
    [Serializable]
    class Hobgoblin : Enemy
    {
        public Hobgoblin(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "H";
            PrimaryForegroundColor = Color.DarkGreen;
            Name = "Hobgoblin";
            //Stamina = 1;
            ForceType = "neutral";
            Damage = 25;
            SetHealth(45);
            CreatureType = "normal";
            RangeOfHit = 1;
            XP = 10;
            Description = "1) Hobgoblins are stronger and more tenacious than usual, ordinary goblins are afraid of them, usually they are the leaders of the tribe. 2) Hobgoblins are much more dangerous than ordinary goblins, they are not only physically strong, but also smarter than their small counterparts.";
        }
    }
    [Serializable]
    class GoblinFlinger : Enemy
    {
        public GoblinFlinger(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "f";
            PrimaryForegroundColor = Color.DarkGreen;
            Name = "Goblin Flinger";
            //Stamina = 1;
            ForceType = "neutral";
            Damage = 7;
            SetHealth(10);
            CreatureType = "light";
            RangeOfHit = 3;
            XP = 5;
            Description = "1) Goblins live underground, some of them sometimes got out, but since there are no living people next to them in the underworld, they also switched to dwarves and began to descend deeper, following them. \n2) They differ from ordinary goblins in the ability to throw daggers, axes and other open objects, but in the rest they differ little.";
        }
    }
    [Serializable]
    class Skeleton : Enemy
    {
        public Skeleton(int x, int y) : base(x, y)
        {
            RangeOfView = 8;
            Symbol = "s";
            PrimaryForegroundColor = Color.Gray;
            Name = "Skeleton";
            //Stamina = 1;
            ForceType = "dark";
            Damage = 15;
            SetHealth(5);
            CreatureType = "light";
            RangeOfHit = 1.5f;
            XP = 2;
            Description = "1) While skeletons and skeleton warriors are average foot soldiers individually, it is possible to build massively populated troops of them. They are numerously produced, are the main creature produced by the Necromancy secondary skill, and other creature types can be converted into them at Necropolis skeleton transformers. \n2) Skeletons have a small number of hp, but in large numbers they represent a high danger due to the damage they deal. \n3) Skeletons are sometimes called a product of necromancy, which is not fully true, because they are not so much different from golems until human mana is placed in them. Of course, skeletons are more inherent in dark magicians, but they are also found in light ones. The idea of ​​the undead and necromancers is outdated at the present time.";
        }
    }
    [Serializable]
    class Zombie : Enemy
    {
        public Zombie(int x, int y) : base(x, y)
        {
            RangeOfView = 4;
            Symbol = "z";
            PrimaryForegroundColor = Color.Brown;
            Name = "Zombie";
            //Stamina = 1;
            ForceType = "dark";
            Damage = 10;
            SetHealth(60);
            CreatureType = "normal";
            RangeOfHit = 1;
            XP = 3;
            Description = "1) Zombies are slow, attacks are weak, but surprisingly they are able to withstand a lot of hits. \n2) The rotting bodies of zombies carry terrible diseases that can significantly weaken those who try to destroy these dead. \n3) The theory of vessels and the science of golems believe that zombies with the rudiments of reason got it because of the carelessness of the magician in the process of creation, from somewhere the living mana was obtained in insufficient quantities to give full functioning, or the vessel was damaged.";
        }
    }
    [Serializable]
    class DeathKnight : Enemy
    {
        public DeathKnight(int x, int y) : base(x, y)
        {
            Symbol = "N";
            PrimaryForegroundColor = Color.Blue;
            Name = "Death Knight";
            //Stamina = 2;
            ForceType = "dark";
            Damage = 18;
            SetHealth(70);
            CreatureType = "normal";
            RangeOfHit = 2;
            XP = 15;
            Description = "1) Death knights - horsemen, faithful servants of the attorney to the master, some of whom were magicians and specialize in creating golems, often they did not go further than the living dead. \n2) They were once human, but many of them voluntarily gave up life for eternal service. High knights are recognized as the crowns of creation of magicians working to improve the human body.";
        }
    }
    [Serializable]
    class Lich : Enemy
    {
        public Lich(int x, int y) : base(x, y)
        {
            Symbol = "L";
            PrimaryForegroundColor = Color.Gray;
            Name = "Lich";
            //Stamina = 1;
            ForceType = "dark";
            Damage = 15;
            SetHealth(40);
            CreatureType = "light";
            RangeOfHit = 4;
            XP = 15;
            Description = "1) Liches are the highest form of undead. In the army of the dead, their main task is to provide fire support to the rest of the troops. They are armed with magical staves, with the help of which they strike enemies with a deadly cloud that destroys all life in long-range combat. \n2) Sometimes they can replace low-level magicians on the battlefield, but they themselves are extremely vulnerable to other people's magic and, as a rule, do not have protective spells.";
        }
    }
    [Serializable]
    class Demon : Enemy
    {
        public Demon(int x, int y) : base(x, y)
        {
            RangeOfView = 12;
            Symbol = "d";
            PrimaryForegroundColor = Color.Blue;
            Name = "Demon";
            //Stamina = 1;
            ForceType = "dark";
            Damage = 33;
            SetHealth(99);
            CreatureType = "heavy";
            RangeOfHit = 2;
            XP = 30;
            Description = "1) The generation of experiments of alchemists and golem magicians, however, the experiment was not successful, moreover, it led to the death of the creators. 2) Unlike other artificial creatures, they can reproduce, but they do this at the expense of splitting the flow of mana and making sacrifices. 3) These generations cannot be controlled, at least no one has managed to do so yet.";
        }
    }
    [Serializable]
    class Devil : Enemy
    {
        public Devil(int x, int y) : base(x, y)
        {
            RangeOfView = 14;
            Symbol = "D";
            PrimaryForegroundColor = Color.Blue;
            Name = "Devil";
            //Stamina = 1;
            ForceType = "dark";
            Damage = 30;
            SetHealth(250);
            CreatureType = "heavy";
            RangeOfHit = 2;
            XP = 66;
            Description = "1) Devils - the leaders of the armies of demons - are not from this world. Devils wear no armor and are strong in melee and ranged combat. Many warriors fall into a panic just at the mere sight of them. In addition, devils are able to open a portal to another territory. \n2) Devils hate angels and even more so archangels, it was they who put up the greatest resistance to the army of demons during the expansion to the northern islands.";
        }
    }
    [Serializable]
    class Angel : Enemy
    {
        public Angel(int x, int y) : base(x, y)
        {
            RangeOfView = 12;
            Symbol = "a";
            PrimaryForegroundColor = Color.White;
            Name = "Angel";
            //Stamina = 1;
            ForceType = "light";
            Damage = 20;
            SetHealth(80);
            CreatureType = "normal";
            RangeOfHit = 2;
            XP = 10;
        }
    }
    [Serializable]
    class Archangel : Enemy
    {
        public Archangel(int x, int y) : base(x, y)
        {
            RangeOfView = 14;
            Symbol = "A";
            PrimaryForegroundColor = Color.White;
            Name = "Archangel";
            //Stamina = 2;
            ForceType = "light";
            Damage = 40;
            SetHealth(200);
            CreatureType = "normal";
            RangeOfHit = 2;
            XP = 30;
            Description = "1) Archangels are the protectors of the human world. As a rule, these are the holy warriors of the old world, thanks to their prayers, who received inhuman abilities and a long life, in exchange they completely devote themselves to the service of God. \n2) If angels are still small people and who find themselves there only because of their origin, then the archangels are a fighting aristocracy, their temper is different, but people respect and a certain awe for each of them. \n3) Only three archangels were able to help hold half of the northern islands for 5 months and prevent the demonic army from passing further. It is a pity that the island magicians were scattered and most of them fled from their native lands.";
        }
    }
}



    
    

    