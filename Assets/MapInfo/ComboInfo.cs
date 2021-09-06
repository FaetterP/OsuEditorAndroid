using UnityEngine;

namespace Assets.MapInfo
{
    struct ComboInfo
    {
        private int _number;
        private Color _color;

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
                return _color;
            }
        }

        public ComboInfo(int number, Color color)
        {
            _number = number;
            _color = color;
        }
    }
}
