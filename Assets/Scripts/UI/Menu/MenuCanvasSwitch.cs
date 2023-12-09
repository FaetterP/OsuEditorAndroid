using UnityEngine;

namespace Assets.Scripts.UI.MapEditor
{
    public class MenuCanvasSwitch : CanvasSwitch
    {
        public enum CanvasName { Main = 0, Settings, Creators }

        public static MenuCanvasSwitch Instance;

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
