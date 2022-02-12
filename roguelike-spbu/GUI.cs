using System;
using System.IO;

namespace roguelike_spbu {

    public class GUI {
        
        
        public string Gui {
            get;
            set;
        }

        public GUI(string path) {
            
            Gui = File.ReadAllText(path);
        }

        
    }
}