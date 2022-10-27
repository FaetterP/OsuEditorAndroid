using Assets.Scripts.Elements;
using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleNoHitsounds : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.noHitsounds");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach(var t in Global.Map.OsuHitObjects)
            {
                if(t is OsuCircle)
                {
                    OsuCircle circle = t as OsuCircle;
                    if (circle.Clap || circle.Finish || circle.Whisle)
                    {
                        return ret;
                    }
                }
            }
            ret.Add(new Error(_message.GetValue(), null));
            return ret;
        }
    }
}
