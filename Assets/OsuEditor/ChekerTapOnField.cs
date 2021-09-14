using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.OsuEditor
{
    class ChekerTapOnField : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks creator;
                         private OsuCircle circle;
                         private OsuSlider slider;
                         private OsuSpinner spinner;

        void Awake()
        {
            circle = Resources.Load<GameObject>("OsuCircle").GetComponent<OsuCircle>();
            slider = Resources.Load<GameObject>("OsuSlider").GetComponent<OsuSlider>();
            spinner = Resources.Load<GameObject>("OsuSpinner").GetComponent<OsuSpinner>();
        }

        void OnMouseDown()
        {
            Touch touch = Input.GetTouch(0);
            var pos = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
            pos = OsuMath.UnityCoordsToOsu(pos);
         
            switch (Global.LeftStatus)
            {
                case LeftStatus.Circle:
                    OsuCircle added_ci = circle.Clone();
                    added_ci.SetCoords(pos);
                    added_ci.Time = Global.MusicTime;
                    added_ci.combo_sum = 1;
                    Global.Map.OsuHitObjects.Add(added_ci);
                    break;

                case LeftStatus.Slider:
                    OsuSlider added_sl = slider.Clone();
                    added_sl.SetCoords(pos);
                    added_sl.Time = Global.MusicTime;
                    added_sl.combo_sum = 2;
                    added_sl.SliderPoints = new List<SliderPoint>();
                    added_sl.SliderPoints.Add(new SliderPoint(256, 176));
                    added_sl.CountOfSlides = 1;
                    added_sl.Length = 100 * Global.Map.Difficulty.SliderMultiplier;
                    added_sl.UpdateTimeEnd();
                    Global.Map.OsuHitObjects.Add(added_sl);
                    break;

                case LeftStatus.Spinner:
                    OsuSpinner added_sp = spinner.Clone();
                    added_sp.SetCoords(256, 192);
                    added_sp.Time = Global.MusicTime;
                    added_sp.TimeEnd = Global.MusicTime+1000;
                    Global.Map.OsuHitObjects.Add(added_sp);
                    break;
            }
            Global.Map.OsuHitObjects.Sort();
            Global.Map.UpdateComboInfos();
            creator.UpdateCircleMarks();
        }
    }
}
