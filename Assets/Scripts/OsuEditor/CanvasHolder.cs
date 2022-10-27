using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class CanvasHolder : MonoBehaviour
    {
        [SerializeField] private Canvas _circleCanvas;
        [SerializeField] private Canvas _sliderCanvas;

        public void SetActiveCircle(bool value)
        {
            _circleCanvas.gameObject.SetActive(value);
        }

        public void SetActiveSlider(bool value)
        {
            _sliderCanvas.gameObject.SetActive(value);
        }
    }
}
