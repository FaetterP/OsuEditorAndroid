using Assets.Scripts.OsuEditor.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class CreatorMusicLineMarks : MonoBehaviour
    {
        [SerializeField] private MusicLineMark toCreate;
        [SerializeField] private MusicLineMarkExpanded toCreateEx;

        public void UpdateMarks()
        {
            MusicLineMark[] l = FindObjectsOfType<MusicLineMark>();
            foreach (var t in l)
            {
                Destroy(t.gameObject);
            }
            MusicLineMarkExpanded[] ll = FindObjectsOfType<MusicLineMarkExpanded>();
            foreach (var t in ll)
            {
                Destroy(t.gameObject);
            }


            foreach (var t in Global.Map.TimingPoints)  //TimingPoints
            {
                MusicLineMark created = Instantiate(toCreate, transform);
                if (t.isParent) { created.Color = Color.red; }
                else { created.Color = Color.green; }
                created.timestamp = t.Offset;
                created.isUp = true;
            }

            foreach(int t in Global.Map.Editor.Bookmarks)   //Bookmarks
            {
                MusicLineMark created = Instantiate(toCreate, transform);
                created.timestamp = t;
                created.Color = Color.blue;
                created.isUp = false;
            }

            for(int i=0;i<Global.Map.TimingPoints.Count-1;i++)  //kiai
            {
                if (Global.Map.TimingPoints[i].Kiai)
                {
                    MusicLineMarkExpanded created = Instantiate(toCreateEx, transform);
                    created.timeLeft = Global.Map.TimingPoints[i].Offset;
                    created.timeRight = Global.Map.TimingPoints[i+1].Offset;
                    created.Color = new Color(255/255f, 151/255f, 15/255f, 0.5f);
                }
            }
        }
    }
}
