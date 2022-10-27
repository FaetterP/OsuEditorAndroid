using Assets.Scripts.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class Clap : MonoBehaviour
    {
        private Image thisImage;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void OnEnable()
        {
            if((Global.SelectedHitObject as OsuCircle).Clap)
            {
                thisImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                thisImage.color = new Color(1, 1, 1, 0.5f);
            }
        }
        void OnMouseDown()
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
