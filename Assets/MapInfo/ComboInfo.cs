using UnityEngine;

namespace Assets.MapInfo
{
    struct ComboInfo
    {
        private int _number;
        private int _ColorIndex;

        public int Number
        {
            get
            {
                return _number;
            }
        }

        public Color Color
        {
            get
            {
                return Global.Map.Colors[_ColorIndex];
            }
        }

        public ComboInfo(int number, int colorIndex)
        {
            _number = number;
            _ColorIndex = colorIndex;
        }
    }
}
