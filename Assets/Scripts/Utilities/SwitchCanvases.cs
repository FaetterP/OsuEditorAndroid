using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    class SwitchCanvases : MonoBehaviour
    {
        [SerializeField] private Canvas[] toEnable;
        [SerializeField] private Canvas[] toDisable;
        void OnMouseDown()
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
