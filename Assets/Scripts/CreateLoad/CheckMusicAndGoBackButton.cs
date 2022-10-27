using Assets.Scripts.Utilities;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.CreateLoad
{
    class CheckMusicAndGoBackButton : MonoBehaviour
    {
        [SerializeField] private Text _errorText;

        private void OnMouseDown()
        {
            if (IsContainsFiles())
            {
                FndlMusicAndBackground();
                SceneManager.LoadScene((int)Scenes.CreateMap);
            }
            else
            {
                _errorText.gameObject.SetActive(true);
            }
        }

        private bool IsContainsFiles()
        {
            bool containsMusic = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.mp3").Any(x => x.Extension == ".mp3");
            bool containsImage = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.jpg").Any(x => x.Extension == ".jpg");

            return containsMusic && containsImage;
        }

        private void FndlMusicAndBackground()
        {
            var files = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".mp3"))
                    Global.Map.General.AudioFilename = file.Name;

                if (file.Name.EndsWith(".jpg"))
                    Global.Map.Events.BackgroungImage = file.Name;
            }
        }
    }
}
