using Assets.Scripts.OsuEditor.AiMod.Messages;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modes
{
    interface ISeeker
    {
        List<AiMessage> FindError();
    }
}
