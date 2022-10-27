using System;

namespace Assets.Scripts.MapInfo
{
    class General
    {
        public string AudioFilename;
        public int AudioLeadIn;
        public int PreviewTime;
        public int Countdown; //TODO
        public string SampleSet; //TODO
        private double _stackLeniency; // 0-10
        private int _mode; // 0-3
        public int LetterboxInBreaks; //TODO
        public bool WidescreenStoryboard;

        public double StackLeniency
        {
            get
            {
                return _stackLeniency;
            }

            set
            {
                if (value < 0 || value > 10) { throw new ArgumentException(); }
                _stackLeniency = value;
            }
        }
        public int Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                if (value < 0 || value > 3) { throw new ArgumentException(); }
                _mode = value;
            }
        }
    }
}