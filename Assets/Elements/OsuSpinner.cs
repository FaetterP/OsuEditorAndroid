using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elements
{
    class OsuSpinner : OsuHitObject
    {
        public int time_end = 0;
        private Image thisImage;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(x, y));
        }
        void Update()
        {
            if (Global.MusicTime < time - Global.AR_ms)
            {
                OsuEditor.CreatorHitObjects.RemoveObjectFromScreen(time);
                Destroy(gameObject);
            }
            else if (Global.MusicTime<time)
            {
                transform.rotation = Quaternion.Euler(0, 0, time);
                thisImage.color = new Color(1, 1, 1, 0.1f);
            }
            else if (Global.MusicTime > time && Global.MusicTime < time_end)
            {
                transform.rotation = Quaternion.Euler(0, 0, Global.MusicTime);
                thisImage.color = new Color(1, 1, 1, 0.1f+0.9f*((Global.MusicTime-time*1.0f)/(time_end-time)));
            }
            else
            {
                OsuEditor.CreatorHitObjects.RemoveObjectFromScreen(time);
                Destroy(gameObject);
            }

        }

        public override string ToString()
        {
            // 256,192,734,12,8,4992,0:1:0:0:
            return "256,192," + time + ",12,8," + time_end + ",0:1:0:0";
        }
    }
}
