using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.TimingPoints
{
    class SetPreviewButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            Global.Map.General.PreviewTime = Global.MusicTime;
            StartCoroutine(SetTime());
        }
        public IEnumerator SetTime()
        {
            GetComponent<Image>().color = Color.cyan;
            yield return new WaitForSeconds(0.2f);
            GetComponent<Image>().color = Color.white;
           // Application.OpenURL(@"file:///" + Global.FullPathToMap);
        }

    }
}
