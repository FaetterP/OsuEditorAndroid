using Assets.Elements;
using Assets.OsuEditor.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.OsuEditor
{
    class SliderPointGameObject : MonoBehaviour, ICloneable
    {
        [SerializeField] Sprite StaticSprite, NotStaticSprite;
        public SliderPoint thisPoint;
        public OsuSlider thisSlider;
        private bool isMoving = false;
        void Start()
        {
            GetComponent<Image>().sprite = thisPoint.isStatic ? StaticSprite : NotStaticSprite;
        }
        void Update()
        {

            if (Global.SelectedHitObject != null && Global.SelectedHitObject.time == thisSlider.time)
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
                if (Global.SelectedHitObject.time != thisSlider.time) { return; }
            }
            catch { return; }
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (isMoving)
                        {
                            var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                            poss.z = 0;
                            transform.localPosition = poss;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (isMoving)
                        {
                            Vector2 pos = transform.localPosition;
                            pos = OsuMath.UnityCoordsToOsu(pos+ new Vector2(thisSlider.transform.localPosition.x, thisSlider.transform.localPosition.y));
                            thisPoint.x = pos.x;
                            thisPoint.y = pos.y;
                            isMoving = false;
                            (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateBezePoints();
                            (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateLength();
                            (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateTimeEnd();
                            thisSlider.RemoveFromScreen();
                            CreatorTimemarks.UpdateCircleMarks();
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
                isMoving = true;
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

                (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateBezePoints();
                (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateLength();
                (OsuMath.GetHitObjectFromTime(thisSlider.time) as OsuSlider).UpdateTimeEnd();
                thisSlider.RemoveFromScreen();
                CreatorTimemarks.UpdateCircleMarks();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
