using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.Utilities;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkSliderGO : TimemarkHitObjectGO
    {
        private OsuSlider _slider;
        private CanvasHolder _holder;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        void Start()
        {
            GetComponent<Image>().color = _slider.ComboColor;

            PrinterNumber printer = gameObject.AddComponent<PrinterNumber>();
            printer.Print(_slider.ComboNumber);
        }

        void OnMouseDown()
        {
            Global.SelectedHitObject = _slider;
            ActiveCanvases();
        }

        protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(true);
        }

        protected override void ApplyTime(int newTime) { }

        public override void Init(Timemark timemark)
        {
            _slider = (timemark as TimemarkHitObject).HitObject as OsuSlider;
            base.Init(timemark);
        }
    }
}
