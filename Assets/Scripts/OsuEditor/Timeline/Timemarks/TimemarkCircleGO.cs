using Assets.Scripts.MapInfo;
using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkCircleGO : TimemarkHitObjectGO
    {
        public OsuCircle _circle;
        private CanvasHolder _holder;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        protected override void ApplyTime(int newTime)
        {
            _circle.SetTime(newTime);
        }

        void Start()
        {
            GetComponent<Image>().color = _circle.ComboColor;

            PrinterNumber printer = gameObject.AddComponent<PrinterNumber>();
            printer.Print(_circle.ComboNumber);
        }

        void OnMouseDown()
        {
            Global.SelectedHitObject = _circle;
            ActiveCanvases();
            CheckMove();
        }

        void OnDestroy()
        {
            _creator.RemoveTimemarkFromScreen(_timemark);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            DeleteButton button = col.gameObject.GetComponent<DeleteButton>();
            if (button == null)
                return;

            button.DeleteHitObject(_circle);

        }

        public override void Init(Timemark timemark)
        {
            _circle = (timemark as TimemarkHitObject).HitObject as OsuCircle;
            base.Init(timemark);
        }

        public TimemarkCircleGO Clone()
        {
            return (TimemarkCircleGO)MemberwiseClone();
        }

        virtual protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(false);
        }
    }
}
