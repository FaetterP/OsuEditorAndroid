using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.Timeline;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor
{
    class SliderPointGameObject : MonoBehaviour, ICloneable
    {
        [SerializeField] private Sprite _staticPointSprite, _notStaticPointSprite;
                         public SliderPoint thisPoint;
                         public OsuSliderDisplay _thisSliderDisplay;
                         private bool _isMoving = false;
                         private CreatorTimemarks _creator;

        private void Start()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            GetComponent<Image>().sprite = thisPoint.IsStatic ? _staticPointSprite : _notStaticPointSprite;
        }

        private void Update()
        {
            if (Global.SelectedHitObject != null && Global.SelectedHitObject.Time == _thisSliderDisplay.Time)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _thisSliderDisplay.EnableSliderPoints(); ;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _thisSliderDisplay.DisableSliderPoints();
            }
            try
            {
                if (Global.SelectedHitObject.Time != _thisSliderDisplay.Time) { return; }
            }
            catch { return; }
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (_isMoving)
                        {
                            var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                            poss.z = 0;
                            transform.localPosition = poss;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (_isMoving)
                        {
                            Vector2 pos = transform.localPosition;
                            pos = OsuMath.UnityCoordsToOsu(pos+ new Vector2(_thisSliderDisplay.transform.localPosition.x, _thisSliderDisplay.transform.localPosition.y));
                            thisPoint.x = pos.x;
                            thisPoint.y = pos.y;
                            _isMoving = false;

                            _thisSliderDisplay.UpdateBezier();

                            Destroy(_thisSliderDisplay.gameObject);
                            _creator.UpdateCircleMarks();
                        }
                        break;
                }
            }
        }
        void Awake()
        {
            gameObject.AddComponent<EventTrigger>();
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((a) => Click());
            trigger.triggers.Add(entry);
        }

        private void Click()
        {
            if (Input.touchCount == 1)
            {
                _isMoving = true;
            }
            if (Input.touchCount == 2)
            {
                switch (Global.SliderStatus)
                {
                    case SliderStatus.Remove:
                        if (_thisSliderDisplay.Slider.SliderPoints.Count > 1)
                        {
                            _thisSliderDisplay.Slider.RemoveSliderPoint(thisPoint);
                        }
                        break;
                    case SliderStatus.Switch:
                        thisPoint.SwitchStatic();
                        break;
                }

                _thisSliderDisplay.UpdateBezier();
                Destroy(_thisSliderDisplay.gameObject);
                _creator.UpdateCircleMarks();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
