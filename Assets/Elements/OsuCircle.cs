using Assets.OsuEditor;
using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    class OsuCircle : OsuHitObject
    {
        public int ComboColorNum;
        public int number=-3;
        public int combo_sum = 0;
        // public Color Color = Color.white;
        public bool whisle, finish, clap;
        public int Sampleset, Additions;

        protected bool isMoving = false;

        void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(x, y));
            GetComponent<PrinterNumber>().number = number;
            GetComponent<PrinterNumber>().Print();
            GetComponent<Image>().color = Global.Map.Colors[ComboColorNum];
        }

        private bool isStart = true;
        void OnEnable()
        {
            if (isStart) { isStart = false; return; }
            CreatorHitObjects.RemoveObjectFromScreen(time);
            Destroy(gameObject);
        }

        void Update()
        {
            int razn = time - Global.MusicTime;
            if (razn > Global.AR_ms || razn < 0)
            {
                CreatorHitObjects.RemoveObjectFromScreen(time);
                Destroy(gameObject);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (isMoving) 
                        { 
                            var poss=transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                            poss.z = 0;
                            transform.localPosition = poss;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (isMoving)
                        {
                            OsuHitObject obj = OsuMath.GetHitObjectFromTime(time);
                            var pos = OsuMath.UnityCoordsToOsu(transform.localPosition);
                            if (pos.x < 0) { pos.x = 0; }
                            if (pos.y < 0) { pos.y = 0; }
                            if (pos.x > 512) { pos.x = 512; }
                            if (pos.y > 384) { pos.y = 384; }
                            obj.x = (int)pos.x;
                            obj.y = (int)pos.y;
                            CreatorHitObjects.RemoveObjectFromScreen(time);
                            Destroy(gameObject);
                            isMoving = false;
                        }
                        break;
                }
            }
        }
        void OnMouseDown()
        {
            isMoving = true;   
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(x + ",");
            sb.Append(y + ",");
            sb.Append(time + ",");
            sb.Append(combo_sum + ",");

            int num = 0;
            if (whisle) { num += 2; }
            if (finish) { num += 4; }
            if (clap) { num += 8; }
            sb.Append(num + ",");

            sb.Append(Sampleset + ":");
            sb.Append(Additions + ":");
            sb.Append("0:0:");

            return sb.ToString();
        }
    }
}
