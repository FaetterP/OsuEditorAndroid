using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleAllTimingSectionsQuiet : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.allTimingSectionsQuiet");

        public ModuleType Type => ModuleType.Timing;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach(var t in Global.Map.TimingPoints)
            {
                if (t.Volume > 5)
                {
                    return ret;
                }
            }

            ret.Add(new Error(_message.GetValue(), null));
            return ret;
        }
    }
}
