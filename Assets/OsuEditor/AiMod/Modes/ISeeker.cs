using Assets.OsuEditor.AiMod.Messages;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modes
{
    interface ISeeker
    {
        List<AiMessage> FindError();
    }
}
