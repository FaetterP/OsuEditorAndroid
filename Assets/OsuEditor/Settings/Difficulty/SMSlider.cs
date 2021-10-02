using Assets.Elements;

namespace Assets.OsuEditor.Settings.Difficulty
{
    class SMSlider : DifficultySlider
    {
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
        }
    }
}
