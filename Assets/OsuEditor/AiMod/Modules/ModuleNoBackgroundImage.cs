using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleNoBackgroundImage : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.noBackgroundImage");

        public ModuleType Type => ModuleType.Design;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            if (Global.Map.Events.BackgroungImage == "")
                ret.Add(new Error(_message.GetValue(), null));

            return ret;
        }
    }
}
