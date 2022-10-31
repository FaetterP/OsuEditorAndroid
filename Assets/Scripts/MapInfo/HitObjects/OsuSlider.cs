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
        public bool _clap, _finish, _whisle;
        private int _countOfSlides;
        private double _length;

        private int _timeEnd;
        private List<Vector2> _bezierPoints = new List<Vector2>();

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

        public ReadOnlyCollection<Vector2> BezierPoints => _bezierPoints.AsReadOnly();

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
            UpdateBezierPoints();
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

            TimemarkSliderStart toAdd = TimemarkSliderStart.GetSliderStartMark(this);
            ret[0] = toAdd;


            TimemarkSlider toAdd1 = TimemarkSlider.GetSliderEndMark(this);
            ret[_countOfSlides] = toAdd1;

            TimingPoint timingPoint = Global.Map.GetNearestTimingPointLeft(Time, false);
            for (int i = 1; i < CountOfSlides; i++)
            {
                int time = Time + (int)Global.Map.SliderLengthToAddedTime(_length, timingPoint.Mult, timingPoint.BeatLength) * i;
                TimemarkSlider toAdd2 = TimemarkSlider.GetSliderMiddleMark(this, time);
                ret[i] = toAdd2;
            }

            return ret;
        }

        public override void SpawnHitObject()
        {
            if (s_sliderDisplay == null)
                s_sliderDisplay = Resources.Load<OsuSliderDisplay>("OsuSlider");

            GameObject.Instantiate(s_sliderDisplay, GameObject.Find("HitObjectsCanvas").transform).Init(this);
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

        public void UpdateBezierPoints()
        {
            _bezierPoints.Clear();
            List<SliderPoint> toBeze = new List<SliderPoint>();
            toBeze.Add(new SliderPoint(X, Y));
            foreach (var t in SliderPoints)
            {
                if (t.IsStatic)
                {
                    toBeze.Add(t);
                    List<Vector2> vec = OsuMath.GetInterPointBeze(toBeze, 100);

                    for (int i = vec.Count - 1; i >= 0; i--)
                    {
                        _bezierPoints.Add(vec[i]);
                    }

                    toBeze.Clear();
                    toBeze.Add(t);
                }
                else
                {
                    toBeze.Add(t);
                }
            }

            List<Vector2> vec2 = OsuMath.GetInterPointBeze(toBeze, 100);
            for (int i = vec2.Count - 1; i >= 0; i--)
            {
                _bezierPoints.Add(vec2[i]);
            }
        }

        public void UpdateLength()
        {
            _length = 0;
            for (int i = 0; i < _bezierPoints.Count - 1; i++)
            {
                _length += Vector2.Distance(_bezierPoints[i], _bezierPoints[i + 1]);
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
            if (_whisle) { num += 2; }
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
