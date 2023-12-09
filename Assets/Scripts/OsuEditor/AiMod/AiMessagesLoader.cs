using Assets.Scripts.OsuEditor.AiMod.Modes;
using UnityEngine;

namespace Assets.Scripts.OsuEditor.AiMod
{
    class AiMessagesLoader : MonoBehaviour
    {
        [SerializeField] private AiContentElement _element;
        private SearchEngine _search = new SearchEngine(new SeekerClassic());

        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            ClearContent();
            LoadMessages();
        }

        private void ClearContent()
        {
            foreach (var child in GetComponentsInChildren<AiContentElement>())
            {
                Destroy(child.gameObject);
            }
        }

        private void LoadMessages()
        {
            foreach (var t in _search.Scan())
            {
                AiContentElement created = Instantiate(_element, transform);
                created.Init(t);
            }
        }
    }
}
