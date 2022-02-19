using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CreateLoad
{
    class CreatorMapFolder : MonoBehaviour
    {
        [SerializeField] private InputField _artist, _artistUnicode, _title, _titleUnicode;
        [SerializeField] private Canvas _disabledCanvas, _enabledCanvas;

        private void OnMouseDown()
        {
            if (IsAllFieldsFilled())
            {
                FillMetadata();

                string folderName = _artistUnicode.text + " - " + _titleUnicode.text;
                Global.FullPathToMapFolder = Application.persistentDataPath + "/" + folderName + "/";
                Directory.CreateDirectory(Global.FullPathToMapFolder);

                _disabledCanvas.gameObject.SetActive(false);
                _enabledCanvas.gameObject.SetActive(true);
            }
        }

        private bool IsAllFieldsFilled()
        {
            return _artist.text != "" && _artistUnicode.text != "" && _title.text != "" && _titleUnicode.text != "";
        }

        private void FillMetadata()
        {
            var metadata = Global.Map.Metadata;

            metadata.Artist = _artist.text;
            metadata.ArtistUnicode = _artistUnicode.text;
            metadata.Title = _title.text;
            metadata.TitleUnicode = _titleUnicode.text;
        }
    }
}
