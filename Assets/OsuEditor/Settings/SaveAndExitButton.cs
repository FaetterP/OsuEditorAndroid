using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings
{
    class SaveAndExitButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.SaveToFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }
}
