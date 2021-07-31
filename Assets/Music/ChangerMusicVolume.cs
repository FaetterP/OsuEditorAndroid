using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    class ChangerMusicVolume : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private AudioSource music;
        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
        }

        private void UpdateVolume()
        {
            music.volume = thisSlider.value;
        }
    }
}
