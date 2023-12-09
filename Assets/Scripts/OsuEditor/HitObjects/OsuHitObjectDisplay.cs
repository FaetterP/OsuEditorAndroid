using Assets.Scripts.MapInfo.HitObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.HitObjects
{
    abstract class OsuHitObjectDisplay : MonoBehaviour
    {
        protected OsuHitObject _hitObject;

        private void OnDisable()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            CreatorHitObjects.RemoveObjectFromList(_hitObject);
        }

        public abstract void Init(OsuHitObject obj);

        public int Time => _hitObject.Time;
        
        public Color ComboColor => _hitObject.ComboColor;
    }
}
