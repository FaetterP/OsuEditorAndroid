using Assets.Elements;
using Assets.MapInfo;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.OsuEditor.Timeline
{
    class CreatorTimemarks : MonoBehaviour
    {
        private Timemark _timemarkSample;
        private List<Timemark> _marksToCreate = new List<Timemark>();
        private List<CircleTimemark> _circleMarksToCreate = new List<CircleTimemark>();
        private List<int> _marksOnScreen = new List<int>();
        private List<int> _circleMarksOnScreen = new List<int>();

        void Awake()
        {
            _timemarkSample = Resources.Load<Timemark>("TimingMark");
            UpdateCircleMarks();
        }

        void Update()
        {
            foreach (var t in _marksToCreate)
            {
                if (!_marksOnScreen.Contains(t.time))
                {
                    if (t.IsRightTime())
                    {
                        Timemark created = Instantiate(t, transform);
                        created.Init(t);
                        _marksOnScreen.Add(t.time);
                    }
                }
            }
            foreach (var t in _circleMarksToCreate)
            {
                if (!_circleMarksOnScreen.Contains(t.time))
                {
                    if (t.IsRightTime())
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

        public void AddToList(Timemark timemark)
        {
            _marksToCreate.Add(timemark);
        }

        private void AddMainStepMarks()
        {
            List<TimingPoint> parents = new List<TimingPoint>();
            foreach (var t in Global.Map.TimingPoints)
            {
                Timemark added = _timemarkSample.Clone();
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
                    Timemark added = _timemarkSample.Clone();
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
                Timemark added = _timemarkSample.Clone();
                added.time = (int)time;
                added.height = 50;
                added.color = Color.white;
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
                    double time = _marksToCreate[i + 1].time - _marksToCreate[i].time;
                    Timemark added = _timemarkSample.Clone();
                    added.time = (int)(_marksToCreate[i].time + time * i0 / num);
                    added.height = 30;
                    added.color = color;
                    toaddList.Add(added);
                }
            }
            _marksToCreate.AddRange(toaddList);
            _marksToCreate.Sort();
        }
    }
}
