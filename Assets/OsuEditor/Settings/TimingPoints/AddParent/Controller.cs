using Assets.Utilities;
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
        private string[] messages_ru = new string[9] { "Нажимайте в такт песне!", "Нажимайте в такт песне!", "Нажимайте в такт песне!", "Продолжайте нажимать.", "Продолжайте нажимать.", "Ещё 3 раза...", "Ещё 2 раза...", "Ещё 1 раз...", "Готово. Если метроном звучит неправльно, нажмите 'Сброс', чтобы начать сначала." };
        private string[] messages_en = new string[9] { "Tap to the beat of the song!", "Press to the beat of the song!", "Press to the beat of the song!", "Keep tapping.", "Keep tapping.", "3 more times...", "2 more times...", " 1 more time...", "Done. If the metronome sounds wrong, click 'Reset' to start over." };
        private Dictionary<Lang, string[]> dc = new Dictionary<Lang, string[]>();

        void Awake()
        {
            dc.Add(Lang.EN, messages_en);
            dc.Add(Lang.RU, messages_ru);
        }

        public void Reset()
        {
            status = 0;
            MusicSlider.value = time_start;
            Music.Pause();
            times.Clear();
            text.text = dc[Global.Lang][times.Count];
        }

        public void AddTime(double time)
        {
            times.Add(time);
            text.text = dc[Global.Lang][times.Count];
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
