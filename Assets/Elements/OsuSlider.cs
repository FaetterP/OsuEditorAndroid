using Assets.MapInfo;
using Assets.OsuEditor;
using Assets.OsuEditor.Timeline;
using Assets.OsuEditor.Timeline.Timemarks;
using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    [RequireComponent(typeof(LineRenderer))]
    class OsuSlider : OsuCircle
    {
        private double _length;
        private int _timeEnd;
        private int _countOfSlides;
        public List<SliderPoint> SliderPoints = new List<SliderPoint>();
        private List<Vector2> _bezePoints = new List<Vector2>();
        private LineRenderer _thisLineRenderer;

        public ReadOnlyCollection<Vector2> BezePoints
        {
            get
            {
                return _bezePoints.AsReadOnly();
            }
        }
        public int CountOfSlides
        {
            get
            {
                return _countOfSlides;
            }
            set
            {
                if (value < 1) { throw new ArgumentException(); }
                _countOfSlides = value;
            }
        }
        public double Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (value <= 0) { throw new ArgumentException(); }
                _length = value;
            }
        }
        public int TimeEnd
        {
            get
            {
                return _timeEnd;
            }
            set
            {
                if (value <= Time) { throw new ArgumentException(); }
                _timeEnd = value;
            }
        }

        void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        void Start()
        {
            _comboInfo = Global.Map.GetComboInfo(Time);

            GetComponent<PrinterNumber>().Print(Number);
            transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(X, Y));
            PrintSliderPoints();
            UpdateBezePoints();
            PrintBezePoints();
        }

        void OnMouseDown()
        {
            _isMoving = true;
        }

        void Update()
        {
            int razn = Time - Global.MusicTime;
            if (Time - Global.MusicTime > Global.AR_ms || _timeEnd - Global.MusicTime < 0)
            {
                Destroy(gameObject);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (_isMoving)
                        {
                            var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                            poss.z = 0;
                            transform.localPosition = poss;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (_isMoving)
                        {
                            OsuHitObject obj = OsuMath.GetHitObjectFromTime(Time);
                            var pos = OsuMath.UnityCoordsToOsu(transform.localPosition);

                            obj.SetCoords(pos);
                            for (int i = 0; i < (obj as OsuSlider).SliderPoints.Count; i++)
                            {
                                (obj as OsuSlider).SliderPoints[i].x += -obj.X + pos.x;
                                (obj as OsuSlider).SliderPoints[i].y += -obj.Y + pos.y;
                            }

                            Destroy(gameObject);
                        }
                        break;
                }
            }
        }

        public void UpdateBezePoints()
        {
            _bezePoints.Clear();
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
                        _bezePoints.Add(vec[i]);
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
                _bezePoints.Add(vec2[i]);
            }
        }

        public void PrintBezePoints()
        {
            GameObject circle = Resources.Load("SliderBezePoint") as GameObject;
            foreach (var t in BezePoints)
            {
                GameObject created = Instantiate(circle, transform);
                var vec = OsuMath.OsuCoordsToUnity(t);
                created.transform.localPosition = new Vector2(vec.x, vec.y) - new Vector2(transform.localPosition.x, transform.localPosition.y);
                created.GetComponent<Image>().color = _comboInfo.Color;
                created.transform.SetAsFirstSibling();
            }
        }

        public void UpdateLength()
        {
            _length = 0;
            for (int i = 0; i < BezePoints.Count - 1; i++)
            {
                double add_length = Math.Sqrt(Math.Pow(BezePoints[i].x - BezePoints[i + 1].x, 2) + Math.Pow(BezePoints[i].y - BezePoints[i + 1].y, 2));
                _length += add_length;
            }
        }

        public void UpdateTimeEnd()
        {
            TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(Time, false);
            _timeEnd = Time + (int)OsuMath.SliderLengthToAddedTime(_length, timingPoint.Mult, timingPoint.BeatLength) * CountOfSlides;
        }

        private void PrintSliderPoints()
        {
            UpdateLine();
            GameObject go = Resources.Load("SliderPoint") as GameObject;
            SliderPointGameObject spgo = go.GetComponent<SliderPointGameObject>();
            foreach (var t in SliderPoints)
            {
                SliderPointGameObject created = Instantiate(spgo, transform);
                created.transform.SetAsLastSibling();
                created.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2((float)t.x, (float)t.y)) - new Vector2(transform.localPosition.x, transform.localPosition.y);
                created.thisSlider = this;
                created.thisPoint = t;
            }
        }

        public void UpdateLine()
        {
            _thisLineRenderer.positionCount = SliderPoints.Count + 1;
            _thisLineRenderer.SetPosition(0, Vector2.zero);
            for (int i = 0; i < SliderPoints.Count; i++)
            {
                Vector3 position = OsuMath.OsuCoordsToUnity(new Vector2((float)SliderPoints[i].x, (float)SliderPoints[i].y));
                position -= transform.localPosition;
                position -= new Vector3(0, 0, 1);
                _thisLineRenderer.SetPosition(i + 1, position);
            }
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime < TimeEnd && Global.MusicTime > Time - Global.AR_ms;
        }

        public override void Init(OsuHitObject obj)
        {
            OsuSlider other = obj as OsuSlider;
            Time = other.Time;
            SetCoords(other.X, other.Y);
            TimeEnd = other.TimeEnd;
            combo_sum = other.combo_sum;
            CountOfSlides = other.CountOfSlides;
            Length = other.Length;
            SliderPoints = other.SliderPoints;
        }

        public override TimemarkCircle[] GetTimemark()
        {
            TimemarkCircle[] ret = new TimemarkCircle[_countOfSlides + 1];

            TimemarkSliderStart toAdd = TimemarkSliderStart.GetSliderStartMark(this);
            ret[0] = toAdd;


            TimemarkSlider toAdd1 = TimemarkSlider.GetSliderEndMark(this);
            ret[_countOfSlides] = toAdd1;

            TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(Time, false);
            for (int i = 1; i < CountOfSlides; i++)
            {
                int time = Time + (int)OsuMath.SliderLengthToAddedTime(Length, timingPoint.Mult, timingPoint.BeatLength) * i;
                TimemarkSlider toAdd2 = TimemarkSlider.GetSliderMiddleMark(this, time);
                ret[i] = toAdd2;
            }

            return ret;
        }

        public new OsuSlider Clone()
        {
            return (OsuSlider)MemberwiseClone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //64,247,300,2,0,P|247:66|410:221,1,525
            sb.Append(X + ",");
            sb.Append(Y + ",");
            sb.Append(Time + ",");
            sb.Append(combo_sum + ",");

            int num = 0;
            if (Whisle) { num += 2; }
            if (Finish) { num += 4; }
            if (Clap) { num += 8; }
            sb.Append(num + ",");
            sb.Append("P|");
            foreach (var t in SliderPoints)
            {
                sb.Append(t.x + ":" + t.y + "|");
                if (t.IsStatic)
                {
                    sb.Append(t.x + ":" + t.y + "|");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("," + CountOfSlides + ",");
            sb.Append(_length);

            return sb.ToString();
        }
    }
}
