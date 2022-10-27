using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Text))]
    class SliderCountText : MonoBehaviour
    {
        private Text _textCount;

        private void Awake()
        {
            _textCount = GetComponent<Text>();
        }

        private void OnEnable()
        {
            _textCount.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();
        }
    }
}
