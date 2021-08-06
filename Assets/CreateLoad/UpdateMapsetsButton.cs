using UnityEngine;

namespace Assets.CreateLoad
{
    class UpdateMapsetsButton : MonoBehaviour
    {
        [SerializeField] private LoaderMapsets loader;
        void OnMouseDown()
        {
            loader.UpdateMapsets();
        }
    }
}
