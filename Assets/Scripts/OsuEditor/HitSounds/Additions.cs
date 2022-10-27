using Assets.Scripts.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class Additions : MonoBehaviour
    {
        Dropdown thisDropdown;

        void Awake()
        {
            thisDropdown = GetComponent<Dropdown>();
            thisDropdown.onValueChanged.AddListener(delegate { UpdateAdditions(); });
        }

        void OnEnable()
        {
            thisDropdown.value = (Global.SelectedHitObject as OsuCircle).Additions;
        }

        private void UpdateAdditions()
        {
            (Global.SelectedHitObject as OsuCircle).Additions = thisDropdown.value;
        }
    }
}
