using Assets.Scripts.MapInfo.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSlider : TimemarkHitObject
    {
        private static TimemarkSlider s_sliderMiddleTimemark;
        private static TimemarkSlider s_sliderEndTimemark;

        private OsuSlider _slider;
        private CanvasHolder _holder;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        void OnMouseDown()
        {
            Global.SelectedHitObject = _slider;
            ActiveCanvases();
        }

        protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(true);
        }

        public static TimemarkSlider GetSliderMiddleMark(OsuSlider obj, int time)
        {
            if (s_sliderMiddleTimemark == null)
            {
                s_sliderMiddleTimemark = Resources.Load<TimemarkSlider>("SliderTimemarkMiddle");
            }

            TimemarkSlider mark = s_sliderMiddleTimemark.Clone();
            mark._time = time;
            mark._slider = obj;

            return mark;
        }

        public static TimemarkSlider GetSliderEndMark(OsuSlider slider)
        {
            if (s_sliderEndTimemark == null)
            {
                s_sliderEndTimemark = Resources.Load<TimemarkSlider>("SliderTimemarkEnd");
            }

            TimemarkSlider mark = s_sliderEndTimemark.Clone();
            mark._time = slider.TimeEnd;
            mark._slider = slider;
            return mark;
        }

        public new TimemarkSlider Clone()
        {
            return (TimemarkSlider)MemberwiseClone();
        }

        protected override void ApplyTime(int newTime) { }

        public override void Init(OsuHitObject hitObjecet)
        {
            _slider = hitObjecet as OsuSlider;
        }
    }
}
