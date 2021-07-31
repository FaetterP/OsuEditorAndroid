using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Utilities
{
    class ChangerScenes : IClickable
    {
        [SerializeField] private int number = 0;
        public override void Click()
        {
            SceneManager.LoadScene(number);
        }
    }
}
