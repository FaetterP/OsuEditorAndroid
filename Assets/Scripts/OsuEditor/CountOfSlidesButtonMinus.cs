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
            if((Global.SelectedHitObject as OsuSlider).CountOfSlides > 1)
            {
                (Global.SelectedHitObject as OsuSlider).CountOfSlides -= 1;
                _textNumber.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

                (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
                _creator.UpdateCircleMarks();

                foreach (var t in FindObjectsOfType<OsuSlider>())
                {
                    Destroy(t.gameObject);
                }
            }
        }
    }
}
