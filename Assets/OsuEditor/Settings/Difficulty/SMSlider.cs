using Assets.Elements;
using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class SMSlider : MonoBehaviour
    {
        private Slider thisSlider;
        [SerializeField] private Text SMText;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.onValueChanged.AddListener(delegate { SaveValue(); });
        }

        void Start()
        {
            thisSlider.value = (float)Global.Map.Difficulty.SliderMultiplier;
        }

        void SaveValue()
        {
            thisSlider.value = (float)Math.Round(thisSlider.value, 1);
            Global.Map.Difficulty.SliderMultiplier = thisSlider.value;
            SMText.text = SMText.GetComponent<LangWriter>().GetText() + " - " + thisSlider.value;

            foreach(var t in Global.Map.OsuHitObjects)
            {
                if(t is OsuSlider)
                {
                    (t as OsuSlider).UpdateTimeEnd();
                }
            }
        }
    }
}
