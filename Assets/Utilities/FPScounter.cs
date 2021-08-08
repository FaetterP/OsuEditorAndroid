using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    [RequireComponent(typeof(Text))]
    class FPSCounter : MonoBehaviour
    {
        private Text thisText;

        void Awake()
        {
            thisText = GetComponent<Text>();
        }

        void Update()
        {
            thisText.text = "FPS:"+(int)(1.0f / Time.deltaTime); ;
        }
    }
}
