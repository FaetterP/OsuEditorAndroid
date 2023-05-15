using Assets.Scripts.MapInfo;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkLineGO : TimemarkGO
    {
        private Color _color;
        private int _height;

        private static TimemarkLineGO s_TimemarkLine;

        void Start()
        {
            GetComponent<Image>().color = _color;

            var newScale = transform.localScale;
            newScale.y = _height;
            transform.localScale = newScale;
        }

        public void Init(TimemarkLine timemark)
        {
            _color = timemark.Color;
            _height = timemark.Height;

            base.Init(timemark);
        }

        public static TimemarkLineGO GetTimingPointMark(TimingPoint point)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLineGO>("TimemarkLine");

            TimemarkLineGO mark = s_TimemarkLine.Clone();

            mark._time = point.Offset;
            mark._color = Color.green;
            mark._height = 100;


            if (point.isParent)
                mark._color = Color.red;

            return mark;
        }

        public static TimemarkLineGO GetBeatMark(int time)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLineGO>("TimemarkLine");

            TimemarkLineGO mark = s_TimemarkLine.Clone();

            mark._time = time;
            mark._color = Color.white;
            mark._height = 50;

            return mark;
        }

        public static TimemarkLineGO GetMiddleMark(int time, Color color)
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLineGO>("TimemarkLine");

            TimemarkLineGO mark = s_TimemarkLine.Clone();

            mark._time = time;
            mark._color = color;
            mark._height = 30;

            return mark;
        }

        public new TimemarkLineGO Clone()
        {
            return (TimemarkLineGO)MemberwiseClone();
        }
    }
}
