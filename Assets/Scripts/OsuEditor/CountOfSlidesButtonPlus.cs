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
            (Global.SelectedHitObject as OsuSlider).CountOfSlides += 1;
            _textNumber.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

            (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
            _creator.UpdateCircleMarks();

            foreach(var t in FindObjectsOfType<OsuSlider>())
            {
                Destroy(t.gameObject);
            }
        }
    }
}
