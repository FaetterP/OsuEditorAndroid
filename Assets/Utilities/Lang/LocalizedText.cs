using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities.Lang
{
    [RequireComponent(typeof(Text))]
    class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
                         private Text _thisText;
                         internal LocalizedString _content;

        internal void Awake()
        {
            _thisText = GetComponent<Text>();
            _content = new LocalizedString(_key);
        }

        void OnEnable()
        {
            _thisText.text = _content.GetValue();
        }
    }
}
