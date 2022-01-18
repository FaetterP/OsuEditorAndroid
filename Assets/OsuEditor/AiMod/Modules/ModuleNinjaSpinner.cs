using Assets.Elements;
using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleNinjaSpinner : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.ninjaSpinner");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var t in Global.Map.OsuHitObjects)
            {
                if (t is OsuSpinner)
                {
                    OsuSpinner spinner = t as OsuSpinner;
                    foreach (var tt in Global.Map.OsuHitObjects)
                    {
                        if (tt.Time >= spinner.Time && tt.Time <= spinner.TimeEnd && tt != spinner)
                        {
                            ret.Add(new Error(_message.GetValue(), tt.Time));
                        }
                    }
                }
            }
            return ret;
        }
    }
}
