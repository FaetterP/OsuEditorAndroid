using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    [RequireComponent(typeof(Image))]
    class Background : MonoBehaviour
    {
        private Image _thisImage;

        private void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        public void SetSprite(Sprite sprite)
        {
            _thisImage.sprite = sprite;
        }

        public Sprite GetSprite()
        {
            return _thisImage.sprite;
        }
    }
}
