using Assets.Elements;
using Assets.OsuEditor.Timeline;
using UnityEngine;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class SMSlider : DifficultySlider
    {
        [SerializeField] private CreatorTimemarks _creator;

        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.SliderMultiplier = value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.SliderMultiplier;
        }

        protected override void ChangeValue()
        {
            base.ChangeValue();

            foreach (var t in Global.Map.OsuHitObjects)
            {
                if (t is OsuSlider)
                {
                    (t as OsuSlider).UpdateTimeEnd();
                }
            }

            _creator.UpdateCircleMarks();
        }

        protected override string GetKey()
        {
            return "editor.settings.sm";
        }
    }
}
