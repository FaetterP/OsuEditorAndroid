using Assets.Utilities;
using UnityEngine;

namespace Assets.OsuEditor.Settings
{
    class SaveAndExitButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.SaveToFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)Scenes.LoadMap);
        }
    }
}
