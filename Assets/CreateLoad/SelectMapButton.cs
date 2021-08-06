using UnityEngine;
using UnityEngine.SceneManagement;

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
