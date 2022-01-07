using Assets.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    [RequireComponent(typeof(Slider))]
    abstract class DifficultySlider : MonoBehaviour
    {
        [SerializeField] protected Text _text;
                         private Slider _thisSlider;

        void Awake()
        {
            _thisSlider = GetComponent<Slider>();
            _thisSlider.onValueChanged.AddListener(delegate { ChangeValue(); });
        }

        void Start()
        {
            _thisSlider.value = (float)GetValue();
        }

        protected virtual void ChangeValue()
        {
            _thisSlider.value = Mathf.Round(_thisSlider.value);
            //_text.text = _text.GetComponent<LangWriter>().GetText() + " - " + _thisSlider.value;
            SetValue(_thisSlider.value);
        }

        protected abstract void SetValue(double value);

        protected abstract double GetValue();
    }
}
