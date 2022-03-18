using System;

namespace roguelike_spbu {
    public static class CheatConsole {
        
        public static void Cheat(Engine engine) {
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key) {
                case ConsoleKey.H:
                    engine.player.HealthPoints = 999999;
                    break;
                case ConsoleKey.A:
                    engine.player.Damage = 999999;
                    break;
                case ConsoleKey.K:
                    engine.entities.ForEach(e => e.HealthPoints = 0);
                    break;
                case ConsoleKey.P:
                    engine.entities.ForEach(e => e.Damage = 0);
                    break;
                default:
                    
                    engine.player.PrimaryForegroundColor = new System.Drawing.Color[] { System.Drawing.Color.AliceBlue, 
                    System.Drawing.Color.Yellow, System.Drawing.Color.Brown, System.Drawing.Color.Violet, System.Drawing.Color.LightPink}[new Random().Next(5)];
                    break;

            }
        }
    }
}