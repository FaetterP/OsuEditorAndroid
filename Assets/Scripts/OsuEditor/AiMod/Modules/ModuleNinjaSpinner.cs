using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleNinjaSpinner : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.ninjaSpinner");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var hitObject in Global.Map.OsuHitObjects)
            {
                if (hitObject is OsuSpinner)
                {
                    OsuSpinner spinner = hitObject as OsuSpinner;
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
