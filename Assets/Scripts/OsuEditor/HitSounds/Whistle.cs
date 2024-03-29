﻿using Assets.Scripts.MapInfo.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Image))]
    class Whistle : MonoBehaviour
    {
        private Image _thisImage;

        void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        void OnEnable()
        {
            if ((Global.SelectedHitObject as OsuCircle).Whistle)
            {
                _thisImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                _thisImage.color = new Color(1, 1, 1, 0.5f);
            }
        }
        void OnMouseDown()
        {
            OsuCircle c = (Global.SelectedHitObject as OsuCircle);
            if (c.Whistle)
            {
                c.Whistle = false;
                _thisImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                c.Whistle = true;
                _thisImage.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
