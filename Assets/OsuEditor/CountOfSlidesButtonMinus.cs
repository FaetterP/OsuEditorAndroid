using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class CountOfSlidesButtonMinus : MonoBehaviour
    {
        [SerializeField] private Text num;
        void OnMouseDown()
        {
            if((Global.SelectedHitObject as OsuSlider).CountOfSlides > 1)
            {
                (Global.SelectedHitObject as OsuSlider).CountOfSlides -= 1;
                num.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

                (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
                CreatorTimemarks.UpdateCircleMarks();

                foreach (var t in FindObjectsOfType<OsuSlider>())
                {
                    t.RemoveFromScreen();
                }
            }
        }
    }
}
