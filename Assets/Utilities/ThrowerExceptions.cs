using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Utilities
{
    class ThrowerExceptions
    {
        /*
        private static Dictionary<Lang, string> pairs = new Dictionary<Lang, string>{
            { Lang.EN, "The map format is not supported. Required v14." },
            { Lang.RU, "Формат карты не поддерживается. Требуется v14." }
        };
        */

        public static Exception GetExc(Dictionary<Lang, string> pairs)
        {
            return new Exception(pairs[Global.Lang]);
        }
    }
}