﻿using Assets.Scripts.MapInfo.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects.SliderStuff;
using Assets.Scripts.Utilities;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.HitObjects
{
    [RequireComponent(typeof(PrinterNumber), typeof(Image))]
    class OsuSliderDisplay : OsuCircleDisplay
    {
        [SerializeField] private SliderArrow _reverseArrow;
        [SerializeField] private LineRenderer _sliderPointsLineRenderer;
        [SerializeField] private LineRenderer _sliderLineRenderer;
        [SerializeField] private Image _lastCircle;

        private OsuSlider _slider;

        public new int Time => _slider.Time;

        public int SliderPointsCount => _slider.SliderPoints.Count;

        public OsuSlider Slider => _slider;

        private void Start()
        {
            gameObject.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2(_slider.X, _slider.Y));
            GetComponent<PrinterNumber>().Print(_slider.ComboNumber);
            GetComponent<Image>().color = _slider.ComboColor;
            _lastCircle.color = _slider.ComboColor;

            _slider.UpdatePrintedPoints();
            PrintSliderPoints();
            PrintSlider();
            PrintReverseArrow();
            Vector2 localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
            _lastCircle.transform.localPosition = OsuMath.OsuCoordsToUnity(_slider.PrintedPoints.Last()) - localPosition;
        }

        private void OnMouseDown()
        {
            _isMoving = true;
        }

        private void Update()
        {
            if (_slider.Time - Global.MusicTime > Global.AR_ms || _slider.TimeEnd - Global.MusicTime < 0)
            {
                Destroy(gameObject);
            }

            if (Input.touchCount > 0)
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
                            Vector2 finalPosition = OsuMath.UnityCoordsToOsu(transform.localPosition);
                            _slider.SetCoords(finalPosition);

                            for (int i = 0; i < _slider.SliderPoints.Count; i++)
                            {
                                _slider.SliderPoints[i].x += -_slider.X + finalPosition.x;
                                _slider.SliderPoints[i].y += -_slider.Y + finalPosition.y;
                            }

                            Destroy(gameObject);
                        }
                        break;
                }
            }
        }

        public void PrintSlider()
        {
            _sliderLineRenderer.positionCount = _slider.PrintedPoints.Count;
            _sliderLineRenderer.startColor = _slider.ComboColor;
            _sliderLineRenderer.endColor = _slider.ComboColor;

            for (int i = 0; i < _slider.PrintedPoints.Count; i++)
            {
                Vector2 vec = OsuMath.OsuCoordsToUnity(_slider.PrintedPoints[i]);
                vec -= new Vector2(transform.localPosition.x, transform.localPosition.y);
                _sliderLineRenderer.SetPosition(i, vec);
            }
        }

        private void PrintSliderPoints()
        {
            UpdateLine();
            GameObject go = Resources.Load("SliderPoint") as GameObject;
            SliderPointGameObject spgo = go.GetComponent<SliderPointGameObject>();
            foreach (var t in _slider.SliderPoints)
            {
                SliderPointGameObject created = Instantiate(spgo, transform);
                created.transform.SetAsLastSibling();
                created.transform.localPosition = OsuMath.OsuCoordsToUnity(new Vector2((float)t.x, (float)t.y)) - new Vector2(transform.localPosition.x, transform.localPosition.y);
                created._thisSliderDisplay = this;
                created.thisPoint = t;
            }
        }

        public void UpdateLine()
        {
            _sliderPointsLineRenderer.positionCount = _slider.SliderPoints.Count + 1;
            _sliderPointsLineRenderer.SetPosition(0, Vector2.zero);
            for (int i = 0; i < _slider.SliderPoints.Count; i++)
            {
                Vector3 position = OsuMath.OsuCoordsToUnity(new Vector2((float)_slider.SliderPoints[i].x, (float)_slider.SliderPoints[i].y));
                position -= transform.localPosition;
                position -= new Vector3(0, 0, 1);
                _sliderPointsLineRenderer.SetPosition(i + 1, position);
            }
        }

        public override void Init(OsuHitObject hitObject)
        {
            if (hitObject is OsuSlider == false)
            {
                throw new Exception("Недопустимый тип.");
            }

            _hitObject = hitObject;
            _slider = hitObject as OsuSlider;
        }

        public Vector2 GetCurrentPoint()
        {
            //int timeLength = _slider.TimeEnd - _slider.Time;
            //int time1 = timeLength / _slider.CountOfSlides;
            //int currentTime = Global.MusicTime - _slider.Time;

            //int currentSlide = currentTime / time1;
            //currentTime %= time1;
            //int index = 0;

            //if (currentSlide < 0)
            //{
            //    return new Vector2(1000, 1000);
            //}
            //else if (currentSlide % 2 == 0)
            //{
            //    index = OsuMath.ResizeValue(0, time1, 0, _slider.PrintedPoints.Count - 1, currentTime);
            //}
            //else
            //{
            //    index = _slider.PrintedPoints.Count - OsuMath.ResizeValue(0, time1, 0, _slider.PrintedPoints.Count, currentTime) - 1;
            //}

            //if (index < 0)
            //    return new Vector2(1000, 1000);

            //return _slider.PrintedPoints[index];
            if (Global.MusicTime < _slider.Time)
            {
                return new Vector2(1000,1000);
            }
            float need = OsuMath.GetMarkX(Global.MusicTime, 0, (int)_slider.Length, _slider.Time, _slider.TimeEnd);
            var points = _slider.PrintedPoints;
            for (int i = 0; i < points.Count - 1; i++)
            {
                float distance = Vector2.Distance(points[i], points[i + 1]);
                if (need - distance > 0)
                {
                    need -= distance;
                    continue;
                }
                Vector2 ret = points[i] + (points[i + 1] - points[i]) * (need / distance);
                return ret;

            }
            return points.Last();
        }

        private void PrintReverseArrow()
        {
            if (_slider.CountOfSlides > 1)
            {
                Vector2 vec = _slider.PrintedPoints[_slider.PrintedPoints.Count - 1] - _slider.PrintedPoints[_slider.PrintedPoints.Count - 2];
                int minus = Math.Sign(vec.y);
                float angle = 180 - minus * Vector2.Angle(vec, Vector2.right);

                var t = Instantiate(_reverseArrow, transform);
                t.transform.localPosition = OsuMath.OsuCoordsToUnity(_slider.PrintedPoints.Last()) - (Vector2)transform.localPosition;
                t.transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }

        public void UpdateBezier()
        {
            _slider.UpdatePrintedPoints();
            _slider.UpdateLength();
            _slider.UpdateTimeEnd(Global.Map);
        }

        public void EnableSliderPoints()
        {
            _sliderPointsLineRenderer.enabled = true;
        }

        public void DisableSliderPoints()
        {
            _sliderPointsLineRenderer.enabled = false;
        }
    }
}
