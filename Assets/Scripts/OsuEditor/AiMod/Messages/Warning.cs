using UnityEngine;

namespace Assets.Scripts.OsuEditor.AiMod.Messages
{
    class Warning : AiMessage
    {
        private static Sprite _icon;

        public Warning(string text, int? time) : base(text, time)
        {
        }

        public override void InitIcon()
        {
            _icon = Resources.Load<Sprite>(@"Icons/Warning");
        }

        public override Sprite GetIcon()
        {
            if (_icon == null)
                InitIcon();

            return _icon;
        }
    }
}
