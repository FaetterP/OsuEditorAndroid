using UnityEngine;

namespace Assets.Scripts.OsuEditor.AiMod
{
    class RefreshAiModButton : MonoBehaviour
    {
        [SerializeField] private AiMessagesLoader _loader;

        private void OnMouseDown()
        {
            _loader.Refresh();
        }
    }
}
