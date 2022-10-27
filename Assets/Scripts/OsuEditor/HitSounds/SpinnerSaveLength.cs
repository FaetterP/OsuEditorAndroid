using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class SpinnerSaveLength : MonoBehaviour
    {
        [SerializeField] private InputField inputFieldLength;

        void OnMouseDown()
        {
            /*OsuSpinner spinner = Global.SelectedHitObject as OsuSpinner;
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
            }

            (OsuMath.GetHitObjectFromTime(spinner.Time) as OsuSpinner).TimeEnd = mark.time;
            foreach(var t in FindObjectsOfType<OsuSpinner>())
            {
                t.RemoveFromScreen();
            }
            CreatorTimemarks.UpdateCircleMarks();*/
        }
    }
}
