using Assets.MapInfo;
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
    class OsuSlider : OsuCircle
    {
        public double length;
        public int time_end;
        public int CountOfSlides = 1;
        public List<SliderPoint> SliderPoints = new List<SliderPoint>();
        public List<Vector2> BezePoints = new List<Vector2>();

        //private bool isMoving = false;

        private LineRenderer thisLineRenderer;
        void Awake()
        {
            thisLineRenderer = GetComponent<LineRenderer>();
        }
        void Start()
        {
            GetComponent<PrinterNumber>().number = number;
            GetComponent<PrinterNumber>().Print();
            transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(x, y));
            PrintSliderPoints();
            UpdateBezePoints();
            PrintBezePoints();
        }
        void OnMouseDown()
        {
            isMoving = true;
        }
            void Update()
        {
            int razn = time - Global.MusicTime;
            if (time - Global.MusicTime > Global.AR_ms || time_end - Global.MusicTime < 0)
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
                            var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
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
                            
                            for(int i=0; i<(obj as OsuSlider).SliderPoints.Count; i++)
                            {
                                (obj as OsuSlider).SliderPoints[i].x += -obj.x + pos.x;
                                (obj as OsuSlider).SliderPoints[i].y += -obj.y + pos.y;
                            }
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
        public void UpdateTimeEnd()
        {
            TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(time, false);
            //Debug.Log(OsuMath.ConvertTimestampToSring(time)+" "+ OsuMath.ConvertTimestampToSring(timingPoint.Offset) +" "+ timingPoint.Mult);
            time_end = time + (int)OsuMath.SliderLengthToAddedTime(length, timingPoint.Mult, timingPoint.BeatLength) * CountOfSlides;
        }

        public void UpdateBezePoints()
        {
            BezePoints.Clear();
            List<SliderPoint> toBeze = new List<SliderPoint>();
            toBeze.Add(new SliderPoint(x, y));
            foreach (var t in SliderPoints)
            {
                if (t.isStatic)
                {
                    toBeze.Add(t);
                    List<Vector2> vec = OsuMath.GetInterPointBeze(toBeze, 100);

                    for (int i = vec.Count-1; i >=0; i--)
                    {
                        BezePoints.Add(vec[i]);
                    }

                    toBeze.Clear();
                    toBeze.Add(t);
                }
                else
                {
                    toBeze.Add(t);
                }
            }

            List<Vector2> vec2 = OsuMath.GetInterPointBeze(toBeze, 100);
            for (int i = vec2.Count - 1; i >= 0; i--)
            {
                BezePoints.Add(vec2[i]);
            }
        }
        public void PrintBezePoints()
        {
            GameObject circle = Resources.Load("SliderBezePoint") as GameObject;
            foreach (var t in BezePoints)
            {
                GameObject created = Instantiate(circle, transform);
                var vec = OsuMath.OsuCoordsToUnity(t);
                created.transform.localPosition = new Vector2(vec.x, vec.y) - new Vector2(transform.localPosition.x, transform.localPosition.y);
                created.GetComponent<Image>().color = Global.Map.Colors[ComboColorNum];
                created.transform.SetAsFirstSibling();
            }
        }

        public void UpdateLength()
        {
            length = 0;
            for(int i = 0; i < BezePoints.Count-1; i++)
            {
                double add_length = Math.Sqrt(Math.Pow(BezePoints[i].x - BezePoints[i + 1].x, 2) + Math.Pow(BezePoints[i].y - BezePoints[i + 1].y, 2));
                length += add_length;
            }
        }

        private void PrintSliderPoints()
        {
            UpdateLine();
            GameObject go = Resources.Load("SliderPoint") as GameObject;
            SliderPointGameObject spgo = go.GetComponent<SliderPointGameObject>();
            foreach(var t in SliderPoints)
            {
                SliderPointGameObject created = Instantiate(spgo, transform);
                created.transform.SetAsLastSibling();
                created.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2((float)t.x, (float)t.y))-new Vector2(transform.localPosition.x, transform.localPosition.y);
                created.thisSlider = this;
                created.thisPoint = t;
            }
        }

        public void UpdateLine()
        {
            thisLineRenderer.positionCount = SliderPoints.Count + 1;
            thisLineRenderer.SetPosition(0, transform.localPosition);
            for(int i = 0; i < SliderPoints.Count; i++)
            {
                thisLineRenderer.SetPosition(i+1, OsuMath.OsuCoordsToUnity(new Vector2((float)SliderPoints[i].x, (float)SliderPoints[i].y)));
            }
            for(int i = 0; i < thisLineRenderer.positionCount; i++)
            {
                thisLineRenderer.SetPosition(i, transform.TransformPoint(thisLineRenderer.GetPosition(i) - new Vector3(transform.localPosition.x, transform.localPosition.y, 100)));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //64,247,300,2,0,P|247:66|410:221,1,525
            sb.Append(x + ",");
            sb.Append(y + ",");
            sb.Append(time + ",");
            sb.Append(combo_sum + ",");

            int num = 0;
            if (whisle) { num += 2; }
            if (finish) { num += 4; }
            if (clap) { num += 8; }
            sb.Append(num + ",");
            sb.Append("P|");
            foreach (var t in SliderPoints)
            {
                sb.Append(t.x + ":" + t.y + "|");
                if (t.isStatic)
                {
                    sb.Append(t.x + ":" + t.y + "|");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(","+CountOfSlides + ",");
            sb.Append(length);

            return sb.ToString();
        }
    }
}
