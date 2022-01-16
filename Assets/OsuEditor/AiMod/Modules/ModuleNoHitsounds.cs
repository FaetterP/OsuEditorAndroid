using Assets.Elements;
using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleNoHitsounds : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.noHitsounds");

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
