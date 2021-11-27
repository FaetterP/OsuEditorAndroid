using Assets.Elements;
using Assets.MapInfo;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Timeline.Timemarks
{
    class TimemarkCircle : MovableTimemark
    {
        protected OsuHitObject _hitObject;
        protected CanvasHolder _holder;

        private static TimemarkCircle s_circleTimemark;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            _holder = FindObjectOfType<CanvasHolder>();
        }

        protected override void ApplyTime(int newTime)
        {
            OsuHitObject circle = OsuMath.GetHitObjectFromTime(_hitObject.Time);
            circle.Time = newTime;
        }

        void Start()
        {
            ComboInfo combo = Global.Map.GetComboInfo(_hitObject.Time);
            GetComponent<Image>().color = combo.Color;

            PrinterNumber printer = gameObject.AddComponent<PrinterNumber>();
            printer.Print(combo.Number);
        }

        void OnMouseDown()
        {
            Global.SelectedHitObject = OsuMath.GetHitObjectFromTime(_hitObject.Time);
            ActiveCanvases();
            CheckMove();
        }

        void OnDestroy()
        {
            _creator.RemoveCircleMarkFromScreen(_time);
        }

        public void Init(TimemarkCircle other)
        {
            _hitObject = other._hitObject;
            base.Init(other);
        }

        public new TimemarkCircle Clone()
        {
            return (TimemarkCircle)MemberwiseClone();
        }

        virtual protected void ActiveCanvases()
        {
            _holder.SetActiveCircle(true);
            _holder.SetActiveSlider(false);
        }

        public static TimemarkCircle GetCircleMark(OsuHitObject obj)
        {
            if (s_circleTimemark == null)
                s_circleTimemark = Resources.Load<TimemarkCircle>("CircleTimemark");

            TimemarkCircle ret = s_circleTimemark.Clone();
            ret._time = obj.Time;
            ret._hitObject = obj;
            return ret;
        }
    }
}
