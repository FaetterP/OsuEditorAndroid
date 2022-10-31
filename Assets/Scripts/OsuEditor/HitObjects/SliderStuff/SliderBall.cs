using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitObjects.SliderStuff
{
    [RequireComponent(typeof(Image))]
    class SliderBall : MonoBehaviour
    {
        [SerializeField] private OsuSliderDisplay _slider;

        private void Start()
        {
            GetComponent<Image>().color = _slider.Slider.ComboColor;
        }

        private void Update()
        {
            Vector3 ballCoords = OsuMath.OsuCoordsToUnity(_slider.GetCurrentPoint());
            transform.localPosition = ballCoords - _slider.transform.localPosition;
        }
    }
}
