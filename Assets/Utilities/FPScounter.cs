using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class FPScounter : MonoBehaviour
    {
        Text thisText;

        void Awake()
        {
            thisText = GetComponent<Text>();
        }

        void Update()
        {
            thisText.text = ((int)(1.0f / Time.deltaTime)).ToString(); ;
        }
    }
}
