using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.OsuEditor.AiMod.Modules;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod.Modes
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
            _modules.Add(new ModuleNoBackgroundImage());
            _modules.Add(new ModuleAllTimingSectionsQuiet());

            _modules.Add(new ModuleSliderTooLarge());
            _modules.Add(new ModuleLongCombo());
            _modules.Add(new ModuleObjectOffscreen());
            _modules.Add(new ModuleObjectEndOffscreen());
            _modules.Add(new ModuleShortSpinner());
            _modules.Add(new ModuleBackgroundTooLarge());
            _modules.Add(new ModuleMapTooLong());
            _modules.Add(new ModuleMapTooShort());
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
