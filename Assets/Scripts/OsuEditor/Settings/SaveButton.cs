using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utilities;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings
{
    class SaveButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.SaveToFile();
        }
    }
}
