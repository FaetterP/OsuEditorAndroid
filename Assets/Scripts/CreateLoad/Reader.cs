using Assets.Scripts.MapInfo;
using Assets.Scripts.MapInfo.HitObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.CreateLoad
{
    class Reader
    {
        private string[] _lines;
        private double _lastParentLenght = -1;
        private IFormatProvider _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        private Beatmap map;

        public Beatmap LoadMapFromFile(string path)
        {
            _lines = File.ReadAllLines(path);
            map = new Beatmap();

            map.General.AudioFilename = GetValue("AudioFilename:");
            map.General.AudioLeadIn = GetIntValue("AudioLeadIn:");
            map.General.PreviewTime = GetIntValue("PreviewTime:");
            map.General.Countdown = GetIntValue("Countdown:");
            map.General.SampleSet = GetValue("SampleSet:");
            map.General.StackLeniency = GetDoubleValue("StackLeniency:");
            map.General.Mode = GetIntValue("Mode:");
            map.General.LetterboxInBreaks = GetIntValue("LetterboxInBreaks:");
            map.General.WidescreenStoryboard = GetBoolValue("WidescreenStoryboard:");

            map.Editor.ClearBookmarks();
            if (GetValue("Bookmarks:") != null)
            {
                foreach (string t in GetValue("Bookmarks:").Split(','))
                {
                    if (t == "") { break; }
                    map.Editor.AddBookmark(int.Parse(t));
                }
            }
            map.Editor.DistanceSpacing = GetDoubleValue("DistanceSpacing:");
            map.Editor.BeatDivisor = GetIntValue("BeatDivisor:");
            map.Editor.GridSize = GetIntValue("GridSize:");
            map.Editor.TimelineZoom = GetDoubleValue("TimelineZoom:");

            map.Metadata.Title = GetValue("Title:");
            map.Metadata.TitleUnicode = GetValue("TitleUnicode:");
            map.Metadata.Artist = GetValue("Artist:");
            map.Metadata.ArtistUnicode = GetValue("ArtistUnicode:");
            map.Metadata.Creator = GetValue("Creator:");
            map.Metadata.Version = GetValue("Version:");
            map.Metadata.Source = GetValue("Source:");
            map.Metadata.Tags = GetValue("Tags:");
            map.Metadata.BeatmapID = GetIntValue("BeatmapID:");
            map.Metadata.BeatmapSetID = GetIntValue("BeatmapSetID:");

            map.Difficulty.HPDrainRate = GetDoubleValue("HPDrainRate:");
            map.Difficulty.CircleSize = GetDoubleValue("CircleSize:");
            map.Difficulty.OverallDifficulty = GetDoubleValue("OverallDifficulty:");
            map.Difficulty.ApproachRate = GetDoubleValue("ApproachRate:");
            map.Difficulty.SliderMultiplier = GetDoubleValue("SliderMultiplier:");
            map.Difficulty.SliderTickRate = GetIntValue("SliderTickRate:");

            string[] backgroundParams = GetValue("0,0,").Split(',');
            map.Events.BackgroungImage = backgroundParams[0].Remove(backgroundParams[0].Length - 1, 1).Remove(0, 1);
            map.Events.xOffset = int.Parse(backgroundParams[1]);
            map.Events.yOffset = int.Parse(backgroundParams[2]);

            List<string> storyboardBlock = GetBlock("[Events]").Split('\n').ToList();
            storyboardBlock.RemoveAt(0);

            string storyboard = "";
            if (storyboardBlock.Count > 0)
                storyboard = storyboardBlock.Aggregate((i, j) => i + '\n' + j);
            map.Events.storyboard = storyboard;

            foreach (var t in GetBlock("[TimingPoints]").Split('\n'))
            {
                map.AddTimingPoint(ConvertTimingPoint(t));
            }

            string coloursBlock = GetBlock("[Colours]");
            if (coloursBlock != "")
            {
                foreach (var t in coloursBlock.Split('\n'))
                {
                    float[] color_arr = t.Split(' ').Last().Split(',').Select(x => int.Parse(x) / 255f).ToArray();
                    Color added = new Color(color_arr[0], color_arr[1], color_arr[2]);
                    map.Colors.Add(added);
                }
            }
            else
            {
                map.Colors.Add(new Color(224 / 255f, 255 / 255f, 255 / 255f));
                map.Colors.Add(new Color(230 / 255f, 230 / 255f, 250 / 255f));
            }

            foreach (string line in GetBlock("[HitObjects]").Split('\n'))
            {
                if (line.Contains('|'))
                    map.OsuHitObjects.Add(new OsuSlider(line));

                else if (line.Split(',').Length == 6)
                    map.OsuHitObjects.Add(new OsuCircle(line));

                else if (line.Split(',').Length == 7)
                    map.OsuHitObjects.Add(new OsuSpinner(line));
            }

            map.UpdateComboInfos();
            foreach (OsuHitObject hitObject in map.OsuHitObjects)
            {
                if (hitObject is OsuSlider)
                {
                    OsuSlider slider = hitObject as OsuSlider;
                    slider.UpdateTimeEnd(map);
                }
            }
            return map;
        }

        private string GetValue(string fieldname)
        {
            foreach (var t in _lines)
            {
                if (t.StartsWith(fieldname))
                {
                    string ret = t.Remove(0, fieldname.Length);

                    if (ret.StartsWith(" "))
                        ret = ret.Remove(0, 1);

                    return ret;
                }
            }
            return null;
        }

        private int GetIntValue(string field)
        {
            return int.Parse(GetValue(field));
        }

        private double GetDoubleValue(string field)
        {
            return double.Parse(GetValue(field), _formatter);
        }

        private bool GetBoolValue(string field)
        {
            int value = int.Parse(GetValue(field));

            if (value != 0 && value != 1)
                throw new FormatException();

            return value == 1;
        }

        private string GetBlock(string block)
        {
            string ret = "";
            bool isNeed = false;

            foreach (var t in _lines)
            {
                if (t.StartsWith("["))
                    isNeed = false;

                if (isNeed && t != "" && t.StartsWith("//") == false)
                    ret += t + "\n";

                if (t == block)
                    isNeed = true;
            }

            if (ret != "")
                ret = ret.Remove(ret.Length - 1, 1);

            return ret;
        }

        private TimingPoint ConvertTimingPoint(string tp)
        {
            string[] param = tp.Split(',');

            TimingPoint ret = new TimingPoint();
            ret.Offset = int.Parse(param[0]);

            double divisor = double.Parse(param[1], _formatter);
            if (divisor > 0)
            {
                ret.isParent = true;
                ret.BeatLength = divisor;
                ret.Mult = 1;
                _lastParentLenght = divisor;
            }
            else
            {
                ret.isParent = false;
                ret.BeatLength = _lastParentLenght;
                ret.Mult = -100 / divisor;
            }
            ret.Meter = int.Parse(param[2]);
            ret.SampleSet = int.Parse(param[3]);
            ret.SampleIndex = int.Parse(param[4]);
            ret.Volume = int.Parse(param[5]);
            ret.Kiai = param[7] == "1";

            return ret;
        }
    }
}
