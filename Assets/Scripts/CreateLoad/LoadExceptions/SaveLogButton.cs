﻿using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CreateLoad.LoadExceptions
{
    class SaveLogButton : MonoBehaviour
    {
        [SerializeField] private Text _pathText;
        public Exception Exception;

        private void OnMouseDown()
        {
            string name = "log " + Global.Map.Metadata.TitleUnicode + " (" + Global.Map.Metadata.Version + ").txt";
            _pathText.text = "";
            _pathText.text += Application.persistentDataPath + "/" + name;

            File.WriteAllText(_pathText.text, GetText());
        }

        private string GetText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Exception.ToString());
            sb.Append("\n---------------------------------------------------------\n");
            sb.Append(File.ReadAllText(Global.FullPathToMap));

            return sb.ToString();
        }
    }
}
