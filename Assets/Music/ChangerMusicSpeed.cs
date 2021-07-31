using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    class ChangerMusicSpeed : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private AudioSource music;
        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { UpdateSpeed(); });
        }

        private void UpdateSpeed()
        {
            music.pitch = thisSlider.value;
        }
    }
}
