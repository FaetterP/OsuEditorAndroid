using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class DeleteButton : MonoBehaviour
    {
        private CreatorTimemarks _creator;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
        }

        public void DeleteHitObject(OsuHitObject hitObject)
        {
            Global.Map.OsuHitObjects.Remove(hitObject);
            Global.Map.UpdateComboInfos();

            foreach (var hitObjectDisplay in FindObjectsOfType<OsuHitObjectDisplay>())
            {
                Destroy(hitObjectDisplay.gameObject);
            }

            _creator.UpdateCircleMarks();
        }
    }
}
