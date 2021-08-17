using System;

namespace Assets.MapInfo
{
    class Difficulty
    {
        private double _hpDrainRate; // 0-10
        private double _circleSize; // 2-7
        private double _overallDifficulty; // 0-10
        private double _approachRate; // 0-10
        private double _sliderMultiplier; // 0.4-3.6
        private int _sliderTickRate; // 1-4

        public double HPDrainRate
        {
            get
            {
                return _hpDrainRate;
            }

            set
            {
                if (value < 0 || value > 10) { throw new ArgumentException(); }
                _hpDrainRate = value;
            }
        } 
        public double CircleSize
        {
            get
            {
                return _circleSize;
            }

            set
            {
                if (value < 2 || value > 7) { throw new ArgumentException(); }
                _circleSize = value;
            }
        }
        public double OverallDifficulty
        {
            get
            {
                return _overallDifficulty;
            }

            set
            {
                if (value < 0 || value > 10) { throw new ArgumentException(); }
                _overallDifficulty = value;
            }
        }
        public double ApproachRate
        {
            get
            {
                return _approachRate;
            }

            set
            {
                if (value < 0 || value > 10) { throw new ArgumentException(); }
                _approachRate = value;
            }
        }
        public double SliderMultiplier
        {
            get
            {
                return _sliderMultiplier;
            }

            set
            {
                if (value < 0.4 || value > 3.6) { throw new ArgumentException(); }
                _sliderMultiplier = value;
            }
        }
        public int SliderTickRate
        {
            get
            {
                return _sliderTickRate;
            }

            set
            {
                if (value < 1 || value > 4) { throw new ArgumentException(); }
                _sliderTickRate = value;
            }
        }
    }
}