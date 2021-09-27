using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad.LoadExceptions
{
    class LoadExceptionHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvasList;
        [SerializeField] private Canvas _canvasExc;
        [SerializeField] private Text _exceptionText;
        [SerializeField] private SaveLogButton _saveButton;

        public void Handle(Exception e)
        {
            _canvasExc.gameObject.SetActive(true);
            _canvasList.gameObject.SetActive(false);
            _exceptionText.text = e.ToString();
            _saveButton.exception = e;
        }
    }
}
