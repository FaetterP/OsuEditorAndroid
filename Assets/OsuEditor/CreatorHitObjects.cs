using Assets.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.OsuEditor
{
    class CreatorHitObjects : MonoBehaviour
    {
        private static List<int> HitObjectsOnScreen = new List<int>();

        void Update()
        {
            foreach(OsuHitObject t in Global.Map.OsuHitObjects)
            {
                if (Global.MusicTime + Global.AR_ms<t.time) { break; }
                if (t is OsuCircle && !(t is OsuSlider) && !HitObjectsOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime>t.time-Global.AR_ms && Global.MusicTime<t.time)
                    {
                        
                        OsuHitObject created = Instantiate(t, gameObject.transform);
                        created.time = t.time;
                        created.x = t.x;
                        created.y = t.y;
                        (created as OsuCircle).number = (t as OsuCircle).number;
                        (created as OsuCircle).combo_sum = (t as OsuCircle).combo_sum;
                        (created as OsuCircle).ComboColorNum = (t as OsuCircle).ComboColorNum;
                        created.transform.SetAsFirstSibling();
                      //  (created as OsuCircle).Color = (t as OsuCircle).Color;
                        HitObjectsOnScreen.Add(t.time);
                    }
                }
                if (t is OsuSlider && !HitObjectsOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime < (t as OsuSlider).time_end && Global.MusicTime > t.time - Global.AR_ms)
                    {
                        OsuHitObject created = Instantiate(t, gameObject.transform);
                        created.time = t.time;
                        created.x = t.x;
                        created.y = t.y;
                        (created as OsuSlider).time_end = (t as OsuSlider).time_end;
                        (created as OsuSlider).number = (t as OsuSlider).number;
                        (created as OsuSlider).combo_sum = (t as OsuSlider).combo_sum;
                        (created as OsuSlider).ComboColorNum = (t as OsuSlider).ComboColorNum;
                     //   (created as OsuSlider).Color = (t as OsuSlider).Color;
                        (created as OsuSlider).CountOfSlides = (t as OsuSlider).CountOfSlides;
                        (created as OsuSlider).length = (t as OsuSlider).length;
                        (created as OsuSlider).SliderPoints = (t as OsuSlider).SliderPoints;
                        created.transform.SetAsFirstSibling();
                        HitObjectsOnScreen.Add(t.time);
                    }
                }
                if (t is OsuSpinner && !HitObjectsOnScreen.Contains(t.time))
                {
                    if (Global.MusicTime < (t as OsuSpinner).time_end && Global.MusicTime > t.time - Global.AR_ms)
                    {
                        OsuHitObject created = Instantiate(t, gameObject.transform);
                        created.x = 256;
                        created.y = 192;
                        created.time = t.time;
                        (created as OsuSpinner).time_end = (t as OsuSpinner).time_end;
                        created.transform.SetAsFirstSibling();
                        HitObjectsOnScreen.Add(t.time);
                    }
                }
            }
        }


        public static void RemoveObjectFromScreen(int time)
        {
            HitObjectsOnScreen.Remove(time);
        }
    }
}
