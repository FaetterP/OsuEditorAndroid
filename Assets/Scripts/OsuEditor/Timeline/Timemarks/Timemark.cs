using System;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class Timemark : IComparable<Timemark>
    {
        private int _time;
        public int Time
        {
            get
            {
                return _time;
            }
        }

        public Timemark(int time)
        {
            _time = time;
        }

        public bool IsRightTime()
        {
            return Global.MusicTime > _time - Global.AR_ms && Global.MusicTime < _time + Global.AR_ms;
        }

        public int CompareTo(Timemark other)
        {
            int result = _time.CompareTo(other._time);
            if (result != 0)
                return result;

            if (this is TimemarkHitObject && other is TimemarkLine)
                return 1;

            if (this is TimemarkLine && other is TimemarkHitObject)
                return -1;

            return 0;
        }

        public abstract void SpawnGameObject();
    }
}
