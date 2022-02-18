using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Utilities
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
