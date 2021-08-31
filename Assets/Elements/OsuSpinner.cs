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
                if (value <= time) { throw new ArgumentException(); }
                _timeEnd = value;
            }
        }


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
            if (Global.MusicTime < time - Global.AR_ms)
            {
                OsuEditor.CreatorHitObjects.RemoveObjectFromScreen(time);
                Destroy(gameObject);
            }
            else if (Global.MusicTime < time)
            {
                transform.rotation = Quaternion.Euler(0, 0, time);
                _thisImage.color = new Color(1, 1, 1, 0.1f);
            }
            else if (Global.MusicTime > time && Global.MusicTime < TimeEnd)
            {
                transform.rotation = Quaternion.Euler(0, 0, Global.MusicTime);
                _thisImage.color = new Color(1, 1, 1, 0.1f + 0.9f * ((Global.MusicTime - time * 1.0f) / (TimeEnd - time)));
            }
            else
            {
                OsuEditor.CreatorHitObjects.RemoveObjectFromScreen(time);
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
            return "256,192," + time + ",12,8," + TimeEnd + ",0:1:0:0";
        }
    }
}
