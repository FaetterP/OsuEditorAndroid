using UnityEngine;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class RemoveTimingPointButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.RemoveTimingPoint(transform.parent.GetComponent<TimingPointElement>().timingPoint);
            transform.parent.parent.GetComponent<LoaderTimingPoints>().UpdateTimingPoints();
        }
    }
}
