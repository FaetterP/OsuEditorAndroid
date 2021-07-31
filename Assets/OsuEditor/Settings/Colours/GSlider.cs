using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class GSlider : MonoBehaviour
    {
        [SerializeField] private ColourHandler handler;
        private Slider thisSlider;
        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { UpdateG(); });
        }

        private void UpdateG()
        {
            var c = Global.Map.Colors[handler.GetNumber()];
            c.g = thisSlider.value / 255f;
            Global.Map.Colors[handler.GetNumber()] = c;
            handler.UpdateColour();
        }
    }
}
