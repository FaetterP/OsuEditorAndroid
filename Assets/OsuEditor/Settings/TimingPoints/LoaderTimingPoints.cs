using Assets.MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class LoaderTimingPoints : MonoBehaviour
    {
        [SerializeField] private TimingPointElement TimingPointElement;
        void OnEnable()
        {
            UpdateTimingPoints();
        }
        public void UpdateTimingPoints()
        {
            Fix();
            DestroyChild();
            foreach (var t in Global.Map.TimingPoints)
            {
                TimingPointElement created = Instantiate(TimingPointElement, transform);
                created.timingPoint = t;
            }
        }
        private void DestroyChild()
        {
            foreach (var child in GetComponentsInChildren<TimingPointElement>())
            {
                Destroy(child.gameObject);
            }
        }

        private void Fix()
        {
            Global.Map.TimingPoints.Sort(new PeopleComparer());
            Global.Map.TimingPoints.Sort();
            TimingPoint timingPoint = Global.Map.TimingPoints[0];
            if (timingPoint.isParent == false && timingPoint.Offset!= Global.Map.TimingPoints[1].Offset)
            {
                timingPoint.isParent = true;
                timingPoint.Mult = 1;
            }

            //TimingPoint lastParent = null;
            //foreach(var t in Global.Map.TimingPoints)
            //{
            //    if (t.isParent) { lastParent = t; continue; }
            //    t.BeatLength = lastParent.BeatLength;
            //    t.BPM = lastParent.BPM;
            //}
        }
    }
    class PeopleComparer : IComparer<TimingPoint>
    {
        public int Compare(TimingPoint p1, TimingPoint p2)
        {
            if (p1.Offset > p2.Offset)
                return 1;
            else if (p1.Offset < p2.Offset)
                return -1;
            else
                return 1;
        }
    }
}
