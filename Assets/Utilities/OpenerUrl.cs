using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utilities
{
    class OpenerUrl : MonoBehaviour
    {
        [SerializeField] private string url_EN, url_RU;
        private Dictionary<Lang, string> urls;

        void Awake()
        {
            urls = new Dictionary<Lang, string>();
            urls.Add(Lang.EN, url_EN);
            urls.Add(Lang.RU, url_RU);
        }

        void OnMouseDown()
        {
            Application.OpenURL(urls[Global.Lang]);
        }
    }
}
