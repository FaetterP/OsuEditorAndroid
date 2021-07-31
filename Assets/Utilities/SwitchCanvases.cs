using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class SwitchCanvases : IClickable
    {
        [SerializeField] private Canvas[] toEnable;
        [SerializeField] private Canvas[] toDisable;
        public override void Click()
        {
            foreach (var t in toEnable)
            {
                t.gameObject.SetActive(true);
            }
            
            foreach(var t in toDisable)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
