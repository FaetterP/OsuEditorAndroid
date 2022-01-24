using Assets.Elements;
using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleSliderTooLarge : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.sliderTooLarge");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var t in Global.Map.OsuHitObjects)
            {
                if (t is OsuSlider)
                {
                    OsuSlider slider = t as OsuSlider;
                    if (slider.SliderPoints.Count >= 20)
                    {
                        ret.Add(new Warning(_message.GetValue(), slider.Time));
                    }
                }
            }

            return ret;
        }
    }
}
