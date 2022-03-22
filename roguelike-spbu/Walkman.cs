using NetCoreAudio;

namespace roguelike_spbu {
    public static class  Walkman {

        static NetCoreAudio.Player walkman = new NetCoreAudio.Player();

        private static string cT = "./sounds/Hub/Menu theme. Mustin - Serenity (Final Fantasy VII Main Theme).mp3";

        public static string CurrentTrack {
            set {

                cT = value; 
            }
            get {
                return cT;
            }
        }

        public static bool IsPlaying {
            get {return walkman.Playing; }
        }

        public static bool IsPaused {
            get {return !walkman.Playing; }
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

        public static void Pause() {
            if (walkman.Playing) walkman.Pause();
        }

        public static void Resume() {
            if (walkman.Paused) walkman.Resume();
        }

    
        


    }
}