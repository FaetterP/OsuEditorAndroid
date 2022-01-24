using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleBackgroundTooLarge : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.backgroundTooLarge");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            // TODO
            //ret.Add(new Warning(_message.GetValue(), null));

            return ret;
        }
    }
}
