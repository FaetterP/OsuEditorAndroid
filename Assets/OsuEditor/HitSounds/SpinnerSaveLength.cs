using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.HitSounds
{
    class SpinnerSaveLength : MonoBehaviour
    {
        [SerializeField] private InputField inputFieldLength;

        void OnMouseDown()
        {
            OsuSpinner spinner = Global.SelectedHitObject as OsuSpinner;
            int length = int.Parse(inputFieldLength.text);
            length = Math.Abs(length);

            int sr = int.MaxValue;
            Timemark mark = null;
            foreach(var t in CreatorTimemarks.MarksToCreate)
            {
                if (sr > Math.Abs(t.time - spinner.Time - length))
                {
                    mark = t;
                    sr = Math.Abs(t.time - spinner.Time - length);
                }
                //else { break; }
            }

            (OsuMath.GetHitObjectFromTime(spinner.Time) as OsuSpinner).TimeEnd = mark.time;
            foreach(var t in FindObjectsOfType<OsuSpinner>())
            {
                t.RemoveFromScreen();
            }
            CreatorTimemarks.UpdateCircleMarks();
        }
    }
}
