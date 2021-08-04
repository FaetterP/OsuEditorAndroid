using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.MapInfo;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.TimingPoints.AddParent
{
    class DoneButton : MonoBehaviour
    {
        [SerializeField] private Controller Controller;
        [SerializeField] private Canvas Enable, Disable;

        void OnMouseDown()
        {
            if (Controller.status != 2) { return; }

            TimingPoint added = new TimingPoint();
            added.isParent = true;
            //added.BPM = 60000f / Controller.GetSr();
            added.BeatLength = Controller.GetSr();
            added.Kiai = false;
            added.Meter = 4;
            added.Mult = 1;
            added.Offset = Controller.time_start;
            added.Volume = 100;
            Global.Map.TimingPoints.Add(added);

            Enable.gameObject.SetActive(true);
            Disable.gameObject.SetActive(false);
        }
    }
}
