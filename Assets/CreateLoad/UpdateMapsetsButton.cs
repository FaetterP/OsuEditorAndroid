using UnityEngine;

namespace Assets.CreateLoad
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
