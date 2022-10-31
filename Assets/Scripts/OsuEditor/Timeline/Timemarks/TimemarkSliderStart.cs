using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSliderStart : TimemarkHitObject
    {
        private OsuSlider _slider;
        private CanvasHolder _holder;

        private static TimemarkSliderStart s_sliderBeginTimemark;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        void OnMouseDown()
        {
            Global.SelectedHitObject = _slider;
            ActiveCanvases();
            CheckMove();
        }

        private void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(true);
        }

        protected override void ApplyTime(int newTime)
        {
            _slider.SetTimeStart(newTime);
        }

        public new TimemarkSliderStart Clone()
        {
            return (TimemarkSliderStart)MemberwiseClone();
        }

        public static TimemarkSliderStart GetSliderStartMark(OsuSlider slider)
        {
            if (s_sliderBeginTimemark == null)
            {
                s_sliderBeginTimemark = Resources.Load<TimemarkSliderStart>("SliderTimemarkStart");
            }

            TimemarkSliderStart mark = s_sliderBeginTimemark.Clone();
            mark._time = slider.Time;
            mark._slider = slider;

            return mark;
        }

        public override void Init(OsuHitObject hitObjecet)
        {
            _slider = hitObjecet as OsuSlider;
        }
    }
}
