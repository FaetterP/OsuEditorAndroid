using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace Assets.CreateLoad
{
    class SelectMapButton : MonoBehaviour
    {
        private string MapName;
        void OnMouseDown()
        {
            Global.FullPathToMap = Global.FullPathToMapFolder + MapName;
            SceneManager.LoadScene(4);
        }

        public void SetText(string text)
        {
            MapName = text;
        }
    }
}
