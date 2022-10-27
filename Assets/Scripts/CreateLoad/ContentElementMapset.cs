using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CreateLoad
{
    class ContentElementMapset : MonoBehaviour
    {
        [SerializeField] private Text _textOnElement;
        [SerializeField] private SelectMapsetButton _button;

        public void SetText(string text)
        {
            _textOnElement.text = text;
            _button.SetText(text);
        }
    }
}
