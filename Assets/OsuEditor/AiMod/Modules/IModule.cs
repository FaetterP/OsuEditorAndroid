using Assets.OsuEditor.AiMod.Messages;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    interface IModule
    {
        List<AiMessage> GetMessages();
    }
}
