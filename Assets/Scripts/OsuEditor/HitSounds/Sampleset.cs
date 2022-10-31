using Assets.Scripts.MapInfo.HitObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitSounds
{
    [RequireComponent(typeof(Dropdown))]
    class Sampleset : MonoBehaviour
    {
        private Dropdown _thisDropdown;

        private void Awake()
        {
            _thisDropdown = GetComponent<Dropdown>();
            _thisDropdown.onValueChanged.AddListener(delegate { UpdateSampleset(); });
        }

        private void OnEnable()
        {
            _thisDropdown.value = (Global.SelectedHitObject as OsuCircle).Sampleset;
        }

        private void UpdateSampleset()
        {
            (Global.SelectedHitObject as OsuCircle).Sampleset = _thisDropdown.value;
        }
    }
}
