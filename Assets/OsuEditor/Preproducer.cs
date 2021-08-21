using UnityEngine;
using UnityEngine.UI;
using Assets.CreateLoad;
using Assets.Music;
using System.Collections;
using Assets.OsuEditor;
using Assets.OsuEditor.Timeline;

namespace Assets.Editor
{
    class Preproducer : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private AudioSource _music;
        [SerializeField] private TimeLine _line;
        [SerializeField] private CreatorMusicLineMarks _creator;
        [SerializeField] private TimelineStepSlider _timelineSlider;
                         private WWW _wwwBackground;

        void Awake()
        {
            Reader.LoadMapFromFile(Global.FullPathToMap);

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
            _background.sprite = Sprite.Create(_wwwBackground.texture, textureRect , Vector2.zero);
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
