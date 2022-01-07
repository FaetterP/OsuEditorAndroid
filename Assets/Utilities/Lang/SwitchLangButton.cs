using UnityEngine;

namespace Assets.Utilities.Lang
{
    class SwitchLangButton : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;

        void OnMouseDown()
        {
            LocalizedString.ChangeLanguage(_language);
        }
    }
}
