using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class ContentElementMapset : MonoBehaviour
    {
        [SerializeField] private Text textOnElement;
        [SerializeField] private SelectMapsetButton button;

        public void SetText(string text)
        {
            textOnElement.text = text;
            button.SetText(text);
        }
    }
}
