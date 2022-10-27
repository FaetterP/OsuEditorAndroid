using Assets.Scripts.Elements;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
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
                Destroy(t.gameObject);
            }
        }
    }
}
