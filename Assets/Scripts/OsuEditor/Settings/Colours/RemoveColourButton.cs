using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings.Colours
{
    class RemoveColourButton : MonoBehaviour
    {
        [SerializeField] ColourHandler handler;
        void OnMouseDown()
        {
            if (Global.Map.Colors.Count > 2)
            {
                Global.Map.Colors.RemoveAt(handler.GetNumber());
                handler.ChangeNumber(Global.Map.Colors.Count - 1);
            }
            UpdateStatus();
            Global.Map.UpdateComboInfos();
        }

        public void UpdateStatus()
        {
            Image thisImage = GetComponent<Image>();
            if (Global.Map.Colors.Count == 2)
            {
                var c = thisImage.color;
                thisImage.color = new Color(c.r, c.g, c.b, 0.5f);
            }
            else { thisImage.color = Color.white; }
        }
    }
}
