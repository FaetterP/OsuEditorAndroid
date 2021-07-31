using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class EnablerKiai : MonoBehaviour
    {
        [SerializeField] Image kiai;

        void Update()
        {
            for (int i = 0; i < Global.Map.TimingPoints.Count - 1; i++)
            {
                if (Global.MusicTime >= Global.Map.TimingPoints[i].Offset && Global.MusicTime <= Global.Map.TimingPoints[i + 1].Offset && Global.Map.TimingPoints[i].Kiai)
                {
                    kiai.color = new Color(1, 1, 1, 1);
                    return;
                }
                else
                {
                    kiai.color = new Color(1, 1, 1, 0);
                }
            }
        }
    }
}
