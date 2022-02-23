using Assets.Elements;
using UnityEngine;

namespace Assets.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerEnd : TimemarkCircle
    {
        private static TimemarkSpinnerEnd s_spinnerEndTimemark;

        void Start() { }

        protected override void ApplyTime(int newTime)
        {
            OsuSpinner spinner = Global.Map.GetHitObjectFromTime(_hitObject.Time) as OsuSpinner;

            if (newTime < spinner.Time)
                newTime = spinner.Time + 1000;

            spinner.TimeEnd = newTime;
        }

        override protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(false);
            _holder.SetActiveSlider(false);
        }

        public new TimemarkSpinnerEnd Clone()
        {
            return (TimemarkSpinnerEnd)MemberwiseClone();
        }

        public static TimemarkSpinnerEnd GetTimemark(OsuSpinner obj)
        {
            if (s_spinnerEndTimemark == null)
                s_spinnerEndTimemark = Resources.Load<TimemarkSpinnerEnd>("SpinnerTimemarkEnd");

            TimemarkSpinnerEnd ret = s_spinnerEndTimemark.Clone();
            ret._hitObject = obj;
            ret._time = obj.TimeEnd;

            return ret;
        }
    }
}
