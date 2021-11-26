using Assets.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _holder.SetActiveSpinner(false);
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
