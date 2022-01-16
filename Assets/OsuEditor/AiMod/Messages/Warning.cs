using UnityEngine;

namespace Assets.OsuEditor.AiMod.Messages
{
    class Warning : AiMessage
    {
        private Sprite _icon;

        public Warning(string text, int? time) : base(text, time)
        {
        }

        public override void InitIcon()
        {
            _icon = Resources.Load<Sprite>(@"Icons/Warning");
        }

        public override Sprite GetIcon()
        {
            return _icon;
        }
    }
}
