using System;
using System.Globalization;

namespace roguelike_spbu
{
    public struct borders {
        public int LeftEvent;
        public int RightEvent;
        public float border;
    }
    static public class Walker
    {
        public static int Alias(float[] probabilities)
        {
            float[] table = new float[probabilities.Length];
            probabilities.CopyTo(table, 0);

            float fullchance = table.Sum();

            int quantity = table.Length;

            float cell = fullchance / quantity;
            int minindex = 0;
            int maxindex = 0;
            borders[] bord = new borders[quantity];

            for (int i = 0; i < quantity; i++)
            {
                float lokmin = float.MaxValue;
                float lokmax = -1;
                for (int d = 0; d < quantity; d++)
                {
                    if (table[d] < lokmin && table[d] != 0)
                    {
                        lokmin = table[d];
                        minindex = d;
                    }

                    if (table[d] > lokmax && table[d] != 0)
                    {
                        lokmax = table[d];
                        maxindex = d;
                    }

                }
                bord[i].border = i * cell + lokmin;
                bord[i].LeftEvent = minindex;
                bord[i].RightEvent = maxindex;
                
                table[maxindex] -= cell - lokmin;
                table[minindex] = 0;

            }

            string[] tokens = fullchance.ToString("G", CultureInfo.InvariantCulture).Split(".");
            int length = tokens.Length > 1 ? tokens[1].Length : 0;

            int intfullchance = (int)(fullchance * (float)Math.Pow(10, length));

            Random rnd = new Random();

            float r = rnd.Next(0, intfullchance);

            r /= (float) Math.Pow(10, length);

            int cellnumb = (int) (r * quantity / fullchance);
            return r < bord[cellnumb].border ?  bord[cellnumb].LeftEvent : bord[cellnumb].RightEvent;
        }
    }
}