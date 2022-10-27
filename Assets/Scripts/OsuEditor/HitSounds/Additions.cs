using Assets.Scripts.OsuEditor.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Dropdown))]
    class Additions : MonoBehaviour
    {
        private Dropdown _thisDropdown;

        void Awake()
        {
            _thisDropdown = GetComponent<Dropdown>();
            _thisDropdown.onValueChanged.AddListener(delegate { UpdateAdditions(); });
        }

        void OnEnable()
        {
            _thisDropdown.value = (Global.SelectedHitObject as OsuCircle).Additions;
        }

        private void UpdateAdditions()
        {
            (Global.SelectedHitObject as OsuCircle).Additions = _thisDropdown.value;
        }
    }
}
