using UnityEngine;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class TimemarkGO : MonoBehaviour
    {
        protected CreatorTimemarks _creator;
        protected int _time;
        protected Timemark _timemark;
        public int Time
        {
            get
            {
                return _time;
            }
        }

        void Awake()
        {
            _creator = FindObjectOfType<CreatorTimemarks>();
        }

        void Update()
        {
            UpdateX();
        }

        protected void UpdateX()
        {
            if (_timemark.IsRightTime())
            {
                int x = OsuMath.GetMarkX(_time, -500, 500, Global.MusicTime - Global.AR_ms, Global.MusicTime + Global.AR_ms);
                transform.localPosition = new Vector2(x, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            _creator.RemoveTimemarkFromScreen(_timemark);
        }

        void OnDisable()
        {
            Destroy(gameObject);
        }

        public virtual void Init(Timemark timemark)
        {
            _time = timemark.Time;
            _timemark = timemark;
        }
    }
}
