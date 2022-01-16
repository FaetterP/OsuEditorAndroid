using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleSimultaneousObjects : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.simultaneousObjects");

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            for (int i = 0; i < Global.Map.OsuHitObjects.Count - 1; i++)
            {
                int leftTime = Global.Map.OsuHitObjects[i].Time;
                int rightTime = Global.Map.OsuHitObjects[i + 1].Time;
                if (rightTime == leftTime)
                {
                    ret.Add(new Error(_message.GetValue(), leftTime));
                }
            }

            return ret;
        }
    }
}
