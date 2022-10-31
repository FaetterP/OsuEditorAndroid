using Assets.Scripts.OsuEditor.HitObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class MovableTimemark : Timemark
    {
        private bool _isMoving;

        void OnMouseDown()
        {
            CheckMove();
        }

        protected void CheckMove()
        {
            if (Global.LeftStatus == LeftStatus.Move)
            {
                _isMoving = true;
            }
        }

        void Update()
        {
            if (Input.touchCount > 0 && _isMoving)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:

                        var poss = transform.parent.worldToLocalMatrix.MultiplyPoint(Camera.main.ScreenToWorldPoint(touch.position));
                        poss.z = 0;
                        transform.localPosition = poss;
                        break;

                    case TouchPhase.Ended:
                        UpdateTime();
                        _isMoving = false;
                        break;
                }
            }
            else
            {
                UpdateX();
            }
        }

        private void UpdateTime()
        {
            TimemarkLine[] marks = FindObjectsOfType<TimemarkLine>();
            double dist = 1000000;
            int newTime = 0;

            foreach (var t in marks)
            {
                double newdist = Math.Abs(t.transform.localPosition.x - transform.localPosition.x);
                if (newdist < dist)
                {
                    dist = newdist;
                    newTime = t.Time;
                }
            }

            ApplyTime(newTime);

            foreach (var t in FindObjectsOfType<OsuHitObjectDisplay>())
            {
                Destroy(t.gameObject);
            }

            Global.Map.UpdateComboInfos();
            _creator.UpdateCircleMarks();
        }

        protected abstract void ApplyTime(int newTime);
    }
}
