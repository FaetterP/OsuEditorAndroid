using Assets.OsuEditor.AiMod.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.AiMod
{
    class AiContentElement : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _text;
        [SerializeField] private Text _time;
                         private AiMessage _message;

        private void Start()
        {
            _icon.sprite = _message.GetIcon();
            _text.text = _message.GetText();
            _time.text = _message.GetTime();
        }

        public void Init(AiMessage message)
        {
            _message = message;
        }
    }
}
