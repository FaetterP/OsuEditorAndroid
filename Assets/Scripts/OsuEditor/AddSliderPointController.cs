using Assets.Scripts.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class AddSliderPointController : MonoBehaviour
    {
        void Update()
        {
            if (Input.touchCount == 2 && Global.SliderStatus == SliderStatus.Add)
            {
                Touch touch = Input.GetTouch(1);
                if(touch.phase== TouchPhase.Ended)
                {
                    var pos = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                    pos = OsuMath.UnityCoordsToOsu(pos);
                    SliderPoint added = new SliderPoint((int)pos.x, (int)pos.y);
                    (Global.SelectedHitObject as OsuSlider).SliderPoints.Add(added);
                    (Global.SelectedHitObject as OsuSlider).UpdateBezePoints();
                    (Global.SelectedHitObject as OsuSlider).UpdateLength();
                    (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
                    OsuSlider[] arr = FindObjectsOfType<OsuSlider>();
                    foreach(var t in arr)
                    {
                        Destroy(t.gameObject);
                    }
                }
            }
        }
    }
}
