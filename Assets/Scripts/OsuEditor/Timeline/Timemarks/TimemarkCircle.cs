using Assets.Scripts.MapInfo;
using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkCircle : TimemarkHitObject
    {
        public OsuCircle _circle;
        private CanvasHolder _holder;

        private static TimemarkCircle s_circleTimemark;

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
            _creator.RemoveCircleMarkFromScreen(_time);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            DeleteButton button = col.gameObject.GetComponent<DeleteButton>();
            if (button == null)
                return;

            button.DeleteHitObject(_circle);

        }

        public override void Init(OsuHitObject circle)
        {
            _circle = circle as OsuCircle;
            //base.Init(circle);
        }

        public TimemarkCircle Clone()
        {
            return (TimemarkCircle)MemberwiseClone();
        }

        virtual protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(false);
        }

        public static TimemarkCircle GetCircleMark(OsuCircle circle)
        {
            if (s_circleTimemark == null)
                s_circleTimemark = Resources.Load<TimemarkCircle>("CircleTimemark");

            TimemarkCircle ret = s_circleTimemark.Clone();
            ret._time = circle.Time;
            ret._circle = circle;

            ret.Init(circle);
            return ret;
        }
    }
}
