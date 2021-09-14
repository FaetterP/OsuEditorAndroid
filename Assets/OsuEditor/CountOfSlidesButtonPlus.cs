using Assets.Elements;
using Assets.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class CountOfSlidesButtonPlus : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks creator;
        [SerializeField] private Text num;

        void OnMouseDown()
        {
            (Global.SelectedHitObject as OsuSlider).CountOfSlides += 1;
            num.text = (Global.SelectedHitObject as OsuSlider).CountOfSlides.ToString();

            (Global.SelectedHitObject as OsuSlider).UpdateTimeEnd();
            creator.UpdateCircleMarks();

            foreach(var t in FindObjectsOfType<OsuSlider>())
            {
                t.RemoveFromScreen();
            }
        }
    }
}
