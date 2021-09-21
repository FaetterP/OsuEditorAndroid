using Assets.OsuEditor;
using Assets.OsuEditor.Timeline;
using System;
using UnityEngine;

namespace Assets.Elements
{
    abstract class OsuHitObject : MonoBehaviour, IComparable<OsuHitObject>
    {
        private int _x;
        private int _y;
        public int Time;
        public int X
        {
            get
            {
                return _x;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
        }

        public void SetCoords(Vector2 pos)
        {
            if (pos.x < 0) { pos.x = 0; }
            if (pos.y < 0) { pos.y = 0; }
            if (pos.x > 512) { pos.x = 512; }
            if (pos.y > 384) { pos.y = 384; }
            _x = (int)pos.x;
            _y = (int)pos.y;
        }

        public void SetCoords(int x, int y)
        {
            if (x < 0) { x = 0; }
            if (y < 0) { y = 0; }
            if (x > 512) { x = 512; }
            if (y > 384) { y = 384; }
            _x = x;
            _y = y;
        }

        public int CompareTo(OsuHitObject p)
        {
            return Time.CompareTo(p.Time);
        }

        void OnDestroy()
        {
            CreatorHitObjects.RemoveObjectFromList(Time);
        }

        public abstract bool IsRightTime();

        public abstract void Init(OsuHitObject obj);

        public abstract CircleTimemark[] GetTimemark();
    }
}
