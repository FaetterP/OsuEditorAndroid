namespace Assets.Scripts.OsuEditor.Settings.Difficulty
{
    class ARSlider : DifficultySlider
    {
        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.ApproachRate = value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.ApproachRate;
        }

        protected override string GetKey()
        {
            return "editor.settings.ar";
        }
    }
}
