﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class AddColourButton : MonoBehaviour
    {
        [SerializeField] ColourHandler handler;
        void OnMouseDown()
        {
            if (Global.Map.Colors.Count < 8)
            {
                Global.Map.Colors.Add(Color.white);
                handler.ChangeNumber(Global.Map.Colors.Count - 1);
            }
            UpdateStatus();
            Global.Map.UpdateComboColours();
            Global.Map.UpdateNumbers();
        }

        public void UpdateStatus()
        {
            Image thisImage = GetComponent<Image>();
            if (Global.Map.Colors.Count == 8)
            {
                var c = thisImage.color;
                thisImage.color = new Color(c.r, c.g, c.b, 0.5f);
            }
            else { thisImage.color = Color.white; }
        }
    }
}