using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.OsuEditor
{
    class LeftButton : MonoBehaviour
    {
        [SerializeField] private LeftStatus LeftStatus;
        [SerializeField] private List<LeftButton> toDisable;
        void OnMouseDown()
        {
            foreach(var t in toDisable)
            {
                t.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            GetComponent<Image>().color = Color.white;
            Global.LeftStatus = LeftStatus;
        }
    }
}
