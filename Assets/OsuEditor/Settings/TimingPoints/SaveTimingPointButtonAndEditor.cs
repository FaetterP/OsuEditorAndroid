using Assets.MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Utilities;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class SaveTimingPointButtonAndEditor : MonoBehaviour
    {
        private TimingPoint TimingPoint;

        [SerializeField] private InputField Offset;
        [SerializeField] private InputField BPM;
        [SerializeField] private Dropdown Meter;
        [SerializeField] private InputField Volume;
        [SerializeField] private Toggle isKiai;
        [SerializeField] private CreatorMusicLineMarks creator;

        [SerializeField] private LoaderTimingPoints loader;

        public void SetTimingPoint(TimingPoint timingPoint)
        {
            Offset.text = timingPoint.Offset.ToString();
            if (timingPoint.isParent) { BPM.text = (60000f/timingPoint.BeatLength).ToString(); }
            else { BPM.text = timingPoint.Mult.ToString(); }
            Volume.text = timingPoint.Volume.ToString();
            isKiai.isOn = timingPoint.Kiai;
            TimingPoint = timingPoint;
            Meter.value = TimingPoint.Meter - 3;
        }

        public TimingPoint GetTimingPoint()
        {
            return TimingPoint;
        }

        void OnMouseDown()
        {
            if (TimingPoint == null) { return; }
            TimingPoint.Offset = int.Parse(Offset.text);
            TimingPoint.Kiai = isKiai.isOn;
            if (TimingPoint.isParent)
            {
                double newBPM = double.Parse(BPM.text);
                bool need = false;
                foreach(var t in Global.Map.TimingPoints)
                {
                    if (t == TimingPoint) { need = true; continue; }
                    if (need) 
                    { 
                        if (t.isParent) { break; }
                        //t.BPM = newBPM;
                     //   Debug.Log(t.Offset + " " + t.BPM);
                    }
                }
                //TimingPoint.BPM = newBPM;
            }
            else
            {
                TimingPoint.Mult = double.Parse(BPM.text);
                //TimingPoint.BPM = OsuMath.GetNearestTimingPointLeft(TimingPoint.Offset, true).BPM;
            }
            TimingPoint.Volume = int.Parse(Volume.text);
            TimingPoint.Meter = Meter.value + 3;
            TimingPoint.BeatLength = OsuMath.GetNearestTimingPointLeft(TimingPoint.Offset, true).BeatLength;

            loader.UpdateTimingPoints();
            TimingPoint = null;
            Offset.text = "";
            BPM.text = "";
            Meter.value = 0;
            Volume.text = "";
            isKiai.isOn = false;
            creator.UpdateMarks();
        }
    }
}
