using UnityEngine;

namespace Assets.Scripts.UI.MapEditor
{
    public class MenuCanvasSelector : MonoBehaviour
    {
        [SerializeField] private MenuCanvasSwitch.CanvasName canvas;

        public void Switch()
        {
            MenuCanvasSwitch.Instance.EnableCanvas(canvas);
        }
    }
}
