using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class ColourHandler : MonoBehaviour
    {
        [SerializeField] private Text text;
        private Image image;
        private int number=0;
        [SerializeField] Slider r, g, b;

        void Awake()
        {
            image = GetComponent<Image>();
        }
        void Start()
        {
            SetNumber(0);
        }
        public void UpdateColour()
        {
            text.text = (number + 1).ToString();
            image.color = Global.Map.Colors[number];
          //  Debug.Log(image.color);

        }

        public void SetNumber(int num)
        {
            if (num < 0 || num > Global.Map.Colors.Count) { throw new ArgumentException(); }
            number = num;
            UpdateColour();
            UpdateSliders();
        }

        public int GetNumber()
        {
            return number;
        }

        public void UpdateSliders()
        {
            r.value = image.color.r * 255;
            g.value = image.color.g * 255;
            b.value = image.color.b * 255;
        }
    }
}
