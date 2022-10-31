using System;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class Timemark : MonoBehaviour, IComparable<Timemark>
    {
        protected CreatorTimemarks _creator;
        protected int _time;
        public int Time
        {
            get
            {
                return _time;
            }
        }

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
        }

        void Update()
        {
            UpdateX();
        }

        protected void UpdateX()
        {
            if (IsRightTime())
            {
                int x = OsuMath.GetMarkX(_time, -500, 500, Global.MusicTime - Global.AR_ms, Global.MusicTime + Global.AR_ms);
                transform.localPosition = new Vector2(x, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            _creator.RemoveMarkFromScreen(_time);
        }

        void OnDisable()
        {
            Destroy(gameObject);
        }

        public virtual void Init(Timemark timemark)
        {
            _time = timemark.Time;
        }

        public bool IsRightTime()
        {
            return Global.MusicTime > _time - Global.AR_ms && Global.MusicTime < _time + Global.AR_ms;
        }

        //public Timemark Clone()
        //{
        //    return (Timemark)MemberwiseClone();
        //}

        public int CompareTo(Timemark other)
        {
            return _time.CompareTo(other._time);
        }
    }
}
