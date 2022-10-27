using System;

namespace Assets.Scripts.MapInfo
{
    class TimingPoint : IComparable<TimingPoint>
    {
        public int Offset;
        public double BeatLength;
        public int Meter;
        public int TimeSignature;
        public int SampleSet;
        public int SampleIndex;
        public int Volume;
        public bool Kiai;

        public double Mult = 1;
        public bool isParent = false;

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