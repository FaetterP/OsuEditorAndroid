using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.OsuEditor.AiMod.Modules
{
    class ModuleMapTooLong : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.mapTooLong");

        public ModuleType Type => ModuleType.Timing;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            int firstTime = Global.Map.OsuHitObjects.First().Time;
            int lastTime = Global.Map.OsuHitObjects.Last().Time;

            if (lastTime - firstTime > 6 * 60 * 1000)
                ret.Add(new Warning(_message.GetValue(), null));

            return ret;
        }
    }
}
