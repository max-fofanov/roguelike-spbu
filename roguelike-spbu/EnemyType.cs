using System;
using System.Drawing;

namespace roguelike_spbu
{
    class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "g";
            PrimaryForegroundColor = Color.Green;
            Name = "Goblin";
            Stamina = 1;
            ForceType = "neutral";
            Damage = 10;
            HealthPoints = 12;
            CreatureType = "light";
            RangeOfHit = 1;
        }
    }

    class Hobgoblin : Enemy
    {
        public Hobgoblin(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "H";
            PrimaryForegroundColor = Color.Green;
            Name = "Hobgoblin";
            Stamina = 1;
            ForceType = "neutral";
            Damage = 25;
            HealthPoints = 45;
            CreatureType = "normal";
            RangeOfHit = 1;
        }
    }
    
    class GoblinFlinger : Enemy
    {
        public GoblinFlinger(int x, int y) : base(x, y)
        {
            RangeOfView = 11;
            Symbol = "f";
            PrimaryForegroundColor = Color.Green;
            Name = "Goblin Flinger";
            Stamina = 1;
            ForceType = "neutral";
            Damage = 7;
            HealthPoints = 10;
            CreatureType = "light";
            RangeOfHit = 3;
        }
    }

    class Skeleton : Enemy
    {
        public Skeleton(int x, int y) : base(x, y)
        {
            RangeOfView = 8;
            Symbol = "s";
            PrimaryForegroundColor = Color.Gray;
            Name = "Skeleton";
            Stamina = 1;
            ForceType = "dark";
            Damage = 15;
            HealthPoints = 5;
            CreatureType = "light";
            RangeOfHit = 1.5f;
        }
    }
    
    class Zombie : Enemy
    {
        public Zombie(int x, int y) : base(x, y)
        {
            RangeOfView = 6;
            Symbol = "z";
            PrimaryForegroundColor = Color.Brown;
            Name = "Zombie";
            Stamina = 1;
            ForceType = "dark";
            Damage = 5;
            HealthPoints = 20;
            CreatureType = "normal";
            RangeOfHit = 1;
        }
    }
    class DeathKnight : Enemy
    {
        public DeathKnight(int x, int y) : base(x, y)
        {
            Symbol = "N";
            PrimaryForegroundColor = Color.Blue;
            Name = "Death Knight";
            Stamina = 2;
            ForceType = "dark";
            Damage = 10;
            HealthPoints = 20;
            CreatureType = "normal";
            RangeOfHit = 1.5f;
        }
    }
    
    class Lich : Enemy
    {
        public Lich(int x, int y) : base(x, y)
        {
            Symbol = "L";
            PrimaryForegroundColor = Color.Gray;
            Name = "Lich";
            Stamina = 1;
            ForceType = "dark";
            Damage = 10;
            HealthPoints = 5;
            CreatureType = "light";
            RangeOfHit = 3;
        }
    }

    class Demon : Enemy
    {
        public Demon(int x, int y) : base(x, y)
        {
            RangeOfView = 12;
            Symbol = "d";
            PrimaryForegroundColor = Color.Blue;
            Name = "Demon";
            Stamina = 1;
            ForceType = "dark";
            Damage = 22;
            HealthPoints = 66;
            CreatureType = "heavy";
            RangeOfHit = 1.5f;
        }
    
    class Devil : Enemy
    {
        public Devil(int x, int y) : base(x, y)
        {
            RangeOfView = 14;
            Symbol = "D";
            PrimaryForegroundColor = Color.Blue;
            Name = "Devil";
            Stamina = 1;
            ForceType = "dark";
            Damage = 30;
            HealthPoints = 250;
            CreatureType = "heavy";
            RangeOfHit = 2;
        }
    }
    
    class Angel : Enemy
    {
        public Angel(int x, int y) : base(x, y)
        {
            RangeOfView = 12;
            Symbol = "a";
            PrimaryForegroundColor = Color.White;
            Name = "Angel";
            Stamina = 1;
            ForceType = "light";
            Damage = 33;
            HealthPoints = 33;
            CreatureType = "normal";
            RangeOfHit = 2;
        }
    }
    
    class Archangel : Enemy
    {
        public Archangel(int x, int y) : base(x, y)
        {
            RangeOfView = 14;
            Symbol = "A";
            PrimaryForegroundColor = Color.White;
            Name = "Archangel";
            Stamina = 2;
            ForceType = "light";
            Damage = 20;
            HealthPoints = 150;
            CreatureType = "normal";
            RangeOfHit = 2;
        }
    }
    }
}


    
    

    