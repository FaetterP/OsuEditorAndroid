using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class MusicLineMarkExpanded : MonoBehaviour
    {
        [NonSerialized] public Color Color;
        [NonSerialized] public int timeLeft, timeRight;

        void Start()
        {
            AudioSource music = FindObjectOfType<AudioSource>();
            GetComponent<Image>().color = Color;
            var pos = transform.localPosition;
            int xleft= OsuMath.GetMarkX(timeLeft, (int)(transform.parent.GetComponent<RectTransform>().rect.width / -2), (int)(transform.parent.GetComponent<RectTransform>().rect.width / 2), 0, (int)(music.clip.length * 1000));
            int xright= OsuMath.GetMarkX(timeRight, (int)(transform.parent.GetComponent<RectTransform>().rect.width / -2), (int)(transform.parent.GetComponent<RectTransform>().rect.width / 2), 0, (int)(music.clip.length * 1000));
            pos.x = (xleft + xright) / 2f;
            pos.y = 0;
            transform.localPosition = pos;
            var sc = transform.localScale;
            sc.x = xright - xleft;
            transform.localScale = sc;
        }
    }
}
