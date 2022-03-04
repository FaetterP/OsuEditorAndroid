using Assets.MapInfo;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class SaveTimingPointButtonAndEditor : MonoBehaviour
    {
        [SerializeField] private LoaderTimingPoints _loader;
        [SerializeField] private CreatorMusicLineMarks _creator;
        [Header("Timing Point Settings")]
        [SerializeField] private InputField _offset;
        [SerializeField] private InputField _bpm;
        [SerializeField] private Dropdown _meter;
        [SerializeField] private InputField _volume;
        [SerializeField] private Toggle _isKiai;

        private TimingPoint _editedTimingPoint;
        private IFormatProvider _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public void SelectTimingPoint(TimingPoint timingPoint)
        {
            _offset.text = timingPoint.Offset.ToString();
            if (timingPoint.isParent) { _bpm.text = (60000f / timingPoint.BeatLength).ToString(); }
            else { _bpm.text = timingPoint.Mult.ToString(); }
            _volume.text = timingPoint.Volume.ToString();
            _isKiai.isOn = timingPoint.Kiai;
            _editedTimingPoint = timingPoint;
            _meter.value = _editedTimingPoint.Meter - 3;
        }

        public TimingPoint GetTimingPoint()
        {
            return _editedTimingPoint;
        }

        void OnMouseDown()
        {
            if (_editedTimingPoint == null)
                return;

            _editedTimingPoint.Offset = int.Parse(_offset.text);
            _editedTimingPoint.Kiai = _isKiai.isOn;
            if (_editedTimingPoint.isParent)
            {
                double newBPM = double.Parse(_bpm.text, _formatter);
                bool need = false;
                foreach (var t in Global.Map.TimingPoints)
                {
                    if (t == _editedTimingPoint) 
                    {
                        need = true;
                        continue; 
                    }
                    if (need)
                    {
                        if (t.isParent) { break; }
                    }
                }
            }
            else
            {
                _editedTimingPoint.Mult = double.Parse(_bpm.text, _formatter);
            }
            _editedTimingPoint.Volume = int.Parse(_volume.text);
            _editedTimingPoint.Meter = _meter.value + 3;
            _editedTimingPoint.BeatLength = Global.Map.GetNearestTimingPointLeft(_editedTimingPoint.Offset, true).BeatLength;

            Global.Map.SortTimingPoints();

            _loader.UpdateTimingPoints();
            _editedTimingPoint = null;
            _offset.text = "";
            _bpm.text = "";
            _meter.value = 0;
            _volume.text = "";
            _isKiai.isOn = false;
            _creator.UpdateMarks();
        }
    }
}
