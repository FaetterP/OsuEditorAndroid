using UnityEngine;

namespace Assets.OsuEditor.AiMod
{
    class ReloadAiModButton : MonoBehaviour
    {
        [SerializeField] private AiMessagesLoader _loader;

        private void OnMouseDown()
        {
            _loader.Refresh();
        }
    }
}
