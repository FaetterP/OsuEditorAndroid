using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class CountOfSlidesButtonMinus : MonoBehaviour
    {
        [SerializeField] private Text _textNumber;
        [SerializeField] private CreatorTimemarks _creator;

        void OnMouseDown()
        {
            OsuSlider slider = Global.SelectedHitObject as OsuSlider;

            if (slider.CountOfSlides > 1)
            {
                _textNumber.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

                (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd(Global.Map);
                _creator.UpdateCircleMarks();

                foreach (var t in FindObjectsOfType<OsuSliderDisplay>())
                {
                    Destroy(t.gameObject);
                }
            }
        }
    }
}
