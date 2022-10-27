using Assets.Scripts.Elements;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class CountOfSlidesButtonMinus : MonoBehaviour
    {
        [SerializeField] private Text num;
        [SerializeField] private CreatorTimemarks creator;

        void OnMouseDown()
        {
            if((Global.SelectedHitObject as OsuSlider).CountOfSlides > 1)
            {
                (Global.SelectedHitObject as OsuSlider).CountOfSlides -= 1;
                num.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

                (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
                creator.UpdateCircleMarks();

                foreach (var t in FindObjectsOfType<OsuSlider>())
                {
                    Destroy(t.gameObject);
                }
            }
        }
    }
}
