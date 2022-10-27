using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(InputField))]
    class SpinnerLength : MonoBehaviour
    {
        private InputField _thisInput;

        private void Awake()
        {
            _thisInput = GetComponent<InputField>();
        }

        private void OnEnable()
        {
            OsuSpinner t = Global.SelectedHitObject as OsuSpinner;
            _thisInput.text = (t.TimeEnd - t.Time).ToString();
        }
    }
}
