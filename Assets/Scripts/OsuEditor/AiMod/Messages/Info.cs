using UnityEngine;

namespace Assets.Scripts.OsuEditor.AiMod.Messages
{
    class Info : AiMessage
    {
        private static Sprite _icon;

        public Info(string text, int? time) : base(text, time)
        {
        }

        public override void InitIcon()
        {
            _icon = Resources.Load<Sprite>(@"Icons/Info");
        }

        public override Sprite GetIcon()
        {
            if (_icon == null)
                InitIcon();

            return _icon;
        }
    }
}
