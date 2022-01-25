﻿using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;
using System.Linq;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleMapTooShort : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.mapTooShort");

        public ModuleType Type => ModuleType.Timing;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            int firstTime = Global.Map.OsuHitObjects.First().Time;
            int lastTime = Global.Map.OsuHitObjects.Last().Time;

            if (lastTime - firstTime < 45 * 1000)
                ret.Add(new Warning(_message.GetValue(), null));

            return ret;
        }
    }
}
