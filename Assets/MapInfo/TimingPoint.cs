using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MapInfo
{
    class TimingPoint : IComparable<TimingPoint>
    {
        public int Offset = 0;
        public double BeatLength = 1000;
        public int Meter=4;
        public int TimeSignature = 0; //
        public int SampleSet = 0; //
        public int SampleIndex = 0; //
        public int Volume = 100;
        public bool Kiai = false;

        public double Mult = 1;
        public bool isParent = false;
        //public double BPM = 100;

        public int CompareTo(TimingPoint other)
        {
            return Offset.CompareTo(other.Offset);
        }

        public override string ToString()
        {
            string ret = "";
            // 10000,333.33     ,4,    0,           0         ,100,    1          ,1
            // time,beatLength,meter,sampleSet,sampleIndex,volume,uninherited,effects
            ret += Offset + ",";
            if (isParent) { ret += BeatLength+","; }
            else { ret += "-" + 100 / Mult + ","; }
            ret += Meter + "," + SampleSet + "," + SampleIndex + "," + Volume + ",";
            ret += isParent ? 1 : 0;
            ret += ",";
            ret += Kiai ? 1 : 0;
            return ret;
        }
    }
}