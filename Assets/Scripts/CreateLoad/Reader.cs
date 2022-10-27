using Assets.Scripts.Elements;
using Assets.Scripts.MapInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.CreateLoad
{
    class Reader : MonoBehaviour
    {
        private string[] _lines;
        private double _lastParentLenght = -1;
        private OsuCircle _circleSample;
        private OsuSlider _sliderSample;
        private OsuSpinner _spinnerSample;
        private IFormatProvider _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        private MapClass map;

        public MapClass LoadMapFromFile(string path)
        {
            _lines = File.ReadAllLines(path);
            map = new MapClass();

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

            _circleSample = Resources.Load<OsuCircle>("OsuCircle");
            _sliderSample = Resources.Load<OsuSlider>("OsuSlider");
            _spinnerSample = Resources.Load<OsuSpinner>("OsuSpinner");

            foreach (var t in GetBlock("[HitObjects]").Split('\n'))
            {
                if (t.Contains('|'))
                    map.OsuHitObjects.Add(ConvertSlider(t));

                else if (t.Split(',').Length == 6)
                    map.OsuHitObjects.Add(ConvertCircle(t));

                else if (t.Split(',').Length == 7)
                    map.OsuHitObjects.Add(ConvertSpinner(t));
            }

            map.UpdateComboInfos();
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

        private OsuCircle ConvertCircle(string line)
        {
            string[] param = line.Split(',');
            OsuCircle circle = _circleSample.Clone();

            Vector2 coords = new Vector2(int.Parse(param[0]), int.Parse(param[1]));
            circle.SetCoords(coords);
            circle.Time = int.Parse(param[2]);
            circle.combo_sum = int.Parse(param[3]);

            int num = int.Parse(param[4]);
            if (num >= 8) { circle.Clap = true; num -= 8; }
            if (num >= 4) { circle.Finish = true; num -= 4; }
            if (num >= 2) { circle.Whisle = true; }

            string[] arr = param[5].Split(':');
            circle.Sampleset = int.Parse(arr[0]);
            circle.Additions = int.Parse(arr[1]);

            return circle;
        }

        private OsuSlider ConvertSlider(string line)
        {
            string[] param = line.Split(',');
            OsuSlider slider = _sliderSample.Clone();

            Vector2 coords = new Vector2(int.Parse(param[0]), int.Parse(param[1]));
            slider.SetCoords(coords);
            slider.Time = int.Parse(param[2]);
            slider.combo_sum = int.Parse(param[3]);
            slider.SliderPoints = new List<SliderPoint>();
            string[] points = param[5].Split('|');

            SliderPoint last = new SliderPoint(int.MaxValue, int.MaxValue);
            for (int i = 1; i < points.Length; i++)
            {
                string[] xy = points[i].Split(':');
                SliderPoint toAdd = new SliderPoint((int)double.Parse(xy[0], _formatter), (int)double.Parse(xy[1], _formatter));

                if (toAdd == last)
                {
                    slider.SliderPoints.Last().SwitchStatic();
                }
                else
                {
                    slider.SliderPoints.Add(toAdd);
                    last = toAdd;
                }
            }
            slider.CountOfSlides = int.Parse(param[6]);
            slider.Length = double.Parse(param[7], _formatter);

            TimingPoint timingPoint = map.GetNearestTimingPointLeft(slider.Time, false);
            slider.TimeEnd = slider.Time + (int)map.SliderLengthToAddedTime(slider.Length, timingPoint.Mult, timingPoint.BeatLength) * slider.CountOfSlides;

            return slider;
        }

        private OsuSpinner ConvertSpinner(string line)
        {
            string[] param = line.Split(',');
            OsuSpinner spinner = _spinnerSample.Clone();

            Vector2 coords = new Vector2(int.Parse(param[0]), int.Parse(param[1]));
            spinner.SetCoords(coords);
            spinner.Time = int.Parse(param[2]);
            spinner.TimeEnd = int.Parse(param[5]);

            return spinner;
        }
    }
}
