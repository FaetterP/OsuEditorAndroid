using Assets.Elements;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.MapInfo
{
    class MapClass
    {
        public General General = new General();
        public Editor Editor = new Editor();
        public Metadata Metadata = new Metadata();
        public Difficulty Difficulty = new Difficulty();
        public Events Events = new Events();
        public List<TimingPoint> TimingPoints = new List<TimingPoint>();
        public List<Color> Colors = new List<Color>();
        public List<OsuHitObject> OsuHitObjects = new List<OsuHitObject>();

        private Dictionary<int, ComboInfo> _comboInfos = new Dictionary<int, ComboInfo>();

        public void UpdateComboInfos()
        {
            _comboInfos.Clear();
            OsuHitObjects.Sort();
            int color_num = 0, number = 1;
            foreach (OsuHitObject t in OsuHitObjects)
            {
                if (t is OsuCircle && !(t is OsuSlider))
                {
                    int sum_color = (t as OsuCircle).combo_sum;
                    (t as OsuCircle).combo_sum = sum_color;
                    if (sum_color == 1) 
                    {
                        number++;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                    else if (sum_color == 5) 
                    {
                        color_num++; 
                        color_num %= Global.Map.Colors.Count;
                        number = 1;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                    else
                    {
                        color_num += (sum_color / 16) + 1;
                        color_num %= Global.Map.Colors.Count;
                        number = 1;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                }
                else if (t is OsuSlider)
                {
                    int sum_color = (t as OsuSlider).combo_sum;
                    (t as OsuSlider).combo_sum = sum_color;
                    if (sum_color == 2) 
                    {
                        number++;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                    else if 
                        (sum_color == 6) 
                    {
                        color_num++;
                        color_num = color_num % Global.Map.Colors.Count;
                        number = 1;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                    else 
                    {
                        color_num += (sum_color / 16) + 1;
                        color_num = color_num % Global.Map.Colors.Count;
                        number = 1;
                        ComboInfo toAdd = new ComboInfo(number, color_num);
                        _comboInfos.Add(t.Time, toAdd);
                    }
                }
                else if(t is OsuSpinner)
                {
                    ComboInfo toAdd = new ComboInfo(9999, 0);
                    _comboInfos.Add(t.Time, toAdd);
                }
            }
        }

        public ComboInfo GetComboInfo(int time)
        {
            return _comboInfos[time];
        }

        private string GetMapTXT()
        {
            string ret =
                "osu file format v14\n\n" +
                "[General]\n" +
                "AudioFilename: " + General.AudioFilename + "\n" +
                "AudioLeadIn: " + General.AudioLeadIn + "\n" +
                "PreviewTime: " + General.PreviewTime + "\n" +
                "Countdown: " + General.Countdown + "\n" +
                "SampleSet: " + General.SampleSet + "\n" +
                "StackLeniency: " + General.StackLeniency + "\n" +
                "Mode: " + General.Mode + "\n" +
                "LetterboxInBreaks: " + General.LetterboxInBreaks + "\n" +
                "WidescreenStoryboard: " + (General.WidescreenStoryboard ? 1 : 0) + "\n" +
                "\n" +
                "[Editor]\n" +
                "Bookmarks:\n" +
                "DistanceSpacing: " + Editor.DistanceSpacing + "\n" +
                "BeatDivisor: " + Editor.BeatDivisor + "\n" +
                "GridSize: " + Editor.GridSize + "\n" +
                "TimelineZoom: " + Editor.TimelineZoom + "\n" +
                "\n" +
                "[Metadata]\n" +
                "Title:" + Metadata.Title + "\n" +
                "TitleUnicode:" + Metadata.TitleUnicode + "\n" +
                "Artist:" + Metadata.Artist + "\n" +
                "ArtistUnicode:" + Metadata.ArtistUnicode + "\n" +
                "Creator:" + Metadata.Creator + "\n" +
                "Version:" + Metadata.Version + "\n" +
                "Source:" + Metadata.Source + "\n" +
                "Tags:" + Metadata.Tags + "\n" +
                "BeatmapID:-1\n" +
                "BeatmapSetID:-1\n" +
                "\n" +
                "[Difficulty]\n" +
                "HPDrainRate:" + Difficulty.HPDrainRate + "\n" +
                "CircleSize:" + Difficulty.CircleSize + "\n" +
                "OverallDifficulty:" + Difficulty.OverallDifficulty + "\n" +
                "ApproachRate:" + Difficulty.ApproachRate + "\n" +
                "SliderMultiplier:" + Difficulty.SliderMultiplier + "\n" +
                "SliderTickRate:" + Difficulty.SliderTickRate + "\n" +
                "\n" +
                "[Events]\n" +
                "//Background and Video events\n" +
                "0,0,\"" + Events.BackgroungImage + "\",0,0\n" +
                "//Break Periods\n" +
                "//Storyboard Layer 0 (Background)\n" +
                "//Storyboard Layer 1 (Fail)\n" +
                "//Storyboard Layer 2 (Pass)\n" +
                "//Storyboard Layer 3 (Foreground)\n" +
                "//Storyboard Sound Samples\n" +
                "\n" +
                "[TimingPoints]\n";
            foreach(var t in TimingPoints)
            {
                ret += t.ToString() + "\n";
            }
            ret += "\n" +
             "[Colours]\n";
            int i = 1;
            foreach(var t in Colors)
            {
                ret += "Combo" + i + " : " + (int)(t.r * 255) + "," + (int)(t.g * 255) + "," + (int)(t.b * 255) + "\n";
                i++;
            }
            ret += "\n" +
            "[HitObjects]\n";
            foreach(var t in OsuHitObjects)
            {
                ret += t.ToString()+"\n";
            }

            return ret;
        }
        public void SaveToFile()
        {
            StreamWriter sw = new StreamWriter(Global.FullPathToMap);
            sw.Write(GetMapTXT());
            sw.Close();
        }

        public ReadOnlyCollection<TimingPoint> GetParentTimingPoints()
        {
            List<TimingPoint> ret = new List<TimingPoint>();
            foreach(var point in TimingPoints)
            {
                if (point.isParent)
                    ret.Add(point);
            }
            return ret.AsReadOnly();
        }
    }
}
