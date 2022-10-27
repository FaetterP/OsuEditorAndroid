using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    class NewCombo : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks creator;
        private Image thisImage;

        private void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            OsuCircle c = Global.SelectedHitObject as OsuCircle;
            if (Global.SelectedHitObject is OsuSlider)
            {
                if (c.combo_sum == 2) { thisImage.color = new Color(1, 1, 1, 0.5f); }
                else { thisImage.color = new Color(1, 1, 1, 1); }
            }
            else
            {
                if (c.combo_sum == 1) { thisImage.color = new Color(1, 1, 1, 0.5f); }
                else { thisImage.color = new Color(1, 1, 1, 1); }
            }
        }

        private void OnMouseDown()
        {
            OsuCircle c = Global.SelectedHitObject as OsuCircle;
            if (Global.SelectedHitObject is OsuSlider)
            {
                if (c.combo_sum >= 6) { c.combo_sum += 16; }
                if (c.combo_sum == 2) { c.combo_sum = 6; }
                if (c.combo_sum == (Global.Map.Colors.Count - 1) * 16 + 6) { c.combo_sum = 2; }
            }
            else
            {
                if (c.combo_sum >= 5) { c.combo_sum += 16; }
                if (c.combo_sum == 1) { c.combo_sum = 5; }
                if (c.combo_sum == (Global.Map.Colors.Count - 1) * 16 + 5) { c.combo_sum = 1; }
            }

            OnEnable();
            foreach (var t in FindObjectsOfType<OsuCircle>())
            {
                Destroy(t.gameObject);
            }
            Global.Map.UpdateComboInfos();
            creator.UpdateCircleMarks();
        }
    }
}
