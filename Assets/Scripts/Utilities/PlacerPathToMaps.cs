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
