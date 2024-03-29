﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Music
{
    class PausePlayButton : MonoBehaviour
    {
        [SerializeField] private Sprite toPlay = null, toPause = null;
        [SerializeField] private AudioSource music;
        private Image thisImage = null;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void Start()
        {
            thisImage.sprite = toPlay;
        }

        void OnMouseDown()
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
