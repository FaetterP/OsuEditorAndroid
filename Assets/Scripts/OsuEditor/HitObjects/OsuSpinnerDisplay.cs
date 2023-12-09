using Assets.Scripts.MapInfo.HitObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitObjects
{
    [RequireComponent(typeof(Image))]
    class OsuSpinnerDisplay : OsuHitObjectDisplay
    {
        private Image _thisImage;

        private OsuSpinner _spinner;


        private void Awake()
        {
            _thisImage = GetComponent<Image>();
        }

        private void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(_spinner.X, _spinner.Y));
        }

        private void Update()
        {
            if (Global.MusicTime < _spinner.Time - Global.AR_ms)
            {
                Destroy(gameObject);
            }
            else if (Global.MusicTime < _spinner.Time)
            {
                transform.rotation = Quaternion.Euler(0, 0, _spinner.Time);
                _thisImage.color = new Color(1, 1, 1, 0.1f);
            }
            else if (Global.MusicTime > _spinner.Time && Global.MusicTime < _spinner.TimeEnd)
            {
                transform.rotation = Quaternion.Euler(0, 0, Global.MusicTime);
                _thisImage.color = new Color(1, 1, 1, 0.1f + 0.9f * ((Global.MusicTime - _spinner.Time * 1.0f) / (_spinner.TimeEnd - _spinner.Time)));
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void Init(OsuHitObject hitObject)
        {
            if (hitObject is OsuSpinner == false)
            {
                throw new Exception("Недопустимый тип.");
            }

            _hitObject = hitObject;
            _spinner = hitObject as OsuSpinner;
        }
    }
}
