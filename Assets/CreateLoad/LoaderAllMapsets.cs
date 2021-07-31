using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Assets.CreateLoad
{
    class LoaderAllMapsets : MonoBehaviour
    {
        [SerializeField] private ContentElementMapset mapElement;

        void Start()
        {
            UpdateMapsets();
        }

        public void UpdateMapsets()
        {
            foreach (var child in GetComponentsInChildren<ContentElementMapset>())
            {
                Destroy(child.gameObject);
            }

            foreach(var t in new DirectoryInfo(Application.persistentDataPath).GetDirectories())
            {
                if (t.Name == "Unity") { continue; }
                ContentElementMapset created = Instantiate(mapElement, transform);
                created.SetText(t.Name);
            }
        }
    }
}
