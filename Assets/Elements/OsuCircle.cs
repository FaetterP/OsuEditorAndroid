using Assets.MapInfo;
using Assets.OsuEditor;
using Assets.Utilities;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(PrinterNumber))]
    class OsuCircle : OsuHitObject
    {
        public int combo_sum;
        public bool Whisle, Finish, Clap;
        public int Sampleset, Additions;

        protected bool _isMoving = false;
        private bool _isStart = true;
        protected ComboInfo _comboInfo;
        public Color ComboColor
        {
            get
            {
                return _comboInfo.Color;
            }
        }
        public int Number
        {
            get
            {
                return _comboInfo.Number;
            }
        }

        void Start()
        {
            _comboInfo = Global.Map.GetComboInfo(Time);

            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(X, Y));
            GetComponent<PrinterNumber>().Print(_comboInfo.Number);
            GetComponent<Image>().color = _comboInfo.Color;
        }

        void OnEnable()
        {
            if (_isStart) { _isStart = false; return; }
            RemoveFromScreen();
        }

        void Update()
        {
            int razn = Time - Global.MusicTime;
            if (razn > Global.AR_ms || razn < 0)
            {
                RemoveFromScreen();
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (_isMoving) 
                        { 
                            var poss=transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                            poss.z = 0;
                            transform.localPosition = poss;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (_isMoving)
                        {
                            OsuHitObject obj = OsuMath.GetHitObjectFromTime(Time);
                            var pos = OsuMath.UnityCoordsToOsu(transform.localPosition);
                            obj.SetCoords(pos);
                            RemoveFromScreen();
                            _isMoving = false;
                        }
                        break;
                }
            }
        }

        void OnMouseDown()
        {
            _isMoving = true;   
        }

        public OsuCircle Clone()
        {
            return (OsuCircle)MemberwiseClone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(X + ",");
            sb.Append(Y + ",");
            sb.Append(Time + ",");
            sb.Append(combo_sum + ",");

            int num = 0;
            if (Whisle) { num += 2; }
            if (Finish) { num += 4; }
            if (Clap) { num += 8; }
            sb.Append(num + ",");

            sb.Append(Sampleset + ":");
            sb.Append(Additions + ":");
            sb.Append("0:0:");

            return sb.ToString();
        }

        public override bool IsRightTime()
        {
            return Global.MusicTime > Time - Global.AR_ms && Global.MusicTime < Time;
        }

        public override void Init(OsuHitObject obj)
        {
            OsuCircle other = obj as OsuCircle;
            Time = other.Time;
            SetCoords(other.X, other.Y);
            //number = other.number;
            combo_sum = other.combo_sum;
            //ComboColorNum = other.ComboColorNum;
        }
    }
}
