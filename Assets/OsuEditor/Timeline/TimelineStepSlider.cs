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
        private Slider thisSlider;
        [SerializeField] Timemark timemark;
        [SerializeField] Text step;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
            thisSlider.value = Global.Map.Editor.BeatDivisor;
            thisSlider.onValueChanged.AddListener(delegate { UpdateMarks(); });

        }

        public void UpdateMarks()
        {
            Timemark[] marks = FindObjectsOfType<Timemark>();
            foreach(var t in marks)
            {
                CreatorTimemarks.RemoveMarkFromScreen(t.time);
                Destroy(t.gameObject);
            }

            CreatorTimemarks.MarksToCreate.Clear();
            AddMainStepMarks();
            switch ((int)thisSlider.value)
            {
                case 2:
                    //Devide2(Color.red);
                    Devide(2, Color.red);
                    break;
                case 3:
                    //Devide3(Color.magenta);
                    Devide(3, Color.magenta);
                    break;
                case 4:
                    //Devide2(Color.red);
                    //Devide2(Color.blue);
                    Devide(2, Color.red);
                    Devide(2, Color.blue);
                    break;
                case 5:
                    //Devide5(Color.yellow);
                    Devide(5, Color.yellow);
                    break;
                case 6:
                    //Devide2(Color.red);
                    //Devide3(Color.magenta);
                    Devide(2, Color.red);
                    Devide(3, Color.magenta);
                    break;
                case 7:
                    //Devide7(Color.yellow);
                    Devide(7, Color.yellow);
                    break;
                case 8:
                    //Devide2(Color.red);
                    //Devide2(Color.blue);
                    // Devide2(Color.magenta);
                    Devide(2, Color.red);
                    Devide(2, Color.blue);
                    Devide(2, Color.magenta);
                    break;
            }

            step.text = "1/" + thisSlider.value;
        }

        private void AddMainStepMarks()
        {
            List<TimingPoint> parents = new List<TimingPoint>();
            foreach(var t in Global.Map.TimingPoints)
            {
                Timemark added = (Timemark)timemark.Clone();
                added.time = t.Offset;
                added.height = 50;
                added.color = Color.cyan;
                CreatorTimemarks.MarksToCreate.Add(added);
                if (t.isParent) { parents.Add(t); }
            }
            double time;
            for (int i=0;i<parents.Count-1;i++)
            {
                time = parents[i].Offset;
                while (time < parents[i + 1].Offset)
                {
                    Timemark added = (Timemark)timemark.Clone();
                    added.time = (int)time;
                    added.height = 50;
                    added.color = Color.white;
                    CreatorTimemarks.MarksToCreate.Add(added);
                    time += parents[i].BeatLength;
                }
            }
            time = parents[parents.Count - 1].Offset;
            while (time < Global.MusicLength)
            {
                Timemark added = (Timemark)timemark.Clone();
                added.time = (int)time;
                added.height = 50;
                added.color = Color.white;
                CreatorTimemarks.MarksToCreate.Add(added);
                time += parents[parents.Count-1].BeatLength;
            }
        }
        private void Devide(ushort num, Color color)
        {
            List<Timemark> toadd = new List<Timemark>();
            for (int i = 0; i < CreatorTimemarks.MarksToCreate.Count - 1; i++)
            {
                for(int i0 = 1; i0 < num; i0++)
                {
                    double time = CreatorTimemarks.MarksToCreate[i + 1].time - CreatorTimemarks.MarksToCreate[i].time;
                    Timemark added = (Timemark)timemark.Clone();
                    added.time = (int)(CreatorTimemarks.MarksToCreate[i].time + time * i0 / num);
                    added.height = 30;
                    added.color = color;
                    toadd.Add(added);
                }
            }
            CreatorTimemarks.MarksToCreate.AddRange(toadd);
            CreatorTimemarks.MarksToCreate.Sort();
        }
    }
}