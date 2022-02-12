using System;
using System.IO;

namespace roguelike_spbu {

    public class GUI
    {
        public string Gui {
            get;
            set;
        }
        public int Height {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public GUI()
        {
            Gui = "";
            Height = 0;
            Width = 0;
        }
        public GUI(string path) {
            Gui = File.ReadAllText(path);
            if (Gui.Length > 0)
                Height = Gui.Split('\n').Length;
            if (Height > 0)
                Width = Gui.Split('\n')[0].Length - 1;
        }
        
    }
}