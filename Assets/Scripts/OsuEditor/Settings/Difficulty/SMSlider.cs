using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.Settings.Difficulty
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

            foreach (var hitObject in Global.Map.OsuHitObjects)
            {
                if (hitObject is OsuSlider)
                {
                    (hitObject as OsuSlider).UpdateTimeEnd(Global.Map);
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
