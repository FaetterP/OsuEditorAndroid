using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings
{
    class SaveAndExitButton : IClickable
    {
        public override void Click()
        {
            Global.Map.SaveToFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }
}
