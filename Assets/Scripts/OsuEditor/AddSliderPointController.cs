using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class AddSliderPointController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.touchCount == 2 && Global.SliderStatus == SliderStatus.Add)
            {
                Touch touch = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Ended)
                {
                    var pos = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                    pos = OsuMath.UnityCoordsToOsu(pos);
                    SliderPoint added = new SliderPoint((int)pos.x, (int)pos.y);
                    (Global.SelectedHitObject as OsuSlider).AddSliderPoint(added);
                    (Global.SelectedHitObject as OsuSlider).UpdatePrintedPoints();

                    OsuSliderDisplay[] arr = FindObjectsOfType<OsuSliderDisplay>();
                    foreach (var t in arr)
                    {
                        Destroy(t.gameObject);
                    }
                }
            }
        }
    }
}
