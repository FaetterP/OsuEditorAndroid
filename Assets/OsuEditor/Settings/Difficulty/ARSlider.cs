using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class ARSlider : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private Text ARText;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { SaveValue(); });
        }

        void Start()
        {
            thisSlider.value = (float)Global.Map.Difficulty.ApproachRate;
        }

        void SaveValue()
        {
            thisSlider.value = (float)Math.Round(thisSlider.value, 1);
            Global.Map.Difficulty.ApproachRate = thisSlider.value;
            ARText.text = ARText.GetComponent<LangWriter>().GetText() + " - " + thisSlider.value;

            double ar = Global.Map.Difficulty.ApproachRate;
            if (ar == 5) { Global.AR_ms = 1200; }
            if (ar < 5) { Global.AR_ms = (int)(1200 + 600 * (5 - ar) / 5); }
            if (ar > 5) { Global.AR_ms = (int)(1200 - 750 * (ar - 5) / 5); }
        }
    }
}
