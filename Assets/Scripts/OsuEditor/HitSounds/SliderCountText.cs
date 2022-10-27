using Assets.Scripts.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class SliderCountText : MonoBehaviour
    {
        Text count;

        void Awake()
        {
            count = GetComponent<Text>();
        }

        void OnEnable()
        {
            count.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();
        }
    }
}
