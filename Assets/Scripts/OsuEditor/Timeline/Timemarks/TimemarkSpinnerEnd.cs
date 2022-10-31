using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerEnd : TimemarkHitObject
    {
        private OsuSpinner _spinner;
        private CanvasHolder _holder;

        private static TimemarkSpinnerEnd s_spinnerEndTimemark;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        void Start() { }

        protected override void ApplyTime(int newTime)
        {
            if (newTime < _spinner.Time)
                newTime = _spinner.Time + 1000;

            _spinner.SetTimeEnd(newTime);
        }

        protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(false);
            _holder.SetActiveSlider(false);
        }

        public new TimemarkSpinnerEnd Clone()
        {
            return (TimemarkSpinnerEnd)MemberwiseClone();
        }

        public static TimemarkSpinnerEnd GetTimemark(OsuSpinner spinner)
        {
            if (s_spinnerEndTimemark == null)
                s_spinnerEndTimemark = Resources.Load<TimemarkSpinnerEnd>("SpinnerTimemarkEnd");

            TimemarkSpinnerEnd ret = s_spinnerEndTimemark.Clone();
            ret._spinner = spinner;
            ret._time = spinner.TimeEnd;

            return ret;
        }

        public override void Init(OsuHitObject hitObjecet)
        {
            _spinner = hitObjecet as OsuSpinner;
        }
    }
}
