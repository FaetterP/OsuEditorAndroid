using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings
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
