using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitObjects
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(PrinterNumber))]
    class OsuCircleDisplay : OsuHitObjectDisplay
    {
        protected bool _isMoving = false;
        private OsuCircle _circle;

        public OsuCircle Circle => _circle;

        void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(_circle.X, _circle.Y));
            GetComponent<PrinterNumber>().Print(_circle.ComboNumber);
            GetComponent<Image>().color = _circle.ComboColor;
        }

        void Update()
        {
            int razn = _circle.Time - Global.MusicTime;
            if (razn > Global.AR_ms || razn < 0)
            {
                Destroy(gameObject);
            }

            if (Input.touchCount == 0)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (_isMoving)
                    {
                        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector3 currentPosition = transform.parent.worldToLocalMatrix.MultiplyPoint(touchPosition);
                        currentPosition.z = 0;
                        transform.localPosition = currentPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    if (_isMoving)
                    {
                        Vector2 finalPosition = OsuMath.UnityCoordsToOsu(transform.localPosition);
                        _circle.SetCoords(finalPosition);
                        Destroy(gameObject);
                    }
                    break;
            }

        }

        void OnMouseDown()
        {
            _isMoving = true;
        }

        public override void Init(OsuHitObject hitObject)
        {
            if (hitObject is OsuCircle == false)
            {
                throw new Exception("Недопустимый тип.");
            }

            _hitObject = hitObject;
            _circle = hitObject as OsuCircle;
        }
    }
}
