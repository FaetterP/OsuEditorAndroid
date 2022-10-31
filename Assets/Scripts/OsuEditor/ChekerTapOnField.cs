using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using UnityEngine;

namespace Assets.Scripts.OsuEditor
{
    class ChekerTapOnField : MonoBehaviour
    {
        [SerializeField] private CreatorTimemarks _creator;

        private void OnMouseDown()
        {
            Touch touch = Input.GetTouch(0);
            var pos = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
            pos = OsuMath.UnityCoordsToOsu(pos);

            switch (Global.LeftStatus)
            {
                case LeftStatus.Circle:
                    OsuCircle circle = new OsuCircle($"{pos.x},{pos.y},{Global.MusicTime},1,0,0:0:0:0:");
                    Global.Map.AddHitObject(circle);
                    break;

                case LeftStatus.Slider:
                    OsuSlider slider = new OsuSlider($"96,162,{Global.MusicTime},6,0,P|296:253|324:164,1,375");
                    Global.Map.AddHitObject(slider);
                    break;

                case LeftStatus.Spinner:
                    OsuSpinner spinner = new OsuSpinner($"256,192,{Global.MusicTime},8,12,{Global.MusicTime + 1000},0:0:0:0:");
                    Global.Map.AddHitObject(spinner);
                    break;
            }
            _creator.UpdateCircleMarks();
        }
    }
}
