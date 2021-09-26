using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad.LoadExceptions
{
    class SaveLogButton : MonoBehaviour
    {
        public Exception exception;
        [SerializeField] private Text _pathText;        

        void OnMouseDown()
        {
            _pathText.text = Application.persistentDataPath+"/";
        }
    }
}
