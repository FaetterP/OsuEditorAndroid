using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class SelectThisTimingPoint : MonoBehaviour
    {
        void OnMouseDown()
        {
            FindObjectOfType<SaveTimingPointButtonAndEditor>().SetTimingPoint(transform.parent.GetComponent<TimingPointElement>().timingPoint);
        }
    }
}
