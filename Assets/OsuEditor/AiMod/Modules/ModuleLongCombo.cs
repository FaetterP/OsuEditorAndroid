using Assets.Elements;
using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleLongCombo : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.longCombo");

        public ModuleType Type => ModuleType.Compose;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var t in Global.Map.OsuHitObjects)
            {
                if (t is OsuSpinner)
                    continue;

                if (Global.Map.GetComboInfo(t.Time).Number >= 25)
                {
                    ret.Add(new Warning(_message.GetValue(), t.Time));
                }
            }

            return ret;
        }
    }
}
