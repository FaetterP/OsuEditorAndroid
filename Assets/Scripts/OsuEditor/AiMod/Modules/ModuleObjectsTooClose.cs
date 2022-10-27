using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleObjectsTooClose : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.objectsTooClose");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            for (int i = 0; i < Global.Map.OsuHitObjects.Count - 1; i++)
            {
                int leftTime = Global.Map.OsuHitObjects[i].Time;
                int rightTime = Global.Map.OsuHitObjects[i + 1].Time;
                if (rightTime - leftTime < 10)
                {
                    ret.Add(new Error(_message.GetValue(), leftTime));
                }
            }

            return ret;
        }
    }
}
