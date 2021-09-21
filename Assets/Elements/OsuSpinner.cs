using Assets.OsuEditor.Timeline;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    class OsuSpinner : OsuHitObject
    {
        private Image _thisImage;
        private int _timeEnd;
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

        private static CircleTimemark s_spinnerStartTimemark;
        private static CircleTimemark s_spinnerEndTimemark;

        void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(X, Y));
        }

        void Update()
        {
            if (Global.MusicTime < Time - Global.AR_ms)
            {
                Destroy(gameObject);
            }
            else if (Global.MusicTime < Time)
            {
                transform.rotation = Quaternion.Euler(0, 0, Time);
                _thisImage.color = new Color(1, 1, 1, 0.1f);
            }
            else if (Global.MusicTime > Time && Global.MusicTime < TimeEnd)
            {
                transform.rotation = Quaternion.Euler(0, 0, Global.MusicTime);
                _thisImage.color = new Color(1, 1, 1, 0.1f + 0.9f * ((Global.MusicTime - Time * 1.0f) / (TimeEnd - Time)));
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public OsuSpinner Clone()
        {
            return (OsuSpinner)MemberwiseClone();
        }

        public override string ToString()
        {
            // 256,192,734,12,8,4992,0:1:0:0:
            return "256,192," + Time + ",12,8," + TimeEnd + ",0:1:0:0";
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime < TimeEnd && Global.MusicTime > Time - Global.AR_ms;
        }

        public override void Init(OsuHitObject obj)
        {
            OsuSpinner other = obj as OsuSpinner;
            SetCoords(256, 192);
            Time = other.Time;
            TimeEnd = other.TimeEnd;
        }

        public override CircleTimemark[] GetTimemark()
        {
            if (s_spinnerEndTimemark == null)
            {
                s_spinnerStartTimemark = Resources.Load<CircleTimemark>("SpinnerTimemarkStart");
                s_spinnerEndTimemark = Resources.Load<CircleTimemark>("SpinnerTimemarkEnd");
            }

            CircleTimemark[] ret = new CircleTimemark[2];

            CircleTimemark toAdd = s_spinnerStartTimemark.Clone();
            toAdd.time = Time;
            toAdd.hitObject = this;
            ret[0] = toAdd;

            toAdd = s_spinnerEndTimemark.Clone();
            toAdd.time = TimeEnd;
            toAdd.hitObject = this;
            ret[1] = toAdd;

            return ret;
        }
    }
}
