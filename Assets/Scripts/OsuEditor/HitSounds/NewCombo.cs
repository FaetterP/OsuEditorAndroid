using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class NewCombo : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks creator;
        private Image _thisImage;

        private void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            OsuCircle c = Global.SelectedHitObject as OsuCircle;
            if (Global.SelectedHitObject is OsuSlider)
            {
                if (c.ComboSum == 2) { _thisImage.color = new Color(1, 1, 1, 0.5f); }
                else { _thisImage.color = new Color(1, 1, 1, 1); }
            }
            else
            {
                if (c.ComboSum == 1) { _thisImage.color = new Color(1, 1, 1, 0.5f); }
                else { _thisImage.color = new Color(1, 1, 1, 1); }
            }
        }

        private void OnMouseDown()
        {
            OsuHitObject selectedObject = Global.SelectedHitObject;
            if (Global.SelectedHitObject is OsuSlider)
            {
                OsuSlider slider = Global.SelectedHitObject as OsuSlider;
                if (slider.ComboSum >= 6) { slider.ComboSum += 16; }
                if (slider.ComboSum == 2) { slider.ComboSum = 6; }
                if (slider.ComboSum == (Global.Map.Colors.Count - 1) * 16 + 6) { slider.ComboSum = 2; }
            }
            else if (Global.SelectedHitObject is OsuCircle)
            {
                OsuCircle circle = Global.SelectedHitObject as OsuCircle;
                if (circle.ComboSum >= 5) { circle.ComboSum += 16; }
                if (circle.ComboSum == 1) { circle.ComboSum = 5; }
                if (circle.ComboSum == (Global.Map.Colors.Count - 1) * 16 + 5) { circle.ComboSum = 1; }
            }

            OnEnable();
            foreach (var t in FindObjectsOfType<OsuCircleDisplay>())
            {
                Destroy(t.gameObject);
            }
            Global.Map.UpdateComboInfos();
            creator.UpdateCircleMarks();
        }
    }
}
