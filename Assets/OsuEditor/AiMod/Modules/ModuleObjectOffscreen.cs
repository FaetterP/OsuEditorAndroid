using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleObjectOffscreen : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.objectOffscreen");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach(var t in Global.Map.OsuHitObjects)
            {
                if (t.X < 0 || t.Y < 0 || t.X > 512 || t.Y > 384)
                {
                    ret.Add(new Warning(_message.GetValue(), t.Time));
                }
            }

            return ret;
        }
    }
}
