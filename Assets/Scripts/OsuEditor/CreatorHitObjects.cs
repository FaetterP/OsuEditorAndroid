using Assets.Scripts.Elements;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class CreatorHitObjects : MonoBehaviour
    {
        private static List<int> _hitObjectsOnScreen = new List<int>();

        void Update()
        {
            foreach(OsuHitObject t in Global.Map.OsuHitObjects)
            {
                if (Global.MusicTime + Global.AR_ms<t.Time) { break; }

                if (!_hitObjectsOnScreen.Contains(t.Time) && t.IsRightTime())
                {
                    OsuHitObject created = Instantiate(t, gameObject.transform);
                    created.Init(t);
                    created.transform.SetAsFirstSibling();
                    _hitObjectsOnScreen.Add(t.Time);
                }
            }
        }

        public static void RemoveObjectFromList(int time)
        {
            _hitObjectsOnScreen.Remove(time);
        }
    }
}
