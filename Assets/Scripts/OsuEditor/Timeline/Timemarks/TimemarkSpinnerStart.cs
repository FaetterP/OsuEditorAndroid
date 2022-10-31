using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerStart : TimemarkHitObject
    {
        private OsuSpinner _spinner;
        private CanvasHolder _holder;

        private static TimemarkSpinnerStart s_spinnerStartTimemark;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        protected override void ApplyTime(int newTime)
        {
            int duration = _spinner.TimeEnd - _spinner.Time;

            _spinner.SetTimeStart(newTime);
        }

        protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(false);
            _holder.SetActiveSlider(false);
        }

        public new TimemarkSpinnerStart Clone()
        {
            return (TimemarkSpinnerStart)MemberwiseClone();
        }

        public static TimemarkSpinnerStart GetTimemark(OsuSpinner obj)
        {
            if (s_spinnerStartTimemark == null)
                s_spinnerStartTimemark = Resources.Load<TimemarkSpinnerStart>("SpinnerTimemarkStart");

            TimemarkSpinnerStart ret = s_spinnerStartTimemark.Clone();
            ret._spinner = obj;
            ret._time = obj.Time;

            return ret;
        }

        public override void Init(OsuHitObject hitObjecet)
        {
            _spinner = hitObjecet as OsuSpinner;
        }
    }
}
