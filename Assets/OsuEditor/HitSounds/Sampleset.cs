using Assets.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.HitSounds
{
    class Sampleset : MonoBehaviour
    {
        Dropdown thisDropdown;

        void Awake()
        {
            thisDropdown = GetComponent<Dropdown>();
            thisDropdown.onValueChanged.AddListener(delegate { UpdateSampleset(); });
        }

        void OnEnable()
        {
            thisDropdown.value = (Global.SelectedHitObject as OsuCircle).Sampleset;
        }

        private void UpdateSampleset()
        {
            (Global.SelectedHitObject as OsuCircle).Sampleset = thisDropdown.value;
        }
    }
}
