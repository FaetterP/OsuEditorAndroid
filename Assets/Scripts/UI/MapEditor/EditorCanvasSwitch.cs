using UnityEngine;

namespace Assets.Scripts.UI.MapEditor
{
    public class EditorCanvasSwitch : CanvasSwitch
    {
        public enum CanvasName { Main = 0, Settings, Metadata, Timing, AddTimingPoint, Exit, AIMode }

        public static EditorCanvasSwitch Instance;

        [SerializeField] private CanvasName _startCanvas;

        public void EnableCanvas(CanvasName name)
        {
            EnableCanvas((int)name);
        }

        protected override int GetStartIndex()
        {
            return (int)_startCanvas;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}
