using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace roguelike_spbu {

    public static class Saver {
        public static bool Save() {
            
            Hashtable addresses = new Hashtable();
            addresses.Add("allVisible", GameInfo.allVisible);
            addresses.Add("currentMap", GameInfo.currentMap);
            addresses.Add("entities", GameInfo.entities);
            addresses.Add("history", GameInfo.history);
            addresses.Add("isMusic", GameInfo.isMusic);
            addresses.Add("mapHeight", GameInfo.mapHeight);
            addresses.Add("mapWidth", GameInfo.mapWidth);
            addresses.Add("player", GameInfo.player);

            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, addresses);
                fs.Close();
                return true;
            }
            catch (SerializationException e)
            {
                fs.Close();
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                return false;
            }
            
        }
    

        public static bool Load()
        {
            Hashtable addresses  = null;

            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                addresses = (Hashtable) formatter.Deserialize(fs);
                return true;
            }
            catch (SerializationException e)
            {
                fs.Close();
                return false;
            }
        }
    }
}
    
