using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.CreateLoad
{
    class CheckMusicAndGoBackButton : IClickable
    {
        [SerializeField] private Text Err;
        public override void Click()
        {
            if (isContainsFiles())
            {
                GetMusicAndBackground();
                //Global.FullPathToMap = Global.FullPathToMapFolder + "/" + Global.Map.Metadata.ArtistUnicode + " - " + Global.Map.Metadata.TitleUnicode + " (" + Global.Map.Metadata.Creator + ") [" + Global.Map.Metadata.Version + "].osu";
                //StreamWriter sw = new StreamWriter(Global.FullPathToMap);
                //sw.Write(EmptyMap.GetText(Global.Map.General.AudioFilename,Global.Map.Metadata.Title, Global.Map.Metadata.TitleUnicode, Global.Map.Metadata.Artist, Global.Map.Metadata.ArtistUnicode, Global.Map.Metadata.Creator, Global.Map.Metadata.Version, Global.Map.Metadata.Source, Global.Map.Metadata.Tags, Global.Map.Events.BackgroungImage));
                //sw.Close();
                SceneManager.LoadScene(2);
            }
            else
            {
                Err.gameObject.SetActive(true);
            }
        }

        private bool isContainsFiles()
        {
            return new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.mp3").Any(x => x.Extension == ".mp3") && new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.jpg").Any(x => x.Extension == ".jpg");
        }

        private void GetMusicAndBackground()
        {
            var files = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles();
            foreach (var t in files)
            {
                if (t.Name.EndsWith(".mp3")) { Global.Map.General.AudioFilename = t.Name; }
                if (t.Name.EndsWith(".jpg")) { Global.Map.Events.BackgroungImage = t.Name; }
            }
        }
    }
}
