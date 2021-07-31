using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.CreateLoad;
using Assets.Music;
using System.Collections;
using UnityEngine.Networking;
using Assets.OsuEditor;
using Assets.OsuEditor.Timeline;

namespace Assets.Editor
{
    class Preproducer : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private AudioSource music;
        [SerializeField] private TimeLine line;
        [SerializeField] private CreatorMusicLineMarks creator;
        [SerializeField] private TimelineStepSlider timelineSlider;
        private WWW wwwBackground, wwwMusic;

        void Awake()
        {
            QualitySettings.SetQualityLevel(5, true);
            Reader.LoadMapFromFile(Global.FullPathToMap);

            wwwBackground = new WWW("file:///" + Global.FullPathToMapFolder + Global.Map.Events.BackgroungImage);
            //wwwMusic = new WWW("file:///" + Global.FullPathToMapFolder + Global.Map.General.AudioFilename);
        }
        void Start()
        {
            SetBackground();
            SetMusic();

            //  Destroy(gameObject);
        }


        private void SetBackground()
        {
            while (!wwwBackground.isDone) { }
            background.sprite = Sprite.Create(wwwBackground.texture, new Rect(0, 0, wwwBackground.texture.width, wwwBackground.texture.height), new Vector2(0, 0));
        }

        //private void SetMusic()
        //{
        //    while (!wwwMusic.isDone) {  }
        //    AudioClip clip = NAudioPlayer.FromMp3Data(wwwMusic.bytes);
        //    clip.name = "audio_from_file";
        //    music.clip = clip;
        //    line.UpdateMax((int)(clip.length*1000));
        //}

        public void SetMusic()
        {
           StartCoroutine(LoadSongCoroutine());
        }

        //IEnumerator LoadSongCoroutine()
        //{
        //    string url = "file:///" + Global.FullPathToMapFolder + Global.Map.General.AudioFilename;
        //    WWW www = new WWW(url);
        //    yield return www;

        //    music.clip = www.GetAudioClip(false, false);
        //    line.UpdateMax((int)(music.clip.length * 1000));
        //    yield break;
        //}
        IEnumerator LoadSongCoroutine()
        {
            string url = "file:///" + Global.FullPathToMapFolder + Global.Map.General.AudioFilename;
            WWW www = new WWW(url);
            yield return www;

            music.clip = www.GetAudioClip(false, false);
            Global.MusicLength = (int)(music.clip.length * 1000);
            line.UpdateMax((int)(music.clip.length * 1000));
            creator.UpdateMarks();
            timelineSlider.UpdateMarks();
            yield break;
        }
    }
}
