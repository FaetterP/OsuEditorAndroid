using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Assets.CreateLoad
{
    class LoaderMaps : MonoBehaviour
    {
        [SerializeField] private ContentElementMap mapElement;

        void Start()
        {
            DestroyChild();
        }

        public void UpdateMaps()
        {
            DestroyChild();
            foreach (var t in new DirectoryInfo(Global.FullPathToMapFolder).GetFiles())
            {
                if (t.Name.EndsWith(".osu"))
                {
                    ContentElementMap created = Instantiate(mapElement, transform);
                    created.SetText(t.Name);
                }
            }
        }

        private void DestroyChild()
        {
            foreach (var child in GetComponentsInChildren<ContentElementMap>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
