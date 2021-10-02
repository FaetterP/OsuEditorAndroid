namespace Assets.OsuEditor.Settings.Difficulty
{
    class HPSlider : DifficultySlider
    {
        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.HPDrainRate = value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.HPDrainRate;
        }
    }
}
