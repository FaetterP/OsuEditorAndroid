using Assets.OsuEditor.AiMod.Messages;
using Assets.Utilities.Lang;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.OsuEditor.AiMod.Modules
{
    class ModuleBackgroundTooLarge : IModule
    {
        private LocalizedString _message = new LocalizedString("AiMod.message.backgroundTooLarge");

        public ModuleType Type => ModuleType.Design;

        public List<AiMessage> GetMessages()
        {
            List<AiMessage> ret = new List<AiMessage>();

            Background background = GameObject.FindObjectOfType<Background>();
            Sprite sprite = background.GetSprite();

            if (sprite.rect.x > 2560 || sprite.rect.y > 1440)
                ret.Add(new Warning(_message.GetValue(), null));

            return ret;
        }
    }
}
