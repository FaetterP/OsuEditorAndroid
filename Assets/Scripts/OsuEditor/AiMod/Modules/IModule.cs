using Assets.Scripts.OsuEditor.AiMod.Messages;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    interface IModule
    {
        ModuleType Type { get; }

        List<AiMessage> GetMessages();
    }
}
