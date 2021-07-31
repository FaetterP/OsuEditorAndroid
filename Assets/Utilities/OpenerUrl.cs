using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utilities
{
    class OpenerUrl : IClickable
    {
        [SerializeField] private string URL_1 = "", URL_2 = "";
        public override void Click()
        {
            Application.OpenURL(URL_1 + Global.Lang + URL_2);
        }
    }
}
