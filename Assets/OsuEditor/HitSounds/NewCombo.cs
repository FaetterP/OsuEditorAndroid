using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.HitSounds
{
    class NewCombo : MonoBehaviour
    {
        private Image thisImage;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void OnEnable()
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
        void OnMouseDown()
        {
            OsuCircle c = Global.SelectedHitObject as OsuCircle;
            if (Global.SelectedHitObject is OsuSlider)
            {
                if (c.combo_sum >= 6) { c.combo_sum += 16; }
                if (c.combo_sum == 2) { c.combo_sum = 6; }
                if (c.combo_sum == (Global.Map.Colors.Count-1) * 16 + 6) { c.combo_sum = 2; }
            }
            else
            {
                if (c.combo_sum >= 5) { c.combo_sum += 16; }
                if (c.combo_sum == 1) { c.combo_sum = 5; }
                if (c.combo_sum == (Global.Map.Colors.Count-1) * 16 + 5) { c.combo_sum = 1; }
            }

            OnEnable();
            foreach(var t in FindObjectsOfType<OsuCircle>())
            {
                t.RemoveFromScreen();
            }
            Global.Map.UpdateComboInfos();
            CreatorTimemarks.UpdateCircleMarks();
        }
    }
}
