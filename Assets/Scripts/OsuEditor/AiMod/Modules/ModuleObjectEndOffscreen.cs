using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleObjectEndOffscreen : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.objectEndOffscreen");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var hitObject in Global.Map.OsuHitObjects)
            {
                if (hitObject is OsuSlider)
                {
                    OsuSlider slider = hitObject as OsuSlider;
                    double x = slider.SliderPoints.Last().x;
                    double y = slider.SliderPoints.Last().y;

                    if (x < 0 || y < 0 || x > 512 || y > 384)
                    {
                        ret.Add(new Warning(_message.GetValue(), slider.Time));
                    }
                }
            }

            return ret;
        }
    }
}
