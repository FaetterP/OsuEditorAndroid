using Assets.Scripts.MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints
{
    class TimingPointElement : MonoBehaviour
    {
        public TimingPoint timingPoint;

        [SerializeField] private Image isParent;
        [SerializeField] private Text Offset;
        [SerializeField] private Text BPM;
        [SerializeField] private Text Meter;
        [SerializeField] private Text Volume;
        [SerializeField] private Image isKiai;
        [SerializeField] private Sprite kiai, not_kiai;

        void Start()
        {
            if (timingPoint.isParent) 
            {
                //isParent.color = Color.red;
                isParent.color = new Color(196f / 255, 42f / 255, 6f / 255);
                BPM.text = (60000f/timingPoint.BeatLength).ToString();
            }
            else 
            {
                //isParent.color = Color.green;
                isParent.color = new Color(91f / 255, 209f / 255, 11f / 255);
                BPM.text = "x" + timingPoint.Mult;
            }
            Offset.text = OsuMath.ConvertTimestampToSring(timingPoint.Offset);
            Meter.text = timingPoint.Meter + "/4";
            Volume.text = timingPoint.Volume.ToString();
            if (timingPoint.Kiai) { isKiai.sprite = kiai; }
            else { isKiai.sprite = not_kiai; }
        }
    }
}
