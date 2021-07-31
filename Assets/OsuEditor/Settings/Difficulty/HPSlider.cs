using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class HPSlider : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private Text HPText;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener( delegate { SaveValue(); });
        }

        void Start()
        {
            thisSlider.value = (float)Global.Map.Difficulty.HPDrainRate;
        }

        void SaveValue()
        {
            thisSlider.value = (float)Math.Round(thisSlider.value, 1);
            Global.Map.Difficulty.HPDrainRate = thisSlider.value;
            HPText.text = HPText.GetComponent<LangWriter>().GetText() +" - " + thisSlider.value;
        }
    }
}
