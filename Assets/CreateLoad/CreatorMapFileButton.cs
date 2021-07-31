using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

namespace Assets.CreateLoad
{
    class CreatorMapFileButton : MonoBehaviour
    {
        [SerializeField] private Text difficulty, creator, source, tags;
        void OnMouseDown()
        {
            if (difficulty.text == "")
            {
                difficulty.color = Color.red;
                return;
            }
            else { difficulty.color = Color.black; }
            if (creator.text == "")
            {
                creator.color = Color.red;
                return;
            }
            else { creator.color = Color.black; }
            if (source.text == "")
            {
                source.color = Color.red;
                return;
            }
            else { source.color = Color.black; }

            Debug.Log(Global.FullPathToMapFolder);
            string[] strarr = Global.FullPathToMapFolder.Split('/');
            string str = strarr[strarr.Length - 2];
            string artist = str.Split('-')[0];
            string  name = str.Split('-')[1];
            FileStream fileStream = File.Open(Global.FullPathToMapFolder + "/" + artist + "-" + name + " (" + creator.text + ") [" + difficulty.text + "].osu", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(EmptyMap.GetText(Global.Map.General.AudioFilename, Global.Map.Metadata.Title, Global.Map.Metadata.TitleUnicode, Global.Map.Metadata.Artist, Global.Map.Metadata.ArtistUnicode, creator.text, difficulty.text, source.text, tags.text, Global.Map.Events.BackgroungImage));
            sw.Close();

            SceneManager.LoadScene(3);
        }
    }
}
