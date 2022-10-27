using Assets.Scripts.OsuEditor.AiMod.Messages;
using Assets.Scripts.OsuEditor.AiMod.Modes;
using Assets.Scripts.Utilities.Lang;
using System.Collections.Generic;

namespace Assets.Scripts.OsuEditor.AiMod
{
    class SearchEngine
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.noProblemsFound");
        private ISeeker _seeker;

        public SearchEngine(ISeeker seeker)
        {
            _seeker = seeker;
        }

        public List<AiMessage> Scan()
        {
            List<AiMessage> ret = new List<AiMessage>();
            ret = _seeker.FindError();

            if (ret.Count == 0)
                ret.Add(new Info(_message.GetValue(), null));
            return ret;
        }
    }
}
