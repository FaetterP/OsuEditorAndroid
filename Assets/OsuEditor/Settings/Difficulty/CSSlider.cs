namespace Assets.OsuEditor.Settings.Difficulty
{
    class CSSlider : DifficultySlider
    {
        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.CircleSize = value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.CircleSize;
        }
    }
}
