using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Music
{
    [RequireComponent(typeof(Slider))]
    class MusicVolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
                         private Slider _thisSlider;
        
        void Awake()
        {
            _thisSlider = GetComponent<Slider>();
            _thisSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        }

        private void UpdateVolume()
        {
            _music.volume = _thisSlider.value;
        }
    }
}
