using UnityEngine;

namespace Assets.Scripts.UI.MapEditor
{
    public class EditorCanvasSelector : MonoBehaviour
    {
        [SerializeField] private EditorCanvasSwitch.CanvasName canvas;

        public void Switch()
        {
            EditorCanvasSwitch.Instance.EnableCanvas(canvas);
        }
    }
}
