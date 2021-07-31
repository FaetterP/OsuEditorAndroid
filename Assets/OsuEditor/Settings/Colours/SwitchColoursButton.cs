using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class SwitchColoursButton : IClickable
    {
        [SerializeField] ColourHandler handler;
        public override void Click()
        {
            int newNumber = handler.GetNumber();
            newNumber++;
            newNumber %= Global.Map.Colors.Count;
            handler.SetNumber(newNumber);
        }
    }
}
