namespace Assets.Scripts.OsuEditor.Settings.Difficulty
{
    class STSlider : DifficultySlider
    {
        protected override void SetValue(double value)
        {
            Global.Map.Difficulty.SliderTickRate = (int)value;
        }

        protected override double GetValue()
        {
            return Global.Map.Difficulty.SliderTickRate;
        }

        protected override string GetKey()
        {
            return "editor.settings.st";
        }
    }
}
