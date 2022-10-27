using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings.Colours
{
    class SwitchColoursButton : MonoBehaviour
    {
        [SerializeField] ColourHandler handler;
        void OnMouseDown()
        {
            int newNumber = handler.GetNumber();
            newNumber++;
            newNumber %= Global.Map.Colors.Count;
            handler.ChangeNumber(newNumber);
        }
    }
}
