using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Utilities;

namespace Assets.OsuEditor
{
    class LeftButton : IClickable
    {
        [SerializeField] private LeftStatus LeftStatus;
        [SerializeField] private List<LeftButton> toDisable;
        public override void Click()
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
