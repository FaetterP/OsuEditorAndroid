using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints.AddParent
{
    class ResetButton : MonoBehaviour
    {
        [SerializeField] Controller Controller;

        void OnMouseDown()
        {
            Controller.Reset();
        }
    }
}
