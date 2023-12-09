using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class SliderPointsChangerButton : MonoBehaviour
    {
        [SerializeField] private SliderStatus SliderStatus;
        private bool isHold = false;
        void OnMouseDown()
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            isHold = true;
            Global.SliderStatus = SliderStatus;
        }

        void Update()
        {
            if (isHold && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    Global.SliderStatus = null;
                    GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
            }
        }
    }
}
