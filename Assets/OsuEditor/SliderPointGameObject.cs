using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class SliderPointGameObject : MonoBehaviour, ICloneable
    {
        [SerializeField] private Sprite _staticPointSprite, _notStaticPointSprite;
                         public SliderPoint thisPoint;
                         public OsuSlider thisSlider;
                         private bool _isMoving = false;
                         private CreatorTimemarks _creator;

        void Start()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
            GetComponent<Image>().sprite = thisPoint.IsStatic ? _staticPointSprite : _notStaticPointSprite;
        }

        void Update()
        {
            if (Global.SelectedHitObject != null && Global.SelectedHitObject.Time == thisSlider.Time)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
                thisSlider.GetComponent<LineRenderer>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
                thisSlider.GetComponent<LineRenderer>().enabled = false;
            }
            try
            {
                if (Global.SelectedHitObject.Time != thisSlider.Time) { return; }
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
                            pos = OsuMath.UnityCoordsToOsu(pos+ new Vector2(thisSlider.transform.localPosition.x, thisSlider.transform.localPosition.y));
                            thisPoint.x = pos.x;
                            thisPoint.y = pos.y;
                            _isMoving = false;
                            (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateBezePoints();
                            (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateLength();
                            (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateTimeEnd();
                            Destroy(thisSlider.gameObject);
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
                        if (thisSlider.SliderPoints.Count > 1)
                        {
                            thisSlider.SliderPoints.Remove(thisPoint);
                        }
                        break;
                    case SliderStatus.Switch:
                        thisPoint.SwitchStatic();
                        break;
                }

                (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateBezePoints();
                (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateLength();
                (Global.Map.GetHitObjectFromTime(thisSlider.Time) as OsuSlider).UpdateTimeEnd();
                Destroy(thisSlider.gameObject);
                _creator.UpdateCircleMarks();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
