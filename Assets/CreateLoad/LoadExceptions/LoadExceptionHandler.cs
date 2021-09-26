using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad.LoadExceptions
{
    class LoadExceptionHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _exceptionText;
        [SerializeField] private SaveLogButton _saveButton;

        public void Handle(Exception e)
        {
            _canvas.gameObject.SetActive(true);
            _exceptionText.text = e.ToString();
            _saveButton.exception = e;
        }
    }
}
