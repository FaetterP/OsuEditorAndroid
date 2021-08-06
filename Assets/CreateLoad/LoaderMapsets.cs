using UnityEngine;
using System.IO;

namespace Assets.CreateLoad
{
    class LoaderMapsets : MonoBehaviour
    {
        [SerializeField] private ContentElementMapset mapElement;

        void Start()
        {
            UpdateMapsets();
        }

        public void UpdateMapsets()
        {
            ClearContent();

            foreach(var t in new DirectoryInfo(Application.persistentDataPath).GetDirectories())
            {
                if (t.Name == "Unity") { continue; }
                ContentElementMapset created = Instantiate(mapElement, transform);
                created.SetText(t.Name);
            }
        }

        private void ClearContent()
        {
            foreach (var child in GetComponentsInChildren<ContentElementMapset>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
