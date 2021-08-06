using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class CreatorMapFolder : MonoBehaviour
    {
        [SerializeField] private InputField artist, artistUnicode, title, titleUnicode;
        [SerializeField] private Canvas disable, enable;
        void OnMouseDown()
        {
            if (IsAllFieldsFilled())
            {
                FillMetadata();

                string folderName = artistUnicode.text + " - " + titleUnicode.text;
                Global.FullPathToMapFolder = Application.persistentDataPath + "/" + folderName + "/";
                Directory.CreateDirectory(Global.FullPathToMapFolder);

                disable.gameObject.SetActive(false);
                enable.gameObject.SetActive(true);
            }
        }

        private bool IsAllFieldsFilled()
        {
            return artist.text != "" && artistUnicode.text != "" && title.text != "" && titleUnicode.text != "";
        }

        private void FillMetadata()
        {
            var metadata = Global.Map.Metadata;
            metadata.Artist = artist.text;
            metadata.ArtistUnicode = artistUnicode.text;
            metadata.Title = title.text;
            metadata.TitleUnicode = titleUnicode.text;
        }
    }
}
