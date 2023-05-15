using Assets.Scripts.MapInfo.HitObjects;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSpinnerEndGO : TimemarkHitObjectGO
    {
        private OsuSpinner _spinner;
        private CanvasHolder _holder;

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

        public override void Init(Timemark timemark)
        {
            _spinner = (timemark as TimemarkHitObject).HitObject as OsuSpinner;
            base.Init(timemark);
        }
    }
}
