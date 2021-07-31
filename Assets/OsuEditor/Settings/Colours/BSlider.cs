using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class BSlider : MonoBehaviour
    {
        [SerializeField] private ColourHandler handler;
        private Slider thisSlider;
        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { UpdateB(); });
        }

        private void UpdateB()
        {
            var c = Global.Map.Colors[handler.GetNumber()];
            c.b = thisSlider.value / 255f;
            Global.Map.Colors[handler.GetNumber()] = c;
            handler.UpdateColour();
        }
    }
}
