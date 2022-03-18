using NetCoreAudio;

namespace roguelike_spbu {
    public static class  Walkman {

        static NetCoreAudio.Player walkman = new NetCoreAudio.Player();

        private static string cT = "./sounds/Traffic.wav";

        public static string CurrentTrack {
            set {

                cT = value; 
            }
            get {
                return cT;
            }
        }

        public static void Play() {
            if (walkman.Playing) walkman.Stop();
            walkman.Play(CurrentTrack);

        }
        public static void Play(String path) {
            CurrentTrack = path;
            Walkman.Play();
        }

        public static void Stop() {
            if (walkman.Playing) walkman.Stop();
        }


    }
}