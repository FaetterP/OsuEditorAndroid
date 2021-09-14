using Assets.MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Timeline
{
    class TimelineStepSlider : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks creatorTimemarks;
        [SerializeField] private Timemark timemark;
        [SerializeField] private Text step;
                         private Slider thisSlider;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.value = Global.Map.Editor.BeatDivisor;
            thisSlider.onValueChanged.AddListener(delegate { UpdateMarks(); });

        }

        public void UpdateMarks()
        {
            creatorTimemarks.UpdateTimemarks((int)thisSlider.value);
            step.text = "1/" + thisSlider.value;
        }
    }
}