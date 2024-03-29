﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings.Colours
{
    class ColourSliderRed : MonoBehaviour
    {
        [SerializeField] ColourHandler handler;
        private Slider thisSlider;
        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { UpdateRed(); });
        }

        private void UpdateRed()
        {
            var c = Global.Map.Colors[handler.GetNumber()];
            c.r = thisSlider.value / 255f;
            Global.Map.Colors[handler.GetNumber()] = c;
            handler.ChangeNumber(handler.GetNumber());
        }
    }
}
