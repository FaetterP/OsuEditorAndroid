using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerStartGO : TimemarkHitObjectGO
    {
        private OsuSpinner _spinner;
        private CanvasHolder _holder;

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

        public override void Init(Timemark timemark)
        {
            _spinner = (timemark as TimemarkHitObject).HitObject as OsuSpinner;
            base.Init(timemark);
        }
    }
}
