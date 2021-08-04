using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utilities
{
    class OpenerUrl : MonoBehaviour
    {
        [SerializeField] private string URL_1 = "", URL_2 = "";
        void OnMouseDown()
        {
            Application.OpenURL(URL_1 + Global.Lang + URL_2);
        }
    }
}
