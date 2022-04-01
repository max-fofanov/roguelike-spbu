using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace roguelike_spbu {

    public static class Saver {
        public static List<string> history = new List<string>();

        public static bool Save(int num = 0) {
            
            Hashtable addresses = new Hashtable();
            addresses.Add("allVisible", GameInfo.allVisible);
            addresses.Add("currentMap", GameInfo.currentMap);
            addresses.Add("history", GameInfo.history);
            addresses.Add("isMusic", GameInfo.isMusic);
            addresses.Add("mapHeight", GameInfo.mapHeight);
            addresses.Add("mapWidth", GameInfo.mapWidth);
            addresses.Add("player", GameInfo.player);

            FileStream fs;

            if (num >= 0) 
                fs = new FileStream("./saves/DataFile" + num + ".dat", FileMode.Create);
            else
                return false;

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, addresses);

                if (num < history.Count)
                    history[num] = "DataFile" + num + ".dat";
                else
                    history.Add("DataFile" + history.Count + ".dat");
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
        public static bool Load(int num = 0)
        {
            Hashtable addresses = new Hashtable();
            FileStream fs;

            if (num >= 0)
                fs = new FileStream("./saves/DataFile" + num + ".dat", FileMode.Open);
            else
                return false;

            /*if (num < history.Count && num >= 0) {
                fs = new FileStream(history[num], FileMode.Open);
            }
            else return false;*/

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                addresses = (Hashtable) formatter.Deserialize(fs);

                GameInfo.allVisible = (bool) addresses["allVisible"];
                GameInfo.currentMap = (int) addresses["currentMap"];
                GameInfo.history = (List<Plane>) addresses["history"];
                GameInfo.isMusic = (bool) addresses["isMusic"];
                GameInfo.mapHeight = (int) addresses["mapHeight"];
                GameInfo.mapWidth = (int) addresses["mapWidth"];
                GameInfo.player = (Player) addresses["player"];

                //SystemInfo.engine = new Engine();
                SystemInfo.engine.ResetVisiblePoints();
                SystemInfo.gui.Print();

                return true;
            }
            catch (SerializationException e)
            {
                fs.Close();
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                return false;
            }
        }

        
    }
}
    
