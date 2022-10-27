using UnityEngine;

namespace Assets.Scripts.CreateLoad
{
    class UpdateMapsetsButton : MonoBehaviour
    {
        [SerializeField] private LoaderMapsets _loader;

        private void OnMouseDown()
        {
            _loader.UpdateMapsets();
        }
    }
}
