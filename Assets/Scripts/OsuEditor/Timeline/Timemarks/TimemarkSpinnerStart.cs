using Assets.Scripts.Elements;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerStart : TimemarkCircle
    {
        private static TimemarkSpinnerStart s_spinnerStartTimemark;

        void Start() { }

        protected override void ApplyTime(int newTime)
        {
            OsuSpinner spinner = Global.Map.GetHitObjectFromTime(_hitObject.Time) as OsuSpinner;
            int duration = spinner.TimeEnd - spinner.Time;

            spinner.Time = newTime;
            spinner.TimeEnd = newTime + duration;
        }

        override protected void ActiveCanvases()
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
            ret._hitObject = obj;
            ret._time = obj.Time;

            return ret;
        }
    }
}
