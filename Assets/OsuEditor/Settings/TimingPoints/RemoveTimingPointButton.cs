using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class RemoveTimingPointButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.TimingPoints.Remove(transform.parent.GetComponent<TimingPointElement>().timingPoint);
            transform.parent.parent.GetComponent<LoaderTimingPoints>().UpdateTimingPoints();
        }
    }
}
