using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class AddColourButton : IClickable
    {
        [SerializeField] ColourHandler handler;
        public override void Click()
        {
            if (Global.Map.Colors.Count < 8)
            {
                Global.Map.Colors.Add(Color.white);
                handler.SetNumber(Global.Map.Colors.Count - 1);
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
