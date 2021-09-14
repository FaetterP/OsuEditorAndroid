using Assets.Elements;
using Assets.MapInfo;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.OsuEditor.Timeline
{
    class CreatorTimemarks : MonoBehaviour
    {
        [SerializeField] private Timemark _timemark;
                         private List<Timemark> _marksToCreate = new List<Timemark>();
                         private List<CircleTimemark> _circleMarksToCreate = new List<CircleTimemark>();
                         private List<int> _marksOnScreen = new List<int>();
                         private List<int> _circleMarksOnScreen = new List<int>();

        void Awake()
        {
            UpdateCircleMarks();
        }

        void Update()
        {
            foreach (var t in _marksToCreate)
            {
                if (!_marksOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime > t.time - Global.AR_ms && Global.MusicTime < t.time + Global.AR_ms)
                    {
                        Timemark created = Instantiate(t, transform);
                        created.height = t.height;
                        created.time = t.time;
                        created.color = t.color;
                        _marksOnScreen.Add(t.time);
                    }
                }
            }
            foreach (var t in _circleMarksToCreate)
            {
                if (!_circleMarksOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime > t.time - Global.AR_ms && Global.MusicTime < t.time + Global.AR_ms)
                    {
                        CircleTimemark created = Instantiate(t, transform);
                        created.hitObject = t.hitObject;
                        created.time = t.time;
                        _circleMarksOnScreen.Add(t.time);
                    }
                }
            }
        }

        public void UpdateCircleMarks()
        {
            foreach (var t in FindObjectsOfType<CircleTimemark>())
            {
                RemoveCircleMarkFromScreen(t.time);
                Destroy(t.gameObject);
            }
            _circleMarksToCreate.Clear();

            CircleTimemark toCreateCircle = Resources.Load<GameObject>("CircleTimemark").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSpinnerStart = Resources.Load<GameObject>("SpinnerTimemarkStart").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSpinnerEnd = Resources.Load<GameObject>("SpinnerTimemarkEnd").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderStart = Resources.Load<GameObject>("SliderTimemarkStart").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderMiddle = Resources.Load<GameObject>("SliderTimemarkMiddle").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderEnd = Resources.Load<GameObject>("SliderTimemarkEnd").GetComponent<CircleTimemark>();

            foreach (OsuHitObject t in Global.Map.OsuHitObjects)
            {
                if (t is OsuCircle)
                {
                    if (t is OsuSlider)
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateSliderStart.Clone();
                        //toAdd.color = (t as OsuSlider).ComboColor;
                        toAdd.time = t.Time;
                        toAdd.hitObject = t;
                        _circleMarksToCreate.Add(toAdd);

                        toAdd = (CircleTimemark)toCreateSliderEnd.Clone();
                        //toAdd.color = (t as OsuSlider).ComboColor;
                        toAdd.time = (t as OsuSlider)._timeEnd;
                        toAdd.hitObject = t;
                        _circleMarksToCreate.Add(toAdd);

                        TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(t.Time, false);
                        for (int i = 1; i < (t as OsuSlider).CountOfSlides; i++)
                        {
                            toAdd = (CircleTimemark)toCreateSliderMiddle.Clone();
                            //toAdd.color = (t as OsuSlider).ComboColor;
                            toAdd.time = t.Time + (int)OsuMath.SliderLengthToAddedTime((t as OsuSlider).Length, timingPoint.Mult, timingPoint.BeatLength) * i;
                            toAdd.hitObject = t;
                            _circleMarksToCreate.Add(toAdd);
                        }
                    }
                    else
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateCircle.Clone();
                        //toAdd.color = (t as OsuCircle).ComboColor;
                        toAdd.time = t.Time;
                        toAdd.hitObject = t;
                        _circleMarksToCreate.Add(toAdd);
                    }
                }
                if (t is OsuSpinner)
                {
                    CircleTimemark toAdd = (CircleTimemark)toCreateSpinnerStart.Clone();
                   // toAdd.color = Color.white;
                    toAdd.time = t.Time;
                    toAdd.hitObject = t;
                    _circleMarksToCreate.Add(toAdd);

                    toAdd = (CircleTimemark)toCreateSpinnerEnd.Clone();
                    //toAdd.color = Color.white;
                    toAdd.time = (t as OsuSpinner).TimeEnd;
                    toAdd.hitObject = t;
                    _circleMarksToCreate.Add(toAdd);
                }
            }
        }

        public void RemoveMarkFromScreen(int time)
        {
            _marksOnScreen.Remove(time);
        }

        public void RemoveCircleMarkFromScreen(int time)
        {
            _circleMarksOnScreen.Remove(time);
        }

        public void UpdateTimemarks(int step)
        {
            Timemark[] marks = FindObjectsOfType<Timemark>();
            foreach (var t in marks)
            {
                t.DestroyFromScreen();
            }

            _marksToCreate.Clear();
            AddMainStepMarks();
            switch (step)
            {
                case 2:
                    Devide(2, Color.red);
                    break;
                case 3:
                    Devide(3, Color.magenta);
                    break;
                case 4:
                    Devide(2, Color.red);
                    Devide(2, Color.blue);
                    break;
                case 5:
                    Devide(5, Color.yellow);
                    break;
                case 6:
                    Devide(2, Color.red);
                    Devide(3, Color.magenta);
                    break;
                case 7:
                    Devide(7, Color.yellow);
                    break;
                case 8:
                    Devide(2, Color.red);
                    Devide(2, Color.blue);
                    Devide(2, Color.magenta);
                    break;
            }

        }
        private void AddMainStepMarks()
        {
            List<TimingPoint> parents = new List<TimingPoint>();
            foreach (var t in Global.Map.TimingPoints)
            {
                Timemark added = (Timemark)_timemark.Clone();
                added.time = t.Offset;
                added.height = 50;
                added.color = Color.cyan;
                _marksToCreate.Add(added);
                if (t.isParent) { parents.Add(t); }
            }
            double time;
            for (int i = 0; i < parents.Count - 1; i++)
            {
                time = parents[i].Offset;
                while (time < parents[i + 1].Offset)
                {
                    Timemark added = (Timemark)_timemark.Clone();
                    added.time = (int)time;
                    added.height = 50;
                    added.color = Color.white;
                    _marksToCreate.Add(added);
                    time += parents[i].BeatLength;
                }
            }
            time = parents[parents.Count - 1].Offset;
            while (time < Global.MusicLength)
            {
                Timemark added = (Timemark)_timemark.Clone();
                added.time = (int)time;
                added.height = 50;
                added.color = Color.white;
                _marksToCreate.Add(added);
                time += parents[parents.Count - 1].BeatLength;
            }
        }
        private void Devide(ushort num, Color color)
        {
            List<Timemark> toadd = new List<Timemark>();
            for (int i = 0; i < _marksToCreate.Count - 1; i++)
            {
                for (int i0 = 1; i0 < num; i0++)
                {
                    double time = _marksToCreate[i + 1].time - _marksToCreate[i].time;
                    Timemark added = (Timemark)_timemark.Clone();
                    added.time = (int)(_marksToCreate[i].time + time * i0 / num);
                    added.height = 30;
                    added.color = color;
                    toadd.Add(added);
                }
            }
            _marksToCreate.AddRange(toadd);
            _marksToCreate.Sort();
        }
    }
}
