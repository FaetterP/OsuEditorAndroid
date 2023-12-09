using Assets.Scripts.MapInfo.HitObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class CreatorHitObjects : MonoBehaviour
    {
        private static CreatorHitObjects s_instance;
        private static List<OsuHitObject> _hitObjectsOnScreen = new List<OsuHitObject>();

        public static CreatorHitObjects Instance => s_instance;

        private void Awake()
        {
            s_instance = this;
        }

        void Update()
        {
            foreach (OsuHitObject hitObject in Global.Map.OsuHitObjects)
            {
                if (Global.MusicTime + Global.AR_ms < hitObject.Time) { break; }

                if (!_hitObjectsOnScreen.Contains(hitObject) && hitObject.IsRightTime())
                {
                    hitObject.SpawnHitObject();
                    _hitObjectsOnScreen.Add(hitObject);
                }
            }
        }

        public static void RemoveObjectFromList(OsuHitObject hitObject)
        {
            _hitObjectsOnScreen.Remove(hitObject);
        }
    }
}
