using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class STSlider : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private Text STText;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { SaveValue(); });
        }

        void Start()
        {
            thisSlider.value = (float)Global.Map.Difficulty.SliderTickRate;
        }

        void SaveValue()
        {
            Global.Map.Difficulty.SliderTickRate = (int)thisSlider.value;
            STText.text = STText.GetComponent<LangWriter>().GetText() + " - " + thisSlider.value;
        }
    }
}
