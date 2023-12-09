using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CanvasSwitch : MonoBehaviour
    {
        [SerializeField] private int _startIndex;
        [SerializeField] private Canvas[] _canvases;

        public void EnableCanvas(int value)
        {
            foreach (Canvas canvas in _canvases)
            {
                canvas.gameObject.SetActive(false);
            }

            _canvases[value].gameObject.SetActive(true);
        }

        protected virtual int GetStartIndex()
        {
            return _startIndex;
        }

        private void Start()
        {
            EnableCanvas(GetStartIndex());
        }

        private void OnValidate()
        {
            if (_canvases.Length == 0)
            {
                _startIndex = 0;
                return;
            }
            if (_startIndex < 0)
            {
                Debug.LogWarning("Index must be non-negative.");
                _startIndex = 0;
            }

            if (_startIndex >= _canvases.Length)
            {
                Debug.LogWarning("Index out of bounds.");
                _startIndex = _canvases.Length - 1;
            }
        }
    }
}
