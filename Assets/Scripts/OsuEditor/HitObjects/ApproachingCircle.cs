using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitObjects
{
    class ApproachingCircle : MonoBehaviour
    {
        [SerializeField] private OsuHitObjectDisplay _thisCircle;
        private RectTransform rectTransform;
        private Image thisImage;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            thisImage = GetComponent<Image>();
        }

        private void Start()
        {
            UpdateColor();
        }

        private void OnEnable()
        {
            //UpdateColor();
        }

        private void Update()
        {
            int razn = _thisCircle.Time - Global.MusicTime;
            if (razn <= Global.AR_ms && razn >= 0)
            {
                float size = 100 + (200f * razn / Global.AR_ms);
                var color = thisImage.color;
                thisImage.color = new Color(color.r, color.g, color.b, 0.1f + 0.9f * ((Global.MusicTime - _thisCircle.Time + Global.AR_ms * 1.0f) / (Global.AR_ms)));
                rectTransform.sizeDelta = new Vector2(size, size);
            }
            else { Destroy(gameObject); }
        }

        public void UpdateColor()
        {
            GetComponent<Image>().color = _thisCircle.ComboColor;
        }
    }
}
