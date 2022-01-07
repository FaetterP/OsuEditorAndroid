using Assets.Utilities.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OsuEditor.Settings.Difficulty
{
    [RequireComponent(typeof(Slider))]
    abstract class DifficultySlider : MonoBehaviour
    {
        [SerializeField] protected Text _text;
                         private Slider _thisSlider;
                         private LocalizedString _lang;

        void Awake()
        {
            _thisSlider = GetComponent<Slider>();
            _thisSlider.onValueChanged.AddListener(delegate { ChangeValue(); });
            _lang = new LocalizedString(GetKey());
        }

        void Start()
        {
            _thisSlider.value = (float)GetValue();
            _text.text = _lang.GetValue() + " - " + _thisSlider.value;
        }

        protected virtual void ChangeValue()
        {
            _thisSlider.value = Mathf.Round(_thisSlider.value);
            _text.text = _lang.GetValue() + " - " + _thisSlider.value;
            SetValue(_thisSlider.value);
        }

        protected abstract void SetValue(double value);

        protected abstract double GetValue();

        protected abstract string GetKey();
    }
}
