using Assets.OsuEditor.AiMod.Messages;
using Assets.OsuEditor.AiMod.Modules;
using System.Collections.Generic;

namespace Assets.OsuEditor.AiMod.Modes
{
    class SeekerClassic : ISeeker
    {
        private List<IModule> _modules = new List<IModule>();

        public SeekerClassic()
        {
            _modules.Add(new ModuleObjectsTooClose());
            _modules.Add(new ModuleSimultaneousObjects());
            _modules.Add(new ModuleNoHitsounds());
            _modules.Add(new ModuleNinjaSpinner());
        }

        public List<AiMessage> FindError()
        {
            List<AiMessage> ret = new List<AiMessage>();

            foreach (var t in _modules)
            {
                List<AiMessage> messages = t.GetMessages();
                if (messages.Count > 0)
                {
                    ret.AddRange(messages);
                }
            }

            return ret;
        }
    }
}
