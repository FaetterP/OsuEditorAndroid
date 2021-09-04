using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    class ApproachingCircle : MonoBehaviour
    {
        [SerializeField]private OsuCircle thisCircle;
        private RectTransform rectTransform;
        private Image thisImage;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            thisImage = GetComponent<Image>();
        }

        void Start()
        {
            UpdateColor();
        }
        void  OnEnable()
        {
            UpdateColor();
        }

        void Update()
        {
            int razn = thisCircle.Time - Global.MusicTime;
            if (razn <= Global.AR_ms && razn >= 0)
            {
                float size = 100+(200f*razn/Global.AR_ms);
                var color = thisImage.color;
                thisImage.color = new Color(color.r, color.g, color.b, 0.1f + 0.9f * ((Global.MusicTime - thisCircle.Time + Global.AR_ms * 1.0f) / (Global.AR_ms)));
                rectTransform.sizeDelta = new Vector2(size,size);
            }
            else { Destroy(gameObject); }
        }

        public void UpdateColor()
        {
            GetComponent<Image>().color = thisCircle.ComboColor;
        }
    }
}
