using UnityEngine;

namespace Assets.Scripts.OsuEditor.AiMod.Messages
{
    abstract class AiMessage
    {
        private readonly string _text;
        private readonly int? _time;

        public AiMessage(string text, int? time)
        {
            _text = text;
            _time = time;
        }

        public abstract void InitIcon();

        public abstract Sprite GetIcon();

        public string GetText()
        {
            return _text;
        }

        public string GetTime()
        {
            if (_time.HasValue)
                return OsuMath.ConvertTimestampToString(_time.Value);
            else
                return "";
        }
    }
}
