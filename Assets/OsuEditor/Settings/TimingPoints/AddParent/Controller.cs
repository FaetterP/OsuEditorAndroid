using Assets.Utilities;
using Assets.Utilities.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.TimingPoints.AddParent
{
    class Controller : MonoBehaviour
    {
        [SerializeField] private AudioSource Music;
        [SerializeField] private Slider MusicSlider;
        [SerializeField] private Text text;
        [SerializeField] private Metronome[] Metronome = new Metronome[4];
        public int time_start;
        private double sr_time;
        public int status = 0;

        private List<double> times = new List<double>();

        private LocalizedString[] messages = new LocalizedString[9];

        void Awake()
        {
            for(int i = 0; i < messages.Length; i++)
            {
                messages[i] = new LocalizedString("editor.timing.addParent.message." + i);
            }
        }

        public void Reset()
        {
            status = 0;
            MusicSlider.value = time_start;
            Music.Pause();
            times.Clear();
            text.text = messages[times.Count].GetValue();
        }

        public void AddTime(double time)
        {
            times.Add(time);
            text.text = messages[times.Count].GetValue();
            if (times.Count > 7) { status = 2; sr_time = 0; foreach (double t in times) { sr_time += t; } sr_time = sr_time / times.Count; }
        }

        public int GetCount()
        {
            return times.Count;
        }
        public void  SetStartTIme(double time)
        {
            time_start = (int)time;
        }


        private int curr0, curr1=-1;
        void Update()
        {
            curr0 = (int)((Global.MusicTime - time_start) / sr_time) % 4;
            while (curr0 < 0) { curr0 += 4; }

            if (times.Count >= 8 && curr0!=curr1)
            {
                curr1 = curr0;
                StartCoroutine(Metronome[curr0].Bell());
            }
        }

        public double GetSr()
        {
            return sr_time;
        }
    }
}
