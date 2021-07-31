using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.CreateLoad
{
    class SelectMapsetButton : IClickable
    {
        private string MapName;
        public override void Click()
        {
            Global.FullPathToMapFolder = Application.persistentDataPath+"/"+MapName+"/";
            FindObjectOfType<LoaderMaps>().UpdateMaps();
            var files = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles();
            foreach (var t in files)
            {
                if (t.Name.EndsWith(".mp3")) { Global.Map.General.AudioFilename = t.Name; }
                if (t.Name.EndsWith(".jpg")) { Global.Map.Events.BackgroungImage = t.Name; }
            }
        }

        public void SetText(string text)
        {
            MapName = text;
        }
    }
}
