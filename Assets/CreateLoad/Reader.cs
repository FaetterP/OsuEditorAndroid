using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Assets.Utilities;
using UnityEngine;
using Assets.Elements;
using Assets.MapInfo;

namespace Assets.CreateLoad
{
    class Reader
    {
        private static string[] lines;
        private static List<string> forMapParse;
        public static void LoadMapFromFile(string path)
        {
            forMapParse = new List<string>();
            lines = File.ReadAllLines(path);
            MapClass map = Global.Map;


            map.General.AudioFilename = GetValue("AudioFilename:");
            map.General.AudioLeadIn = int.Parse(GetValue("AudioLeadIn:"));
            map.General.PreviewTime = int.Parse(GetValue("PreviewTime:"));
            map.General.Countdown = int.Parse(GetValue("Countdown:"));
            map.General.SampleSet = GetValue("SampleSet:");
            map.General.StackLeniency = double.Parse(GetValue("StackLeniency:"));
            map.General.Mode = int.Parse(GetValue("Mode:"));
            map.General.LetterboxInBreaks = int.Parse(GetValue("LetterboxInBreaks:"));
            map.General.WidescreenStoryboard = int.Parse(GetValue("WidescreenStoryboard:"))==1;
            map.Editor.Bookmarks = new List<int>();
            if (GetValue("Bookmarks:") != null)
            {
                foreach (var t in GetValue("Bookmarks:").Split(','))
                {
                    if (t == "") { break; }
                    map.Editor.Bookmarks.Add(int.Parse(t));
                }
            }
            map.Editor.DistanceSpacing = double.Parse(GetValue("DistanceSpacing:"));
            map.Editor.BeatDivisor = int.Parse(GetValue("BeatDivisor:"));
            map.Editor.GridSize = int.Parse(GetValue("GridSize:"));
            map.Editor.TimelineZoom = double.Parse(GetValue("TimelineZoom:"));

            map.Metadata.Title = GetValue("Title:");
            map.Metadata.TitleUnicode = GetValue("TitleUnicode:");
            map.Metadata.Artist = GetValue("Artist:");
            map.Metadata.ArtistUnicode = GetValue("ArtistUnicode:");
            map.Metadata.Creator = GetValue("Creator:");
            map.Metadata.Version = GetValue("Version:");
            map.Metadata.Source = GetValue("Source:");
            map.Metadata.Tags = GetValue("Tags:");
            map.Metadata.BeatmapID = int.Parse(GetValue("BeatmapID:"));
            map.Metadata.BeatmapSetID = int.Parse(GetValue("BeatmapSetID:"));

            map.Difficulty.HPDrainRate = double.Parse(GetValue("HPDrainRate:"));
            map.Difficulty.CircleSize = double.Parse(GetValue("CircleSize:"));
            map.Difficulty.OverallDifficulty = double.Parse(GetValue("OverallDifficulty:"));
            map.Difficulty.ApproachRate = double.Parse(GetValue("ApproachRate:"));
            map.Difficulty.SliderMultiplier = double.Parse(GetValue("SliderMultiplier:"));
            map.Difficulty.SliderTickRate = double.Parse(GetValue("SliderTickRate:"));

            string[] bg_string_arr = GetValue("0,0,").Split(',');
            map.Events.BackgroungImage = bg_string_arr[0].Remove(bg_string_arr[0].Length - 1, 1).Remove(0, 1);
            map.Events.xOffset = int.Parse(bg_string_arr[1]);
            map.Events.yOffset = int.Parse(bg_string_arr[2]);
            List<string> raw_sb = GetBlock("[Events]").Split('\n').ToList();
            raw_sb.RemoveAt(0);
            string sb = "";
            foreach(var t in raw_sb)
            {
                sb += t + "\n";
            }
            map.Events.storyboard = sb;

            map.TimingPoints = new List<TimingPoint>();
            foreach (var t in GetBlock("[TimingPoints]").Split('\n'))
            {
                map.TimingPoints.Add(ConvertTimingPoint(t));
            }

            map.Colors = new List<Color>();
            if (GetBlock("[Colours]") != "")
            {
                foreach (var t in GetBlock("[Colours]").Split('\n'))
                {
                    string[] color_arr = t.Split(' ').Last().Split(',');
                    Color added = new Color(int.Parse(color_arr[0]) / 255f, int.Parse(color_arr[1]) / 255f, int.Parse(color_arr[2]) / 255f);
                    map.Colors.Add(added);
                }
            }
            else
            {
                map.Colors.Add(new Color(224 / 255f, 255 / 255f, 255 / 255f));
                map.Colors.Add(new Color(230 / 255f, 230 / 255f, 250 / 255f));
            }

            map.OsuHitObjects = new List<OsuHitObject>();
            circle = (Resources.Load("OsuCircle") as GameObject).GetComponent<OsuCircle>();
            slider = (Resources.Load("OsuSlider") as GameObject).GetComponent<OsuSlider>();
            spinner = (Resources.Load("OsuSpinner") as GameObject).GetComponent<OsuSpinner>();
            foreach (var t in GetBlock("[HitObjects]").Split('\n'))
            {
                if (t.Contains('|')) { AddSlider(t); }
                else if (t.Split(',').Length == 6) { AddCircle(t); }
                else if (t.Split(',').Length == 7) { AddSpinner(t); }
            }

            map.UpdateComboColours();
            map.UpdateNumbers();
        }

        private static string GetValue(string field)
        {
            foreach(var t in lines)
            {
                if (t.StartsWith(field))
                {
                    string ret = t.Remove(0, field.Length);
                    if (ret.StartsWith(" ")) { ret = ret.Remove(0, 1); }
                    return ret;
                }
            }
            return null;
        }

        private static string GetBlock(string block)
        {
            string ret = "";
            bool isNeed = false;
            foreach(var t in lines)
            {
                if (t.StartsWith("[")) { isNeed = false; }

                if (isNeed && t!="" && !t.StartsWith("//"))
                {
                    ret += t + "\n";
                }
               
                if(t == block)
                {
                    isNeed = true;
                }
            }
            if (ret != "") { ret=ret.Remove(ret.Length - 1, 1); }
            return ret;
        }

        private static double last_parent_lenght = -1;
        private static TimingPoint ConvertTimingPoint(string tp)
        {
            string[] param = tp.Split(',');

            TimingPoint ret = new TimingPoint();
            ret.Offset = int.Parse(param[0]);

            double divisor = double.Parse(param[1]);
            if (divisor > 0)
            {
                ret.isParent = true;
                ret.BeatLength = divisor;
                ret.Mult = 1;
                last_parent_lenght = divisor;
            }
            else
            {
                ret.isParent = false;
                ret.BeatLength = last_parent_lenght;
                ret.Mult = -100 / divisor;
            }
            ret.Meter = int.Parse(param[2]);
            ret.SampleSet = int.Parse(param[3]);
            ret.SampleIndex = int.Parse(param[4]);
            ret.Volume = int.Parse(param[5]);
            ret.Kiai = param[7] == "1";

            return ret;
        }

        private static OsuCircle circle;
        private static OsuSlider slider;
        private static OsuSpinner spinner;
        private static void FillHitObjects()
        {
            Global.Map.OsuHitObjects = new List<OsuHitObject>();
            var circlego = Resources.Load("OsuCircle") as GameObject;
            var slidergo = Resources.Load("OsuSlider") as GameObject;
            var spinnergo = Resources.Load("OsuSpinner") as GameObject;
            circle = circlego.GetComponent<OsuCircle>();
            slider = slidergo.GetComponent<OsuSlider>();
            spinner = spinnergo.GetComponent<OsuSpinner>();

            foreach (string t in forMapParse)
            {
                if (t.Contains('|')) { AddSlider(t); }
                else if (t.Split(',').Length == 6) { AddCircle(t); }
                else if (t.Split(',').Length == 7) { AddSpinner(t); }
            }

            // foreach (var t in Global.Map.OsuHitObjects) { Debug.Log(t.GetType()); }
            // Debug.Log(Global.Map.OsuHitObjects.Count);
        }


        private static void AddCircle(string line)
        {
            string[] tt = line.Split(',');
            OsuCircle added = (OsuCircle)circle.Clone();
            added.x = int.Parse(tt[0]);
            added.y = int.Parse(tt[1]);
            added.time = int.Parse(tt[2]);
            added.combo_sum = int.Parse(tt[3]);

            int num = int.Parse(tt[4]);
            if (num >= 8) { added.clap = true; num -= 8; }
            if (num >= 4) { added.finish = true; num -= 4; }
            if (num >= 2) { added.whisle = true; }

            string[] arr = tt[5].Split(':');
            added.Sampleset = int.Parse(arr[0]);
            added.Additions = int.Parse(arr[1]);
            Global.Map.OsuHitObjects.Add(added);
        }

        private static void AddSlider(string line)
        {

            string[] tt = line.Split(',');
            OsuSlider added = (OsuSlider)slider.Clone();
            added.x = int.Parse(tt[0]);
            added.y = int.Parse(tt[1]);
            added.time = int.Parse(tt[2]);
            added.combo_sum = int.Parse(tt[3]);
            added.SliderPoints = new List<SliderPoint>();
            string[] points = tt[5].Split('|');

            SliderPoint last = new SliderPoint(int.MaxValue, int.MaxValue);
            for (int i = 1; i < points.Length; i++)
            {
                string[] xy = points[i].Split(':');
                SliderPoint toAdd = new SliderPoint(int.Parse(xy[0]), int.Parse(xy[1]));
                if (toAdd.x == last.x && toAdd.y == last.y) { added.SliderPoints[added.SliderPoints.Count - 1].SwitchStatic(); }
                else { added.SliderPoints.Add(toAdd); last.x = toAdd.x; last.y = toAdd.y; }

            }
            added.CountOfSlides = int.Parse(tt[6]);
            added.length = double.Parse(tt[7]);
            added.UpdateTimeEnd();
            Global.Map.OsuHitObjects.Add(added);
        }

        private static void AddSpinner(string line)
        {
            string[] tt = line.Split(',');
            OsuSpinner added = (OsuSpinner)spinner.Clone();
            added.x = int.Parse(tt[0]);
            added.y = int.Parse(tt[1]);
            added.time = int.Parse(tt[2]);
            added.time_end = int.Parse(tt[5]);
            Global.Map.OsuHitObjects.Add(added);
        }
    }
}
