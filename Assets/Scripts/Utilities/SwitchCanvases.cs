using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class SwitchCanvases : MonoBehaviour // TODO: remove class
    {
        [SerializeField] private Canvas[] toEnable;
        [SerializeField] private Canvas[] toDisable;
        void OnMouseDown()
        {
            foreach (var t in toEnable)
            {
                t.gameObject.SetActive(true);
            }
            
            foreach(var t in toDisable)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
