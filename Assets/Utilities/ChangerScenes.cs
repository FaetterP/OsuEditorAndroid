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
        [SerializeField] private int number = 0;
        void OnMouseDown()
        {
            SceneManager.LoadScene(number);
        }
    }
}
