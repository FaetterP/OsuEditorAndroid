using Assets.Elements;
using Assets.MapInfo;
using Assets.OsuEditor;
using Assets.Utilities;

namespace Assets
{
    class Global
    {
        public static int MusicLength = 1;
        public static int MusicTime = 0;
        
        public static MapClass Map = new MapClass();
        public static int AR_ms
        {
            get
            {
                return Map.Difficulty.AR_ms;
            }
        }

        public static string FullPathToMapFolder = @"C:\Users\FaetterP\AppData\LocalLow\faetterp\osueditor\beatmap-637254798315041883-NieR Automata OST Amusement Park Dynamic Vocals/";
        public static string FullPathToMap = @"C:\Users\FaetterP\AppData\LocalLow\faetterp\osueditor\beatmap-637254798315041883-NieR Automata OST Amusement Park Dynamic Vocals/NieRAutomata - Amusement park (FaetterP) [Easy].osu";

        public static LeftStatus? LeftStatus = null;
        public static OsuHitObject SelectedHitObject;
        public static SliderStatus? SliderStatus = null; 
    }
}
