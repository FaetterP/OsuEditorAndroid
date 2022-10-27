using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
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
