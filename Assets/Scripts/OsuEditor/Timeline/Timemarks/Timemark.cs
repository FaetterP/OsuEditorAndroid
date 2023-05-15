using System;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class Timemark: IComparable<Timemark>
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
            return _time.CompareTo(other._time);
        }

        public abstract void SpawnGameObject();
    }
}
