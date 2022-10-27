using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects.SliderStuff;
using Assets.Scripts.OsuEditor.Timeline;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class ChekerTapOnField : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks _creator;
                         private OsuCircle _circle;
                         private OsuSlider _slider;
                         private OsuSpinner _spinner;

        private void Awake()
        {
            _circle = Resources.Load<OsuCircle>("OsuCircle");
            _slider = Resources.Load<OsuSlider>("OsuSlider");
            _spinner = Resources.Load<OsuSpinner>("OsuSpinner");
        }

        private void OnMouseDown()
        {
            Touch touch = Input.GetTouch(0);
            var pos = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
            pos = OsuMath.UnityCoordsToOsu(pos);
         
            switch (Global.LeftStatus)
            {
                case LeftStatus.Circle:
                    OsuCircle added_ci = _circle.Clone();
                    added_ci.SetCoords(pos);
                    added_ci.Time = Global.MusicTime;
                    added_ci.combo_sum = 1;
                    Global.Map.AddHitObject(added_ci);
                    break;

                case LeftStatus.Slider:
                    OsuSlider added_sl = _slider.Clone();
                    added_sl.SetCoords(pos);
                    added_sl.Time = Global.MusicTime;
                    added_sl.combo_sum = 2;
                    added_sl.SliderPoints = new List<SliderPoint>();
                    added_sl.SliderPoints.Add(new SliderPoint(256, 176));
                    added_sl.CountOfSlides = 1;
                    added_sl.Length = 100 * Global.Map.Difficulty.SliderMultiplier;
                    added_sl.UpdateTimeEnd();
                    Global.Map.AddHitObject(added_sl);
                    break;

                case LeftStatus.Spinner:
                    OsuSpinner added_sp = _spinner.Clone();
                    added_sp.SetCoords(256, 192);
                    added_sp.Time = Global.MusicTime;
                    added_sp.TimeEnd = Global.MusicTime+1000;
                    Global.Map.AddHitObject(added_sp);
                    break;
            }
            _creator.UpdateCircleMarks();
        }
    }
}
