using Assets.Scripts.MapInfo.HitObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkHitObject : Timemark
    {
        public readonly OsuHitObject HitObject;

        private readonly string _type;

        private static Dictionary<string, TimemarkHitObjectGO> dc = new Dictionary<string, TimemarkHitObjectGO>();

        public TimemarkHitObject(int time, OsuHitObject hitObject, string type) : base(time)
        {
            HitObject = hitObject;
            _type = type;
        }

        public override void SpawnGameObject()
        {
            string objectType = "";
            if (HitObject is OsuCircle)
                objectType = "Circle";
            else if (HitObject is OsuSpinner)
                objectType = "Spinner";
            else if (HitObject is OsuSlider)
                objectType = "Slider";

            string fullType = $"{objectType}{_type}";
            if (dc.ContainsKey(fullType) == false)
                dc.Add(fullType, Resources.Load<TimemarkHitObjectGO>($"Timemark{fullType}"));

            GameObject.Instantiate(dc[fullType], GameObject.Find("TimeMarksLine").transform).Init(this);
        }
    }
}
