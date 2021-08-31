using Assets.Elements;
using Assets.MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.OsuEditor.Timeline
{
    class CreatorTimemarks : MonoBehaviour
    {
        
        public static List<Timemark> MarksToCreate = new List<Timemark>();
        public static List<CircleTimemark> CircleMarksToCreate = new List<CircleTimemark>();
        private static List<int> MarksOnScreen = new List<int>();
        private static List<int> CircleMarksOnScreen = new List<int>();

        void Awake()
        {
            UpdateCircleMarks();
        }

        void Update()
        {
            foreach (var t in MarksToCreate)
            {
                if (!MarksOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime > t.time - Global.AR_ms && Global.MusicTime < t.time+Global.AR_ms)
                    {
                        Timemark created = Instantiate(t, transform);
                        created.height = t.height;
                        created.time = t.time;
                        created.color = t.color;
                        MarksOnScreen.Add(t.time);
                    }
                }
            }
            foreach (var t in CircleMarksToCreate)
            {
                if (!CircleMarksOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime > t.time - Global.AR_ms && Global.MusicTime < t.time + Global.AR_ms)
                    {
                        CircleTimemark created = Instantiate(t, transform);
                        created.hitObject = t.hitObject;
                        created.time = t.time;
                        created.color = t.color;
                        CircleMarksOnScreen.Add(t.time);
                    }
                }
            }
        }

        public static void UpdateCircleMarks()
        {
            foreach (var t in FindObjectsOfType<CircleTimemark>())
            {
                RemoveCircleMarkFromScreen(t.time);
                Destroy(t.gameObject);
            }
            CircleMarksToCreate.Clear();
            GameObject go = Resources.Load("CircleTimemark") as GameObject;
            CircleTimemark toCreateCircle = go.GetComponent<CircleTimemark>();
            go = Resources.Load("SpinnerTimemarkStart") as GameObject;
            CircleTimemark toCreateSpinnerStart = go.GetComponent<CircleTimemark>();
            go = Resources.Load("SpinnerTimemarkEnd") as GameObject;
            CircleTimemark toCreateSpinnerEnd = go.GetComponent<CircleTimemark>();
            go = Resources.Load("SliderTimemarkStart") as GameObject;
            CircleTimemark toCreateSliderStart = go.GetComponent<CircleTimemark>();
            go = Resources.Load("SliderTimemarkMiddle") as GameObject;
            CircleTimemark toCreateSliderMiddle = go.GetComponent<CircleTimemark>();
            go = Resources.Load("SliderTimemarkEnd") as GameObject;
            CircleTimemark toCreateSliderEnd = go.GetComponent<CircleTimemark>();
            foreach (OsuHitObject t in Global.Map.OsuHitObjects)
            {
                if(t is OsuCircle)
                {
                    if(t is OsuSlider)
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateSliderStart.Clone();
                        toAdd.color = Global.Map.Colors[(t as OsuCircle).ComboColorNum];
                        toAdd.time = t.time;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);

                        toAdd = (CircleTimemark)toCreateSliderEnd.Clone();
                        toAdd.color = Global.Map.Colors[(t as OsuCircle).ComboColorNum];
                        toAdd.time = (t as OsuSlider)._timeEnd;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);

                        TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(t.time, false);
                        for (int i=1;i<(t as OsuSlider).CountOfSlides; i++)
                        {
                            toAdd = (CircleTimemark)toCreateSliderMiddle.Clone();
                            toAdd.color = Global.Map.Colors[(t as OsuCircle).ComboColorNum];
                            toAdd.time = t.time+(int)OsuMath.SliderLengthToAddedTime((t as OsuSlider).Length, timingPoint.Mult, timingPoint.BeatLength)*i;
                            toAdd.hitObject = t;
                            CircleMarksToCreate.Add(toAdd);
                        }
                    }
                    else
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateCircle.Clone();
                        toAdd.color = Global.Map.Colors[(t as OsuCircle).ComboColorNum];
                        toAdd.time = t.time;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);
                    }
                }
                if(t is OsuSpinner)
                {
                    CircleTimemark toAdd = (CircleTimemark)toCreateSpinnerStart.Clone();
                    toAdd.color = Color.white;
                    toAdd.time = t.time;
                    toAdd.hitObject = t;
                    CircleMarksToCreate.Add(toAdd);

                    toAdd = (CircleTimemark)toCreateSpinnerEnd.Clone();
                    toAdd.color = Color.white;
                    toAdd.time = (t as OsuSpinner).TimeEnd;
                    toAdd.hitObject = t;
                    CircleMarksToCreate.Add(toAdd);
                }
            }
        }

        public static void RemoveMarkFromScreen(int time)
        {
            MarksOnScreen.Remove(time);
        }
        public static void RemoveCircleMarkFromScreen(int time)
        {
            CircleMarksOnScreen.Remove(time);
        }
    }
}
