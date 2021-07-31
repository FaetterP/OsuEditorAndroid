using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    class PausePlayButton : IClickable
    {
        [SerializeField]private Sprite toPlay = null, toPause = null;
        [SerializeField]private AudioSource music;
        private Image thisImage = null;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }
        void Start()
        {
            thisImage.sprite = toPlay;
        }

        public override void Click()
        {
            if (music.isPlaying)
            {
                thisImage.sprite = toPlay;
                music.Pause();
            }
            else
            {
                thisImage.sprite = toPause;
                music.Play();
            }
        }
    }
}
