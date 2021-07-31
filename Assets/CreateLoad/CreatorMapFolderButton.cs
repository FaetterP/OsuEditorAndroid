using Assets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class CreatorMapFolderButton : MonoBehaviour
    {
        public void CreateFolder(string name)
        {
            Global.FullPathToMapFolder = Application.persistentDataPath + "/" + name + "/";
            Directory.CreateDirectory(Global.FullPathToMapFolder);
        }
    }
}
