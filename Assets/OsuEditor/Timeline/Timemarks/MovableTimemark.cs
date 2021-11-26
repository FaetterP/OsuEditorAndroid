using Assets.Elements;
using System;
using UnityEngine;

namespace Assets.OsuEditor.Timeline.Timemarks
{
    abstract class MovableTimemark : Timemark
    {
        private bool _isMoving;

        void OnMouseDown()
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
            Timemark[] marks = FindObjectsOfType<Timemark>();
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

            foreach (var t in FindObjectsOfType<OsuHitObject>())
            {
                Destroy(t.gameObject);
            }

            Global.Map.UpdateComboInfos();
            _creator.UpdateCircleMarks();
        }

        protected abstract void ApplyTime(int newTime);
    }
}
