using Assets.Elements;
using UnityEngine;

namespace Assets.OsuEditor.Timeline.Timemarks
{
    class TimemarkSliderStart : TimemarkCircle
    {
        private static TimemarkSliderStart s_sliderBeginTimemark;

        override protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(true);
        }

        protected override void ApplyTime(int newTime)
        {
            OsuSlider slider = OsuMath.GetHitObjectFromTime(_hitObject.Time) as OsuSlider;
            int duration = slider.TimeEnd - slider.Time;
            slider.Time = newTime;
            slider.TimeEnd = newTime + duration;
        }

        public new TimemarkSliderStart Clone()
        {
            return (TimemarkSliderStart)MemberwiseClone();
        }

        public static TimemarkSliderStart GetSliderStartMark(OsuSlider obj)
        {
            if (s_sliderBeginTimemark == null)
            {
                s_sliderBeginTimemark = Resources.Load<TimemarkSliderStart>("SliderTimemarkStart");
            }

            TimemarkSliderStart mark = s_sliderBeginTimemark.Clone();
            mark._time = obj.Time;
            mark._hitObject = obj;

            return mark;
        }
    }
}
