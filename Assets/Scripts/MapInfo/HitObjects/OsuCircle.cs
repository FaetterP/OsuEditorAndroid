using Assets.Scripts.OsuEditor;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using System;
using UnityEngine;

namespace Assets.Scripts.MapInfo.HitObjects
{
    class OsuCircle : OsuHitObject
    {
        private int _x, _y;
        private int _time;
        private int _comboSum;
        public bool Clap, Finish, Whisle;
        private int _sampleset;
        private int _additions;

        private static OsuCircleDisplay s_circleDisplay;

        // x,y,time,type,hitSound,objectParams,hitSample
        // 239,73,6450,1,0,0:0:0:0:
        public OsuCircle(string line)
        {
            string[] param = line.Split(',');

            _x = int.Parse(param[0]);
            _y = int.Parse(param[1]);
            _time = int.Parse(param[2]);
            _comboSum = int.Parse(param[3]);

            int soundsBinary = int.Parse(param[4]);
            if (soundsBinary >= 8) { Clap = true; soundsBinary -= 8; }
            if (soundsBinary >= 4) { Finish = true; soundsBinary -= 4; }
            if (soundsBinary >= 2) { Whisle = true; }

            string[] otherParams = param[5].Split(':');
            _sampleset = int.Parse(otherParams[0]);
            _additions = int.Parse(otherParams[1]);
        }

        public override int X => _x;

        public override int Y => _y;

        public override int Time => _time;

        public int Sampleset
        {
            get
            {
                return _sampleset;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Недопустимое значение.");
                }
                _sampleset = value;
            }
        }

        public int Additions
        {
            get
            {
                return _additions;
            }
            set
            {
                if (value < 0 | value > 3)
                {
                    throw new Exception("Недопустимое значение");
                }

                _additions = value;
            }
        }

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

        public void SetCoords(Vector2 coords)
        {
            if (coords.x < 0 || coords.x > 512 || coords.y < 0 || coords.y > 384)
            {
                throw new Exception("Недопустимые координаты.");
            }

            _x = (int)coords.x;
            _y = (int)coords.y;
        }

        public void SetTime(int newTime)
        {
            if (newTime < 0)
            {
                throw new Exception("Недопустимое время.");
            }

            _time = newTime;
        }

        public override void SpawnHitObject()
        {
            if (s_circleDisplay == null)
                s_circleDisplay = Resources.Load<OsuCircleDisplay>("OsuCircle");

            GameObject.Instantiate(s_circleDisplay, CreatorHitObjects.Instance.transform).Init(this);
        }

        public override int UpdateComboColor(Color[] colors, int lastColorIndex, int lastNumber)
        {
            if (_comboSum == 1)
            {
                _comboColor = colors[lastColorIndex];
                _comboNumber = lastNumber + 1;
                return lastColorIndex;
            }
            else if (_comboSum == 5)
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

        public override TimemarkHitObject[] GetTimemark()
        {
            TimemarkHitObject[] ret = new TimemarkHitObject[1];

            TimemarkHitObject toAdd = new TimemarkHitObject(_time, this, "");
            ret[0] = toAdd;

            return ret;
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime > _time - Global.AR_ms && Global.MusicTime < _time;
        }
    }
}
