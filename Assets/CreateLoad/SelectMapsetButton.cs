using UnityEngine;

namespace Assets.CreateLoad
{
    class SelectMapsetButton : MonoBehaviour
    {
        private string _mapName;
        private LoaderMaps _loaderMaps;

        private void Awake()
        {
            _loaderMaps = FindObjectOfType<LoaderMaps>();
        }

        private void OnMouseDown()
        {
            Global.FullPathToMapFolder = Application.persistentDataPath + "/" + _mapName + "/";
            _loaderMaps.UpdateMaps();
        }

        public void SetText(string text)
        {
            _mapName = text;
        }
    }
}
