using Assets.Scripts.MapInfo;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints
{
    class AddNotParentTimingPointButton : MonoBehaviour
    {
        [SerializeField] private LoaderTimingPoints loader;
        [SerializeField] private CreatorMusicLineMarks creator;
        void OnMouseDown()
        {
            TimingPoint added = new TimingPoint();
            TimingPoint nearest = Global.Map.GetNearestTimingPointLeft(Global.MusicTime, true);
            added.BeatLength = nearest.BeatLength;
            //added.BPM = nearest.BPM;
            added.isParent = false;
            added.Kiai = false;
            added.Meter = 4;
            added.Offset = Global.MusicTime;
            added.Volume = 100;
            Global.Map.AddTimingPoint(added);

            loader.UpdateTimingPoints();
            creator.UpdateMarks();
        }
    }
}
