using Assets.Scripts.MapInfo;
using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline
{
    class CreatorTimemarks : MonoBehaviour
    {
        private List<Timemark> _timemarkLines = new List<Timemark>();
        private List<Timemark> _timemarkHtObjects = new List<Timemark>();

        private List<Timemark> _marksToCreate = new List<Timemark>();
        private List<Timemark> _marksOnScreen = new List<Timemark>();

        void Awake()
        {
            UpdateCircleMarks();
        }

        void Update()
        {
            foreach (Timemark timemark in _marksToCreate)
            {
                if (_marksOnScreen.Contains(timemark) == false)
                {
                    if (timemark.IsRightTime())
                    {
                        timemark.SpawnGameObject();
                        _marksOnScreen.Add(timemark);
                    }
                }
            }
        }

        public void UpdateCircleMarks()
        {
            foreach (var t in FindObjectsOfType<TimemarkCircleGO>())
            {
                Destroy(t.gameObject);
            }

            _timemarkHtObjects.Clear();

            foreach (OsuHitObject hitObject in Global.Map.OsuHitObjects)
            {
                foreach (TimemarkHitObject timemark in hitObject.GetTimemark())
                {
                    if (_marksToCreate.Contains(timemark))
                    {
                        //Debug.Log(timemark.GetType());
                        continue;
                    }
                    //Debug.Log($"{hitObject.Time} {_marksToCreate.Contains(timemark)} {timemark.Time}");
                    _timemarkHtObjects.Add(timemark);
                }
            }

            //_marksToCreate.Sort();
            _marksToCreate.Clear();
            _marksToCreate.AddRange(_timemarkLines);
            _marksToCreate.AddRange(_timemarkHtObjects);
            _marksToCreate.Sort();
        }

        public void RemoveTimemarkFromScreen(Timemark timemark)
        {
            _marksOnScreen.Remove(timemark);
        }

        public void UpdateTimemarks(int step)
        {
            TimemarkGO[] marks = FindObjectsOfType<TimemarkGO>();
            foreach (var t in marks)
            {
                Destroy(t.gameObject);
            }

            _timemarkLines = _marksToCreate.Where(val => val is TimemarkHitObject).ToList();
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

            _marksToCreate.Clear();
            _marksToCreate.AddRange(_timemarkLines);
            _marksToCreate.AddRange(_timemarkHtObjects);
            _marksToCreate.Sort();
        }

        private void AddMainStepMarks()
        {
            foreach (var point in Global.Map.TimingPoints)
            {
                Color color = point.isParent ? Color.red : Color.green;
                Timemark added = new TimemarkLine(point.Offset, color, 100);

                _timemarkLines.Add(added);
            }

            ReadOnlyCollection<TimingPoint> parents = Global.Map.GetParentTimingPoints();
            double time;
            for (int i = 0; i < parents.Count - 1; i++)
            {
                time = parents[i].Offset;
                while (time < parents[i + 1].Offset)
                {
                    Timemark added = new TimemarkLine((int)time, Color.white, 50);
                    _timemarkLines.Add(added);
                    time += parents[i].BeatLength;
                }
            }

            time = parents[parents.Count - 1].Offset;
            while (time < Global.MusicLength)
            {
                Timemark added = new TimemarkLine((int)time, Color.white, 50);
                _timemarkLines.Add(added);
                time += parents[parents.Count - 1].BeatLength;
            }
        }
        private void Devide(ushort num, Color color)
        {
            List<Timemark> toaddList = new List<Timemark>();
            int count = _timemarkLines.Count;
            for (int i = 0; i < count - 1; i++)
            {
                Timemark prev = _timemarkLines[i];
                if (prev is TimemarkLine == false)
                {
                    continue;
                }

                for (int i0 = 1; i0 < num; i0++)
                {
                    Timemark next = _timemarkLines[i + 1];
                    int time = next.Time - prev.Time;
                    Timemark added = new TimemarkLine((int)(1f * prev.Time + time * i0 / num), color, 50);
                    toaddList.Add(added);
                }
            }
            _timemarkLines.AddRange(toaddList);
            _timemarkLines.Sort();
        }
    }
}
