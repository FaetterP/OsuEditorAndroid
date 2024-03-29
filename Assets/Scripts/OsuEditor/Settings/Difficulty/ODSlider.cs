﻿namespace Assets.Scripts.OsuEditor.Settings.Difficulty
{
    class ODSlider : DifficultySlider
    {
        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.OverallDifficulty = value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.OverallDifficulty;
        }

        protected override string GetKey()
        {
            return "editor.settings.od";
        }
    }
}
