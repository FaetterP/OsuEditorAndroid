using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints
{
    class SelectThisTimingPoint : MonoBehaviour
    {
        void OnMouseDown()
        {
            FindObjectOfType<SaveTimingPointButtonAndEditor>().SelectTimingPoint(transform.parent.GetComponent<TimingPointElement>().timingPoint);
        }
    }
}
