using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utilities
{
    class ChangerScenes : MonoBehaviour
    {
        [SerializeField] private Scenes number;
        void OnMouseDown()
        {
            SceneManager.LoadScene((int)number);
        }
    }
}
