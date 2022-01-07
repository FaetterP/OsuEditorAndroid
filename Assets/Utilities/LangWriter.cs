using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Utilities
{
    [RequireComponent(typeof(Text))]
    class LangWriter : MonoBehaviour
    {
        [SerializeField] [TextArea()] private string en, ru;
        private Dictionary<Lang, string> localizedText;
        private Text thisText;

        void Awake()
        {
            thisText = GetComponent<Text>();

            localizedText = new Dictionary<Lang, string>();
            localizedText.Add(Lang.EN, en);
            localizedText.Add(Lang.RU, ru);
        }

        void OnEnable()
        {
            thisText.text = localizedText[Global.Lang];
        }

        public string GetText()
        {
            return localizedText[Global.Lang];
        }
    }
}