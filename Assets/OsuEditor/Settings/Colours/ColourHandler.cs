using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Colours
{
    class ColourHandler : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Slider r, g, b;
                         private Image image;
                         private int number = 0;


        void Awake()
        {
            image = GetComponent<Image>();
            r.onValueChanged.AddListener(delegate { UpdateColourR(); });
            g.onValueChanged.AddListener(delegate { UpdateColourG(); });
            b.onValueChanged.AddListener(delegate { UpdateColourB(); });
        }

        void Start()
        {
            ChangeNumber(0);
        }

        private void UpdateColourR()
        {
            var c = Global.Map.Colors[number];
            c.r = r.value / 255f;
            Global.Map.Colors[number] = c;
            image.color = Global.Map.Colors[number];
        }

        private void UpdateColourG()
        {
            var c = Global.Map.Colors[number];
            c.g = g.value / 255f;
            Global.Map.Colors[number] = c;
            image.color = Global.Map.Colors[number];
        }

        private void UpdateColourB()
        {
            var c = Global.Map.Colors[number];
            c.b = b.value / 255f;
            Global.Map.Colors[number] = c;
            image.color = Global.Map.Colors[number];
        }

        public void ChangeNumber(int num)
        {
            if (num < 0 || num > Global.Map.Colors.Count) { throw new ArgumentException(); }
            number = num;
            UpdateColourNumber();
            UpdateSliders();
        }

        public void UpdateColourNumber()
        {
            text.text = (number + 1).ToString();
            image.color = Global.Map.Colors[number];
        }

        public int GetNumber()
        {
            return number;
        }

        public void UpdateSliders()
        {
            r.value = Global.Map.Colors[number].r * 255;
            g.value = Global.Map.Colors[number].g * 255;
            b.value = Global.Map.Colors[number].b * 255;
        }
    }
}
