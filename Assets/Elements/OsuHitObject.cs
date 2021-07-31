using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Elements
{
    abstract class OsuHitObject : MonoBehaviour, ICloneable, IComparable<OsuHitObject>
    {
        public int x = 0;
        public int y = 0;
        public int time = 0;

        public object Clone()
        {
            return MemberwiseClone();

        }
        public int CompareTo(OsuHitObject p)
        {
            return time.CompareTo(p.time);
        }
    }
}
