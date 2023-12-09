using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleNoBackgroundImage : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.noBackgroundImage");

        public ModuleType Type => ModuleType.Design;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            if (Global.Map.Events.BackgroundImage == "")
                ret.Add(new Error(_message.GetValue(), null));

            return ret;
        }
    }
}
