using Assets.Scripts.OsuEditor;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MapInfo.HitObjects
{
    class OsuSlider : OsuHitObject
    {
        private int _xStart, _yStart;
        private int _timeStart;
        private int _comboSum;
        private List<SliderPoint> _sliderPoints;
        public bool _clap, _finish, _whistle;
        private int _countOfSlides;
        private double _length;

        private int _timeEnd;
        private List<Vector2> _printedPoints = new List<Vector2>();

        private static OsuSliderDisplay s_sliderDisplay;

        // x,y,time,type,hitSound,curveType|curvePoints,slides,length,edgeSounds,edgeSets,hitSample
        // 96,162,4875,6,0,P|296:253|324:164,1,375
        public OsuSlider(string line)
        {
            IFormatProvider _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            string[] param = line.Split(',');

            _xStart = int.Parse(param[0]);
            _yStart = int.Parse(param[1]);
            _timeStart = int.Parse(param[2]);
            _comboSum = int.Parse(param[3]);

            _sliderPoints = new List<SliderPoint>();
            string[] points = param[5].Split('|');

            SliderPoint last = new SliderPoint(int.MaxValue, int.MaxValue);
            for (int i = 1; i < points.Length; i++)
            {
                string[] xy = points[i].Split(':');
                SliderPoint toAdd = new SliderPoint((int)double.Parse(xy[0], _formatter), (int)double.Parse(xy[1], _formatter));

                if (toAdd == last)
                {
                    _sliderPoints.Last().SwitchStatic();
                }
                else
                {
                    _sliderPoints.Add(toAdd);
                    last = toAdd;
                }
            }
            _countOfSlides = int.Parse(param[6]);
            _length = double.Parse(param[7], _formatter);

            //TimingPoint timingPoint = map.GetNearestTimingPointLeft(_timeStart, false);
            //_timeEnd = _timeStart + (int)map.SliderLengthToAddedTime(_length, timingPoint.Mult, timingPoint.BeatLength) * _countOfSlides;

        }

        public override int X => _xStart;

        public override int Y => _yStart;

        public override int Time => _timeStart;

        public int TimeEnd => _timeEnd;

        public double Length => _length;

        public int ComboSum
        {
            get
            {
                return _comboSum;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Недопустимое значение.");
                }
                _comboSum = value;
            }
        }

        public ReadOnlyCollection<Vector2> PrintedPoints => _printedPoints.AsReadOnly();

        public ReadOnlyCollection<SliderPoint> SliderPoints => _sliderPoints.AsReadOnly();

        public int CountOfSlides
        {
            get
            {
                return _countOfSlides;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Недопустимое значение");
                }
                _countOfSlides = value;
            }
        }

        public void AddSliderPoint(SliderPoint sliderPoint)
        {
            _sliderPoints.Add(sliderPoint);
        }

        public void RemoveSliderPoint(SliderPoint sliderPoint)
        {
            _sliderPoints.Remove(sliderPoint);
        }

        public void SetCoords(Vector2 coords)
        {
            if (coords.x < 0 || coords.x > 512 || coords.y < 0 || coords.y > 384)
            {
                throw new Exception("Недопустимые координаты.");
            }

            _xStart = (int)coords.x;
            _yStart = (int)coords.y;
            UpdatePrintedPoints();
        }

        public void SetTimeStart(int newTime)
        {
            if (newTime < 0)
            {
                throw new Exception("Недопустимое время");
            }

            int timeLength = _timeEnd - _timeStart;

            _timeStart = newTime;
            _timeEnd = newTime + timeLength;
        }

        public override TimemarkHitObject[] GetTimemark()
        {
            TimemarkHitObject[] ret = new TimemarkHitObject[_countOfSlides + 1];

            TimemarkHitObject toAdd = new TimemarkHitObject(_timeStart, this, "Start");
            ret[0] = toAdd;


            TimemarkHitObject toAdd1 = new TimemarkHitObject(_timeEnd, this, "End");
            ret[_countOfSlides] = toAdd1;

            TimingPoint timingPoint = Global.Map.GetNearestTimingPointLeft(Time, false);
            for (int i = 1; i < CountOfSlides; i++)
            {
                int time = Time + (int)Global.Map.SliderLengthToAddedTime(_length, timingPoint.Mult, timingPoint.BeatLength) * i;
                TimemarkHitObject toAdd2 = new TimemarkHitObject(time, this, "");
                ret[i] = toAdd2;
            }

            return ret;
        }

        public override void SpawnHitObject()
        {
            if (s_sliderDisplay == null)
                s_sliderDisplay = Resources.Load<OsuSliderDisplay>("OsuSlider");

            GameObject.Instantiate(s_sliderDisplay, CreatorHitObjects.Instance.transform).Init(this);
        }

        public override int UpdateComboColor(Color[] colors, int lastColorIndex, int lastNumber)
        {
            if (_comboSum == 2)
            {
                _comboNumber = lastNumber + 1;
                _comboColor = colors[lastColorIndex];
                return lastColorIndex;
            }
            else if (_comboSum == 6)
            {
                _comboNumber = 1;
                int newColorIndex = (lastColorIndex + 1) % colors.Length;
                _comboColor = colors[newColorIndex];
                return newColorIndex;
            }
            else
            {
                lastColorIndex += (_comboSum / 16) + 1;
                int newColorIndex = lastColorIndex % colors.Length;

                _comboNumber = 1;
                _comboColor = colors[newColorIndex];
                return newColorIndex;
            }
        }

        public void UpdatePrintedPoints()
        {
            _printedPoints.Clear();
            List<SliderPoint> toBezier = new List<SliderPoint>();
            // List<Vector2> bezierPoints;

            // toBezier.Add(new SliderPoint(X, Y));
            foreach (var t in SliderPoints)
            {
                if (t.IsStatic)
                {
                    toBezier.Add(t);
                    List<Vector2> vec = OsuMath.GetInterPointBezier(toBezier, toBezier.Count * 5);

                    for (int i = vec.Count - 1; i >= 0; i--)
                    {
                        _printedPoints.Add(vec[i]);
                    }

                    toBezier.Clear();
                    toBezier.Add(t);
                }
                else
                {
                    toBezier.Add(t);
                }
            }

            List<Vector2> vec2 = OsuMath.GetInterPointBezier(toBezier, toBezier.Count * 5);
            for (int i = vec2.Count - 1; i >= 0; i--)
            {
                _printedPoints.Add(vec2[i]);
            }

            //var firstPoint = SliderPoints[0];
            //toBezier.Add(new SliderPoint(X, Y));
            //_printedPoints.Add(new Vector2(X, Y));
            //if (firstPoint.IsStatic)
            //{
            //    _printedPoints.Add(new Vector2((float)firstPoint.x, (float)firstPoint.y));
            //}

            //for (int i = 1; i < SliderPoints.Count; i++)
            //{
            //    SliderPoint sliderPoint = SliderPoints[i];

            //    if (SliderPoints[i - 1].IsStatic && sliderPoint.IsStatic)
            //    {
            //        _printedPoints.Add(new Vector2((float)sliderPoint.x, (float)sliderPoint.y));
            //        //bezierPoints = OsuMath.GetInterPointBezier(new List<SliderPoint>() { sliderPoint, SliderPoints[i - 1] }, 10);

            //        //for (int i0 = bezierPoints.Count - 1; i0 >= 0; i0--)
            //        //{
            //        //    _printedPoints.Add(bezierPoints[i0]);
            //        //}
            //        toBezier.Clear();
            //        toBezier.Add(sliderPoint);
            //        continue;
            //    }

            //    if (sliderPoint.IsStatic)
            //    {
            //        bezierPoints = OsuMath.GetInterPointBezier(toBezier, 10);

            //        for (int i0 = bezierPoints.Count - 1; i0 >= 0; i0--)
            //        {
            //            _printedPoints.Add(bezierPoints[i0]);
            //        }

            //        toBezier.Clear();
            //        toBezier.Add(sliderPoint);
            //    }
            //    else
            //    {
            //        toBezier.Add(sliderPoint);
            //    }
            //}
            //bezierPoints = OsuMath.GetInterPointBezier(toBezier, 100);

            //for (int i0 = bezierPoints.Count - 1; i0 >= 0; i0--)
            //{
            //    _printedPoints.Add(bezierPoints[i0]);
            //}

        }

        public void UpdateLength()
        {
            _length = 0;
            for (int i = 0; i < _printedPoints.Count - 1; i++)
            {
                _length += Vector2.Distance(_printedPoints[i], _printedPoints[i + 1]);
            }
        }

        public void UpdateTimeEnd(Beatmap map)
        {
            TimingPoint timingPoint = map.GetNearestTimingPointLeft(Time, false);
            _timeEnd = _timeStart + (int)map.SliderLengthToAddedTime(_length, timingPoint.Mult, timingPoint.BeatLength) * _countOfSlides;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //64,247,300,2,0,P|247:66|410:221,1,525
            sb.Append(_xStart + ",");
            sb.Append(_yStart + ",");
            sb.Append(_timeStart + ",");
            sb.Append(_comboSum + ",");

            int num = 0;
            if (_whistle) { num += 2; }
            if (_finish) { num += 4; }
            if (_clap) { num += 8; }
            sb.Append(num + ",");

            sb.Append("P|");
            foreach (SliderPoint sliderPoint in SliderPoints)
            {
                sb.Append((int)sliderPoint.x + ":" + (int)sliderPoint.y + "|");
                if (sliderPoint.IsStatic)
                {
                    sb.Append((int)sliderPoint.x + ":" + (int)sliderPoint.y + "|");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("," + _countOfSlides + ",");
            sb.Append(_length);

            return sb.ToString();
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime < _timeEnd && Global.MusicTime > Time - Global.AR_ms;
        }
    }
}
