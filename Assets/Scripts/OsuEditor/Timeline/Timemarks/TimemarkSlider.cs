using Assets.Scripts.OsuEditor.HitObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSlider : TimemarkCircle
    {
        private static TimemarkSlider s_sliderMiddleTimemark;
        private static TimemarkSlider s_sliderEndTimemark;

        protected override void ApplyTime(int newTime)
        {
        }

        override protected void ActiveCanvases()
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
            mark._hitObject = obj;

            return mark;
        }

        public static TimemarkSlider GetSliderEndMark(OsuSlider obj)
        {
            if (s_sliderEndTimemark == null)
            {
                s_sliderEndTimemark = Resources.Load<TimemarkSlider>("SliderTimemarkEnd");
            }

            TimemarkSlider mark = s_sliderEndTimemark.Clone();
            mark._time = obj.TimeEnd;
            mark._hitObject = obj;

            return mark;
        }

        public new TimemarkSlider Clone()
        {
            return (TimemarkSlider)MemberwiseClone();
        }
    }
}
