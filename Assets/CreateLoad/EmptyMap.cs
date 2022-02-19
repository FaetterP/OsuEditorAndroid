
namespace Assets.CreateLoad
{
    class EmptyMap
    {
        public static string GetText(string audioFilename, string title, string titleUnicode, string artist, string artistUnicode, string creator, string version, string source, string tags, string background)
        {
            string ret =
                "osu file format v14\n\n" +
                "[General]\n" +
                "AudioFilename: " + audioFilename + "\n" +
                "AudioLeadIn: 0\n" +
                "PreviewTime: 0\n" +
                "Countdown: 0\n" +
                "SampleSet: Soft\n" +
                "StackLeniency: 0.7\n" +
                "Mode: 0\n" +
                "LetterboxInBreaks: 0\n" +
                "WidescreenStoryboard: 1\n" +
                "\n" +
                "[Editor]\n" +
                "Bookmarks:\n" +
                "DistanceSpacing: 1\n" +
                "BeatDivisor: 4\n" +
                "GridSize: 4\n" +
                "TimelineZoom: 1.5\n" +
                "\n" +
                "[Metadata]\n" +
                "Title:" + title + "\n" +
                "TitleUnicode:" + titleUnicode + "\n" +
                "Artist:" + artist + "\n" +
                "ArtistUnicode:" + artistUnicode + "\n" +
                "Creator:" + creator + "\n" +
                "Version:" + version + "\n" +
                "Source:" + source + "\n" +
                "Tags:" + tags + "\n" +
                "BeatmapID:-1\n" +
                "BeatmapSetID:-1\n" +
                "\n" +
                "[Difficulty]\n" +
                "HPDrainRate:5\n" +
                "CircleSize:5\n" +
                "OverallDifficulty:5\n" +
                "ApproachRate:5\n" +
                "SliderMultiplier:1\n" +
                "SliderTickRate:1\n" +
                "\n" +
                "[Events]\n" +
                "//Background and Video events\n" +
                "0,0,\"" + background + "\",0,0\n" +
                "//Break Periods\n" +
                "//Storyboard Layer 0 (Background)\n" +
                "//Storyboard Layer 1 (Fail)\n" +
                "//Storyboard Layer 2 (Pass)\n" +
                "//Storyboard Layer 3 (Foreground)\n" +
                "//Storyboard Sound Samples\n" +
                "\n" +
                "[TimingPoints]\n" +
                "0,600,4,1,0,100,1,0\n" +
                "\n" +
                "[Colours]\n" +
                "Combo1 : 224,255,255\n" +
                "Combo2 : 230,230,250\n" +
                "\n" +
                "[HitObjects]\n";

            return ret;
        }
    }
}
