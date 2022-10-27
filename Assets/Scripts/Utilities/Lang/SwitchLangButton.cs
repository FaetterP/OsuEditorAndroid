using UnityEngine;

namespace Assets.Scripts.Utilities.Lang
{
    class SwitchLangButton : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;

        void OnMouseDown()
        {
            LocalizedString.ChangeLanguage(_language);

            foreach(var t in FindObjectsOfType<LocalizedText>())
            {
                t.gameObject.SetActive(false);
                t.gameObject.SetActive(true);
            }
        }
    }
}
