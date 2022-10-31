using Assets.Scripts.OsuEditor.Timeline.Timemarks;
using UnityEngine;
using System;

namespace Assets.Scripts.MapInfo.HitObjects
{
    abstract class OsuHitObject : IComparable<OsuHitObject>
    {
        protected Color _comboColor;
        protected int _comboNumber;

        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract int Time { get; }
        public Color ComboColor => _comboColor;
        public int ComboNumber => _comboNumber;

        public abstract int UpdateComboColor(Color[] colors, int lastColorIndex, int lastNumber);
        public abstract void SpawnHitObject();
        public abstract TimemarkHitObject[] GetTimemark();
        public abstract bool IsRightTime();

        public int CompareTo(OsuHitObject other)
        {
            return Time.CompareTo(other.Time);
        }
    }
}
