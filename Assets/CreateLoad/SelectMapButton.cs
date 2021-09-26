using Assets.CreateLoad.LoadExceptions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CreateLoad
{
    class SelectMapButton : MonoBehaviour
    {
        private string _mapName;
        private static Reader s_reader;

        void OnMouseDown()
        {
            if (s_reader == null) { s_reader = FindObjectOfType<Reader>(); }

            string path = Global.FullPathToMapFolder + _mapName;
            Global.FullPathToMap = path;

            try
            {
                s_reader.LoadMapFromFile(path);
            }
            catch (Exception e)
            {
                FindObjectOfType<LoadExceptionHandler>().Handle(e);
                return;
            }

            SceneManager.LoadScene(4);
        }

        public void SetText(string text)
        {
            _mapName = text;
        }
    }
}
