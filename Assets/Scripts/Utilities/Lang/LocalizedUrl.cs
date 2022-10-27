using UnityEngine;

namespace Assets.Scripts.Utilities.Lang
{
    class LocalizedUrl : LocalizedText
    {
        [SerializeField] private string _keyUrl;
                         private LocalizedString _contentUrl;

        new void Awake()
        {
            _contentUrl = new LocalizedString(_keyUrl);
            base.Awake();
        }

        void OnMouseDown()
        {
            Application.OpenURL(_contentUrl.GetValue());
        }
    }
}
