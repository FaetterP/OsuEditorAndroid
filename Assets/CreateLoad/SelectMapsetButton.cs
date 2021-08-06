using System.IO;
using UnityEngine;

namespace Assets.CreateLoad
{
    class SelectMapsetButton : MonoBehaviour
    {
        private string mapName;

        void OnMouseDown()
        {
            Global.FullPathToMapFolder = Application.persistentDataPath + "/" + mapName + "/";
            FindObjectOfType<LoaderMaps>().UpdateMaps();
        }

        public void SetText(string text)
        {
            mapName = text;
        }
    }
}
