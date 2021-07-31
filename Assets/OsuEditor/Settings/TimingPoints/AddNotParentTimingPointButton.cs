using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.MapInfo;
using Assets.Utilities;
using UnityEngine;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class AddNotParentTimingPointButton : IClickable
    {
        [SerializeField] private LoaderTimingPoints loader;
        [SerializeField] private CreatorMusicLineMarks creator;
        public override void Click()
        {
            TimingPoint added = new TimingPoint();
            TimingPoint nearest = OsuMath.GetNearestTimingPointLeft(Global.MusicTime, true);
            added.BeatLength = nearest.BeatLength;
            //added.BPM = nearest.BPM;
            added.isParent = false;
            added.Kiai = false;
            added.Meter = 4;
            added.Offset = Global.MusicTime;
            added.Volume = 100;
            Global.Map.TimingPoints.Add(added);

            loader.UpdateTimingPoints();
            creator.UpdateMarks();
        }
    }
}
