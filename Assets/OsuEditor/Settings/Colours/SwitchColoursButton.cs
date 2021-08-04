using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class SwitchColoursButton : MonoBehaviour
    {
        [SerializeField] ColourHandler handler;
        void OnMouseDown()
        {
            int newNumber = handler.GetNumber();
            newNumber++;
            newNumber %= Global.Map.Colors.Count;
            handler.SetNumber(newNumber);
        }
    }
}
