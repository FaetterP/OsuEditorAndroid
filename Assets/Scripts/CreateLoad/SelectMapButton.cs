using Assets.Scripts.CreateLoad.LoadExceptions;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.CreateLoad
{
    class SelectMapButton : MonoBehaviour
    {
        private string _mapName;

        private void OnMouseDown()
        {
            Reader reader = new Reader();

            string path = Global.FullPathToMapFolder + _mapName;
            Global.FullPathToMap = path;

            try
            {
                Global.Map = reader.LoadMapFromFile(path);
            }
            catch (Exception e)
            {
                FindObjectOfType<LoadExceptionHandler>().Handle(e);
                return;
            }

            SceneManager.LoadScene((int)Scenes.EditorClassic);
        }

        public void SetText(string text)
        {
            _mapName = text;
        }
    }
}
