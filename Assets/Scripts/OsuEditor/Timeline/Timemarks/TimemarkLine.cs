using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    class TimemarkLine : Timemark
    {
        public readonly Color Color;
        public readonly int Height;

        private static TimemarkLineGO s_TimemarkLine;

        public TimemarkLine(int time, Color color, int heigth) : base(time)
        {
            Color = color;
            Height = heigth;
        }

        public override void SpawnGameObject()
        {
            if (s_TimemarkLine == null)
                s_TimemarkLine = Resources.Load<TimemarkLineGO>("TimemarkLine");


            GameObject.Instantiate(s_TimemarkLine, GameObject.Find("TimeMarksLine").transform).Init(this);
        }
    }
}
