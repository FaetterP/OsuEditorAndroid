using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.MapInfo;
using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline
{
    class CreatorTimemarks : MonoBehaviour
    {
        private List<Timemark> _marksToCreate = new List<Timemark>();
        private List<TimemarkCircle> _circleMarksToCreate = new List<TimemarkCircle>();
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
                if (!_marksOnScreen.Contains(t.Time))
                {
                    if (t.IsRightTime())
                    {
                        Timemark created = Instantiate(t, transform);
                        created.Init(t);
                        _marksOnScreen.Add(t.Time);
                    }
                }
            }
            foreach (var t in _circleMarksToCreate)
            {
                if (!_circleMarksOnScreen.Contains(t.Time))
                {
                    if (t.IsRightTime())
                    {
                        TimemarkCircle created = Instantiate(t, transform);
                        created.Init(t);
                        _circleMarksOnScreen.Add(t.Time);
                    }
                }
            }
        }

        public void UpdateCircleMarks()
        {
            foreach (var t in FindObjectsOfType<TimemarkCircle>())
            {
                Destroy(t.gameObject);
            }
            _circleMarksToCreate.Clear();

            foreach (OsuHitObject hitObject in Global.Map.OsuHitObjects)
            {
                foreach (var timemark in hitObject.GetTimemark())
                {
                    _circleMarksToCreate.Add(timemark);
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
                Destroy(t.gameObject);
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

        public void AddToList(Timemark timemark)
        {
            _marksToCreate.Add(timemark);
        }

        private void AddMainStepMarks()
        {
            foreach (var point in Global.Map.TimingPoints)
            {
                TimemarkLine added = TimemarkLine.GetTimingPointMark(point);
                _marksToCreate.Add(added);
            }

            ReadOnlyCollection<TimingPoint> parents = Global.Map.GetParentTimingPoints();
            double time;
            for (int i = 0; i < parents.Count - 1; i++)
            {
                time = parents[i].Offset;
                while (time < parents[i + 1].Offset)
                {
                    Timemark added = TimemarkLine.GetBeatMark((int)time);
                    _marksToCreate.Add(added);
                    time += parents[i].BeatLength;
                }
            }

            time = parents[parents.Count - 1].Offset;
            while (time < Global.MusicLength)
            {
                Timemark added = TimemarkLine.GetBeatMark((int)time);
                _marksToCreate.Add(added);
                time += parents[parents.Count - 1].BeatLength;
            }
        }
        private void Devide(ushort num, Color color)
        {
            List<Timemark> toaddList = new List<Timemark>();
            for (int i = 0; i < _marksToCreate.Count - 1; i++)
            {
                for (int i0 = 1; i0 < num; i0++)
                {
                    double time = _marksToCreate[i + 1].Time - _marksToCreate[i].Time;
                    Timemark added = TimemarkLine.GetMiddleMark((int)(_marksToCreate[i].Time + time * i0 / num), color);
                    toaddList.Add(added);
                }
            }
            _marksToCreate.AddRange(toaddList);
            _marksToCreate.Sort();
        }
    }
}
