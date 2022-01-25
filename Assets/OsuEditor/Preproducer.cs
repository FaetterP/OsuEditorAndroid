﻿using Assets.Music;
using Assets.OsuEditor;
using Assets.OsuEditor.Timeline;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor
{
    class Preproducer : MonoBehaviour
    {
        [SerializeField] private Background _background;
        [SerializeField] private AudioSource _music;
        [SerializeField] private TimeLine _line;
        [SerializeField] private CreatorMusicLineMarks _creator;
        [SerializeField] private TimelineStepSlider _timelineSlider;
                         private WWW _wwwBackground;

        void Awake()
        {
            _wwwBackground = new WWW("file:///" + Global.FullPathToMapFolder + Global.Map.Events.BackgroungImage);
        }

        void Start()
        {
            SetBackground();
            SetMusic();
        }

        private void SetBackground()
        {
            Rect textureRect = new Rect(0, 0, _wwwBackground.texture.width, _wwwBackground.texture.height);
            Sprite sprite = Sprite.Create(_wwwBackground.texture, textureRect, Vector2.zero);
            _background.SetSprite(sprite);
        }

        public void SetMusic()
        {
            StartCoroutine(LoadSongCoroutine());
        }

        IEnumerator LoadSongCoroutine()
        {
            string url = "file:///" + Global.FullPathToMapFolder + Global.Map.General.AudioFilename;
            WWW wwwMusic = new WWW(url);
            yield return wwwMusic;

            _music.clip = wwwMusic.GetAudioClip(false, false);
            Global.MusicLength = (int)(_music.clip.length * 1000);
            _line.UpdateMax((int)(_music.clip.length * 1000));
            _creator.UpdateMarks();
            _timelineSlider.UpdateMarks();
            yield break;
        }
    }
}
