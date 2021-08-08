using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class SwitchLangButton : MonoBehaviour
    {
        [SerializeField] Lang lang;

        void OnMouseDown()
        {
            Global.Lang = lang;
            foreach(var t in FindObjectsOfType<Text>())
            {
                t.gameObject.SetActive(false);
                t.gameObject.SetActive(true);
            }
        }
    }
}
