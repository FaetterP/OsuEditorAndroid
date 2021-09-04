using Assets.Elements;
using Assets.MapInfo;
using System.Collections.Generic;
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
                    if (Global.MusicTime > t.time - Global.AR_ms && Global.MusicTime < t.time + Global.AR_ms)
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

            CircleTimemark toCreateCircle = Resources.Load<GameObject>("CircleTimemark").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSpinnerStart = Resources.Load<GameObject>("SpinnerTimemarkStart").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSpinnerEnd = Resources.Load<GameObject>("SpinnerTimemarkEnd").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderStart = Resources.Load<GameObject>("SliderTimemarkStart").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderMiddle = Resources.Load<GameObject>("SliderTimemarkMiddle").GetComponent<CircleTimemark>();
            CircleTimemark toCreateSliderEnd = Resources.Load<GameObject>("SliderTimemarkEnd").GetComponent<CircleTimemark>();

            foreach (OsuHitObject t in Global.Map.OsuHitObjects)
            {
                if (t is OsuCircle)
                {
                    int index;
                    if (t is OsuSlider)
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateSliderStart.Clone();
                        index = OsuMath.GetIndexFromTime(t.Time);
                        toAdd.color = Global.Map.Colors[Global.Map.ComboColors[index]];
                        toAdd.time = t.Time;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);

                        toAdd = (CircleTimemark)toCreateSliderEnd.Clone();
                        index = OsuMath.GetIndexFromTime(t.Time);
                        toAdd.color = Global.Map.Colors[Global.Map.ComboColors[index]];
                        toAdd.time = (t as OsuSlider)._timeEnd;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);

                        TimingPoint timingPoint = OsuMath.GetNearestTimingPointLeft(t.Time, false);
                        for (int i = 1; i < (t as OsuSlider).CountOfSlides; i++)
                        {
                            toAdd = (CircleTimemark)toCreateSliderMiddle.Clone();
                            index = OsuMath.GetIndexFromTime(t.Time);
                            toAdd.color = Global.Map.Colors[Global.Map.ComboColors[index]];
                            toAdd.time = t.Time + (int)OsuMath.SliderLengthToAddedTime((t as OsuSlider).Length, timingPoint.Mult, timingPoint.BeatLength) * i;
                            toAdd.hitObject = t;
                            CircleMarksToCreate.Add(toAdd);
                        }
                    }
                    else
                    {
                        CircleTimemark toAdd = (CircleTimemark)toCreateCircle.Clone();
                        index = OsuMath.GetIndexFromTime(t.Time);
                        toAdd.color = Global.Map.Colors[Global.Map.ComboColors[index]];
                        toAdd.time = t.Time;
                        toAdd.hitObject = t;
                        CircleMarksToCreate.Add(toAdd);
                    }
                }
                if (t is OsuSpinner)
                {
                    CircleTimemark toAdd = (CircleTimemark)toCreateSpinnerStart.Clone();
                    toAdd.color = Color.white;
                    toAdd.time = t.Time;
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
