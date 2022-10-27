using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    class PlacerPathToMaps : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Text>().text = Application.persistentDataPath;
        }
    }
}
