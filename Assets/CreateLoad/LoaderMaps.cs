using UnityEngine;
using System.IO;

namespace Assets.CreateLoad
{
    class LoaderMaps : MonoBehaviour
    {
        [SerializeField] private ContentElementMap mapElement;

        void Start()
        {
            ClearContent();
        }

        public void UpdateMaps()
        {
            ClearContent();
            foreach (var t in new DirectoryInfo(Global.FullPathToMapFolder).GetFiles())
            {
                if (t.Name.EndsWith(".osu"))
                {
                    ContentElementMap created = Instantiate(mapElement, transform);
                    created.SetText(t.Name);
                }
            }
        }

        private void ClearContent()
        {
            foreach (var child in GetComponentsInChildren<ContentElementMap>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
