using Assets.Elements;
using Assets.OsuEditor.Timeline;
using UnityEngine;

namespace Assets.OsuEditor
{
    class DeleteButton : MonoBehaviour
    {
        private CreatorTimemarks _creator;

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
        }

        public void DeleteHitObject(OsuHitObject obj)
        {
            Global.Map.OsuHitObjects.Remove(obj);
            Global.Map.UpdateComboInfos();

            foreach (var t in FindObjectsOfType<OsuHitObject>())
            {
                Destroy(t.gameObject);
            }

            _creator.UpdateCircleMarks();
        }
    }
}
