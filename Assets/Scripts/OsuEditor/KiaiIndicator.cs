using Assets.Scripts.MapInfo;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    [RequireComponent(typeof(Image))]
    class KiaiIndicator : MonoBehaviour
    {
        private Image _thisImage;

        void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        void Update()
        {
            _thisImage.enabled = IsKiaiNow();
        }

        private bool IsKiaiNow()
        {
            for (int i = 0; i < Global.Map.TimingPoints.Count - 1; i++)
            {
                TimingPoint leftPoint = Global.Map.TimingPoints[i];
                TimingPoint rightPoint = Global.Map.TimingPoints[i+1];

                if (Global.MusicTime >= leftPoint.Offset && Global.MusicTime <= rightPoint.Offset && leftPoint.Kiai)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
