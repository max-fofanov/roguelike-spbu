using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace roguelike_spbu {

    public static class Statistics {

        public static Hashtable statistics = new Hashtable();
        
        public static void InvokeCtor() {}

        static Statistics() {

            if (! LoadStatistics()) {
                statistics["killed"] = 0;
                statistics["deaths"] = 0;
                statistics["maxLevel"] = 0;
                statistics["timeInGame"] = 0L;
            }

            GameInfo.startTime = DateTime.Now.Ticks;

        }

        public static bool SaveStatistics() {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("statistics.dat", FileMode.Create);

            try
            {
                formatter.Serialize(fs, statistics);
                fs.Close();
                return true;
            }
            catch (SerializationException e)
            {
                fs.Close();
                return false;
            }
        }

        public static bool LoadStatistics() {
            
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream("statistics.dat", FileMode.Open);

                statistics = (Hashtable) formatter.Deserialize(fs);

                return true;
            }
            catch (FileNotFoundException e) {
                return false;
            }
            catch (SerializationException e)
            {
                return false;
            }
            
        }




    }
}