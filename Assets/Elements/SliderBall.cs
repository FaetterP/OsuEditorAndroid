using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    class SliderBall : MonoBehaviour
    {
        [SerializeField] private OsuSlider _slider;

        void Start()
        {
            GetComponent<Image>().color = _slider.ComboColor;
        }

        void Update()
        {
            Vector3 ballCoords = OsuMath.OsuCoordsToUnity(_slider.GetCurrentPoint());
            transform.localPosition = ballCoords - _slider.transform.localPosition;
        }
    }
}
