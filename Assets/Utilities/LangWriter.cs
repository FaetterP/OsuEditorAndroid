using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class LangWriter : MonoBehaviour
    {
        [SerializeField] [TextArea()] private string en = "", ru = "";

        void OnEnable()
        {
            Text TextOnThisObj = GetComponent<Text>();
            switch (Global.Lang)
            {
                case Lang.EN:
                    TextOnThisObj.text = en;
                    break;

                case Lang.RU:
                    TextOnThisObj.text = ru;
                    break;
            }
        }

        public string GetText()
        {
            switch (Global.Lang)
            {
                case Lang.EN:
                    return(en);
                case Lang.RU:
                    return(ru);
            }return null;
        }
    }
}
