﻿using Assets.Elements;
using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Timeline
{
    class CircleTimemark : MonoBehaviour, ICloneable, IComparable<CircleTimemark>
    {
        public OsuHitObject hitObject;
        private bool isMoving = false;
        public int time;
        public Color color;
        CanvasHolder holder;
        
        void Awake()
        {
            holder = FindObjectOfType<CanvasHolder>();
        }

        void Start()
        {
            GetComponent<Image>().color = color;
            if (hitObject is OsuCircle)
            {
                gameObject.AddComponent<PrinterNumber>();
                GetComponent<PrinterNumber>().number = (hitObject as OsuCircle).number;
                GetComponent<PrinterNumber>().Print();
            }
        }
        void OnMouseDown()
        {
            if (Global.LeftStatus == LeftStatus.Select)
            {
                Global.SelectedHitObject = OsuMath.GetHitObjectFromTime(hitObject.time);

                holder.SliderCanvas.gameObject.SetActive(false);
                holder.CircleCanvas.gameObject.SetActive(false);
                holder.SpinnerCanvas.gameObject.SetActive(false);
                if (hitObject is OsuSlider)
                {
                    holder.SliderCanvas.gameObject.SetActive(true);
                    holder.CircleCanvas.gameObject.SetActive(true);
                    holder.SpinnerCanvas.gameObject.SetActive(false);
                }
                else if (hitObject is OsuCircle)
                {
                    holder.SliderCanvas.gameObject.SetActive(false);
                    holder.CircleCanvas.gameObject.SetActive(true);
                    holder.SpinnerCanvas.gameObject.SetActive(false);
                }
                else
                {
                    holder.SliderCanvas.gameObject.SetActive(false);
                    holder.CircleCanvas.gameObject.SetActive(false);
                    holder.SpinnerCanvas.gameObject.SetActive(true);
                }
            }
            else if (Global.LeftStatus == LeftStatus.Move)
            {
                isMoving = true;
            }
        }
        void Update()
        {
            if (Input.touchCount > 0 && isMoving)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:

                        var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                        poss.z = 0;
                        transform.localPosition = poss;
                        break;

                    case TouchPhase.Ended:
                        UpdateTime();
                        isMoving = false;
                        break;
                }
            }
            else if (Global.MusicTime > time - Global.AR_ms && Global.MusicTime < time + Global.AR_ms && !isMoving)
            {
                int x = OsuMath.GetMarkX(time, -500, 500, Global.MusicTime - Global.AR_ms, Global.MusicTime + Global.AR_ms);
                transform.localPosition = new Vector2(x, 0);
            }
            else
            {
                CreatorTimemarks.RemoveCircleMarkFromScreen(time);
                Destroy(gameObject);
            }
        }

        private void UpdateTime()
        {
            Timemark[] marks = FindObjectsOfType<Timemark>();
            double dist = 1000000;
            int time = 10000000;
            int added_spinner_time = 0;
            if (hitObject is OsuSpinner) { added_spinner_time = (hitObject as OsuSpinner).time_end - hitObject.time; }
            foreach (var t in marks)
            {
                double newdist = Math.Abs(t.transform.localPosition.x - transform.localPosition.x);
                if (newdist < dist)
                {
                    dist = newdist;
                    time = t.time;
                }
            }
            //GameObject.Find("DEBUG").GetComponent<Text>().text = time.ToString();
            OsuMath.GetHitObjectFromTime(this.time).time = time;
            Global.Map.OsuHitObjects.Sort();

            if (hitObject is OsuSlider)
            {
                (hitObject as OsuSlider).UpdateTimeEnd();
            }
            if (hitObject is OsuSpinner)
            {
                (hitObject as OsuSpinner).time_end= (hitObject as OsuSpinner).time + added_spinner_time;
            }

            foreach (var t in FindObjectsOfType<OsuHitObject>())
            {
                CreatorHitObjects.RemoveObjectFromScreen(t.time);
                Destroy(t.gameObject);
            }
            Global.Map.UpdateComboColours();
            Global.Map.UpdateNumbers();
            CreatorTimemarks.UpdateCircleMarks();
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(CircleTimemark other)
        {
            return time.CompareTo(other.time);
        }
    }
}