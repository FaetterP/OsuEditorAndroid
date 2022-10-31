using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class CountOfSlidesButtonPlus : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks _creator;
        [SerializeField] private Text _textNumber;

        void OnMouseDown()
        {
            OsuSlider slider = Global.SelectedHitObject as OsuSlider;
            slider.CountOfSlides += 1;
            _textNumber.text = slider.CountOfSlides.ToString();

            slider.UpdateTimeEnd(Global.Map);
            _creator.UpdateCircleMarks();

            foreach(var t in FindObjectsOfType<OsuSliderDisplay>())
            {
                Destroy(t.gameObject);
            }
        }
    }
}
