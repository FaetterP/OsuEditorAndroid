using Assets.Scripts.OsuEditor;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using System;
using UnityEngine;

namespace Assets.Scripts.MapInfo.HitObjects
{
    class OsuSpinner : OsuHitObject
    {
        private int _x, _y;
        private int _timeStart;
        private int _timeEnd;

        private static OsuSpinnerDisplay s_spinnerDisplay;

        // x,y,time,type,hitSound,endTime,hitSample
        // 256,192,3600,12,0,4200,0:0:0:0:
        public OsuSpinner(string line)
        {
            string[] param = line.Split(',');

            _x = int.Parse(param[0]);
            _y = int.Parse(param[1]);
            _timeStart = int.Parse(param[2]);
            _timeEnd = int.Parse(param[5]);
        }

        public override int X => _x;

        public override int Y => _y;

        public override int Time => _timeStart;

        public int TimeEnd => _timeEnd;

        public void SetTimeStart(int newTime)
        {
            if (newTime < 0)
            {
                throw new Exception("Недопустимое время.");
            }

            int timeLength = _timeEnd - _timeStart;
            _timeStart = newTime;
            _timeEnd = newTime + timeLength;
        }

        public void SetTimeEnd(int newTime)
        {
            if (newTime <= _timeStart)
            {
                _timeEnd = _timeStart + 1000;
            }

            _timeEnd = newTime;
        }

        public override TimemarkHitObject[] GetTimemark()
        {
            TimemarkHitObject[] ret = new TimemarkHitObject[2];

            TimemarkHitObject toAdd = new TimemarkHitObject(_timeStart, this, "Start");
            ret[0] = toAdd;

            TimemarkHitObject toAdd2 = new TimemarkHitObject(_timeEnd, this, "End");
            ret[1] = toAdd2;

            return ret;
        }

        public override void SpawnHitObject()
        {
            if (s_spinnerDisplay == null)
                s_spinnerDisplay = Resources.Load<OsuSpinnerDisplay>("OsuSpinner");

            GameObject.Instantiate(s_spinnerDisplay, CreatorHitObjects.Instance.transform).Init(this);
        }

        public override int UpdateComboColor(Color[] colors, int lastColorIndex, int lastNumber)
        {
            _comboNumber = 0;
            _comboColor = Color.white;
            return lastColorIndex + 1;
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime < _timeEnd && Global.MusicTime > _timeStart - Global.AR_ms;
        }


        public override string ToString()
        {
            // 256,192,734,12,8,4992,0:1:0:0:
            return "256,192," + _timeStart + ",12,8," + _timeEnd + ",0:1:0:0";
        }
    }
}
