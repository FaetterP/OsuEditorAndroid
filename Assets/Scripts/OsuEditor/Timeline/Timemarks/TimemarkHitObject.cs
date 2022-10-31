using Assets.Scripts.MapInfo.HitObjects;

namespace Assets.Scripts.OsuEditor.Timeline.Timemarks
{
    abstract class TimemarkHitObject : MovableTimemark
    {
        public abstract void Init(OsuHitObject hitObjecet);
    }
}
