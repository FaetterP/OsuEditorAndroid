using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints
{
    class RemoveTimingPointButton : MonoBehaviour
    {
        [SerializeField] private TimingPointElement _element;
        private LoaderTimingPoints _loader;

        private void Awake()
        {
            _loader = FindObjectOfType<LoaderTimingPoints>();
        }

        void OnMouseDown()
        {
            Global.Map.RemoveTimingPoint(_element.timingPoint);
            _loader.UpdateTimingPoints();
        }
    }
}
