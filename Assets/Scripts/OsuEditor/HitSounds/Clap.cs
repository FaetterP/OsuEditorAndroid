using Assets.Scripts.MapInfo.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class Clap : MonoBehaviour
    {
        private Image thisImage;

        private void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if ((Global.SelectedHitObject as OsuCircle).Clap)
            {
                thisImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                thisImage.color = new Color(1, 1, 1, 0.5f);
            }
        }

        private void OnMouseDown()
        {
            OsuCircle c = (Global.SelectedHitObject as OsuCircle);
            if (c.Clap)
            {
                c.Clap = false;
                thisImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                c.Clap = true;
                thisImage.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
