using Assets.Scripts.MapInfo.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Dropdown))]
    class Additions : MonoBehaviour
    {
        private Dropdown _thisDropdown;

        private void Awake()
        {
            _thisDropdown = GetComponent<Dropdown>();
            _thisDropdown.onValueChanged.AddListener(delegate { UpdateAdditions(); });
        }

        private void OnEnable()
        {
            _thisDropdown.value = (Global.SelectedHitObject as OsuCircle).Additions;
        }

        private void UpdateAdditions()
        {
            (Global.SelectedHitObject as OsuCircle).Additions = _thisDropdown.value;
        }
    }
}
