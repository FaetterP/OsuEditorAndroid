using UnityEngine;

namespace Assets.Scripts.MapInfo
{
    struct ComboInfo
    {
        private int _number;
        private int _colorIndex;

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
                return Global.Map.Colors[_colorIndex];
            }
        }

        public ComboInfo(int number, int colorIndex)
        {
            _number = number;
            _colorIndex = colorIndex;
        }
    }
}
