using Assets.OsuEditor.AiMod.Messages;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    interface IModule
    {
        ModuleType Type { get; }

        List<AiMessage> GetMessages();
    }
}
