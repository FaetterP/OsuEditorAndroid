using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.CreateLoad
{
    class CheckMusicAndGoBackButton : MonoBehaviour
    {
        [SerializeField] private Text error;
        void OnMouseDown()
        {
            if (IsContainsFiles())
            {
                FndlMusicAndBackground();
                SceneManager.LoadScene(2);
            }
            else
            {
                error.gameObject.SetActive(true);
            }
        }

        private bool IsContainsFiles()
        {
            return new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.mp3").Any(x => x.Extension == ".mp3") && new DirectoryInfo(Global.FullPathToMapFolder).GetFiles("*.jpg").Any(x => x.Extension == ".jpg");
        }

        private void FndlMusicAndBackground()
        {
            var files = new DirectoryInfo(Global.FullPathToMapFolder).GetFiles();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".mp3")) { Global.Map.General.AudioFilename = file.Name; }
                if (file.Name.EndsWith(".jpg")) { Global.Map.Events.BackgroungImage = file.Name; }
            }
        }
    }
}
