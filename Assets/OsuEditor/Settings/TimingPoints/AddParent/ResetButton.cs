using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.OsuEditor.Settings.TimingPoints.AddParent
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
