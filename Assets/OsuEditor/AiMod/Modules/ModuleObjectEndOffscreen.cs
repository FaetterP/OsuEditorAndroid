using Assets.Elements;
using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleObjectEndOffscreen : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.objectEndOffscreen");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var t in Global.Map.OsuHitObjects)
            {
                if(t is OsuSlider)
                {
                    OsuSlider slider = t as OsuSlider;
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
