using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class ODSlider : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private Text ODText;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { SaveValue(); });
        }

        void Start()
        {
            thisSlider.value = (float)Global.Map.Difficulty.CircleSize;
        }

        void SaveValue()
        {
            thisSlider.value = (float)Math.Round(thisSlider.value, 1);
            Global.Map.Difficulty.CircleSize = thisSlider.value;
            ODText.text = ODText.GetComponent<LangWriter>().GetText() + " - " + thisSlider.value;
        }
    }
}
