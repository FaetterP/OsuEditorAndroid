using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class ContentElementMap : MonoBehaviour
    {
        [SerializeField] private Text _textOnElement;
        [SerializeField] private SelectMapButton _button;

        public void SetText(string text)
        {
            _textOnElement.text = text;
            _button.SetText(text);
        }
    }
}
