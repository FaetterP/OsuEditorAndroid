using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Timeline
{
    class Timemark : MonoBehaviour, IComparable<Timemark>
    {
        private CreatorTimemarks creator;
        public int time;
        public Color color;
        public int height;

        void Start()
        {
            creator = FindObjectOfType<CreatorTimemarks>();
            GetComponent<Image>().color = color;
            var t = transform.localScale;
            t.y = height;
            transform.localScale = t;
        }

        void Update()
        {
            if (IsRightTime())
            {
                int x = OsuMath.GetMarkX(time, -500, 500, Global.MusicTime - Global.AR_ms, Global.MusicTime + Global.AR_ms);
                transform.localPosition = new Vector2(x, 0);
            }
            else
            {
                DestroyFromScreen();
            }
        }

        public void DestroyFromScreen()
        {
            creator.RemoveMarkFromScreen(time);
            Destroy(gameObject);
        }

        public Timemark Clone()
        {
            return (Timemark)MemberwiseClone();
        }

        public int CompareTo(Timemark other)
        {
            return time.CompareTo(other.time);
        }

        public void Init(Timemark other)
        {
            height = other.height;
            time = other.time;
            color = other.color;
        }

        public bool IsRightTime()
        {
            return Global.MusicTime > time - Global.AR_ms && Global.MusicTime < time + Global.AR_ms;
        }
    }
}
