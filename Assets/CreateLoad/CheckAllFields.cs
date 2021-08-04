using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class CheckAllFields : MonoBehaviour
    {
        [SerializeField] InputField Artist, ArtistU, Title, TitleU;
        // [SerializeField] Text Err;
        [SerializeField] Canvas Disable, Enable;
        void OnMouseDown()
        {
            if (isAllFieldsFilled())
            {
                string folder = ArtistU.text + " - " + TitleU.text;
                var meta = Global.Map.Metadata;
                meta.Artist = Artist.text;
                meta.ArtistUnicode = ArtistU.text;
                meta.Title = Title.text;
                meta.TitleUnicode = TitleU.text;
                if (true)
                {
                    GetComponent<CreatorMapFolderButton>().CreateFolder(folder);
                    Disable.gameObject.SetActive(false);
                    Enable.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log(456);
               // Err.gameObject.SetActive(true);
            }
        }

        private bool isAllFieldsFilled()
        {
            return Artist.text != "" && ArtistU.text != "" && Title.text != "" && TitleU.text != "";
        }
    }
}
