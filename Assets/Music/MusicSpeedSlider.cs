using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    [RequireComponent(typeof(Slider))]
    class MusicSpeedSlider : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
                         private Slider _thisSlider;

        void Awake()
        {
            _thisSlider = GetComponent<Slider>();
            _thisSlider.onValueChanged.AddListener(delegate { UpdateSpeed(); });
        }

        private void UpdateSpeed()
        {
            _music.pitch = _thisSlider.value;
        }
    }
}
