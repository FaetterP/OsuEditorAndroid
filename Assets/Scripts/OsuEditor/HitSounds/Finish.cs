using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Image))]
    class Finish : MonoBehaviour
    {
        private Image _thisImage;

        private void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if ((Global.SelectedHitObject as OsuCircle).Finish)
            {
                _thisImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                _thisImage.color = new Color(1, 1, 1, 0.5f);
            }
        }

        private void OnMouseDown()
        {
            OsuCircle c = (Global.SelectedHitObject as OsuCircle);
            if (c.Finish)
            {
                c.Finish = false;
                _thisImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                c.Finish = true;
                _thisImage.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
