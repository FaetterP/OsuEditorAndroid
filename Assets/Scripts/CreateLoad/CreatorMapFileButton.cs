using Assets.Scripts.Utilities;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.CreateLoad
{
    class CreatorMapFileButton : MonoBehaviour
    {
        [SerializeField] private Text _difficulty, _creator, _source, _tags;

        private void OnMouseDown()
        {
            string[] pathArray = Global.FullPathToMapFolder.Split('/');
            string[] str = pathArray[pathArray.Length - 2].Split('-');
            string artist = str[0];
            string name = str[1];

            FillImageAndMusic();

            string fileName = Global.FullPathToMapFolder + "/" + artist + "-" + name + " (" + _creator.text + ") [" + _difficulty.text + "].osu";
            FileStream fileStream = File.Open(fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(EmptyMap.GetText(Global.Map.General.AudioFilename, name, name, artist, artist, _creator.text, _difficulty.text, _source.text, _tags.text, Global.Map.Events.BackgroundImage));
            sw.Close();

            SceneManager.LoadScene((int)Scenes.LoadMap);
        }

        private void FillImageAndMusic()
        {
            foreach (var t in new DirectoryInfo(Global.FullPathToMapFolder).GetFiles())
            {
                if (t.Name.EndsWith(".jpg"))
                    Global.Map.Events.BackgroundImage = t.Name;

                if (t.Name.EndsWith(".mp3"))
                    Global.Map.General.AudioFilename = t.Name;
            }
        }
    }
}
