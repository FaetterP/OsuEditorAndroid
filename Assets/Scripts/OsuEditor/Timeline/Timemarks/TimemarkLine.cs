using Assets.Scripts.MapInfo;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkLine : Timemark
    {
        private Color _color;
        private int _height;

        private static TimemarkLine s_TimemarkLine;

        void Start()
        {
            GetComponent<Image>().color = _color;

            var newScale = transform.localScale;
            newScale.y = _height;
            transform.localScale = newScale;
        }

        public override void Init(Timemark other)
        {
            TimemarkLine otherLine = other as TimemarkLine;

            _height = otherLine._height;
            _color = otherLine._color;

            base.Init(other);
        }

        public static TimemarkLine GetTimingPointMark(TimingPoint point)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLine>("TimemarkLine");

            TimemarkLine mark = s_TimemarkLine.Clone();

            mark._time = point.Offset;
            mark._color = Color.green;
            mark._height = 100;


            if (point.isParent)
                mark._color = Color.red;

            return mark;
        }

        public static TimemarkLine GetBeatMark(int time)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLine>("TimemarkLine");

            TimemarkLine mark = s_TimemarkLine.Clone();

            mark._time = time;
            mark._color = Color.white;
            mark._height = 50;

            return mark;
        }

        public static TimemarkLine GetMiddleMark(int time, Color color)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLine>("TimemarkLine");

            TimemarkLine mark = s_TimemarkLine.Clone();

            mark._time = time;
            mark._color = color;
            mark._height = 30;

            return mark;
        }

        public new TimemarkLine Clone()
        {
            return (TimemarkLine)MemberwiseClone();
        }
    }
}
