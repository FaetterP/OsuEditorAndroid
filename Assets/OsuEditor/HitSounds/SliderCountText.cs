using Assets.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.HitSounds
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
