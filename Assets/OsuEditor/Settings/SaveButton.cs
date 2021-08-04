using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using System.IO;
using UnityEngine;

namespace Assets.OsuEditor.Settings
{
    class SaveButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.SaveToFile();
        }
    }
}
