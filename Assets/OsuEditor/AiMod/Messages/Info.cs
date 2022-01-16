using UnityEngine;

namespace Assets.OsuEditor.AiMod.Messages
{
    class Info : AiMessage
    {
        private Sprite _icon;

        public Info(string text, int? time) : base(text, time)
        {
        }

        public override void InitIcon()
        {
            _icon = Resources.Load<Sprite>(@"Icons/Info");
        }

        public override Sprite GetIcon()
        {
            return _icon;
        }
    }
}
