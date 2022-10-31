namespace Assets.Scripts.MapInfo.HitObjects
{
    class SliderPoint
    {
        public double x, y;
        public bool IsStatic;

        public SliderPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            IsStatic = false;
        }
        public void SwitchStatic()
        {
            if (IsStatic) { IsStatic = false; }
            else { IsStatic = true; }
        }

        public static bool operator ==(SliderPoint a, SliderPoint b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(SliderPoint a, SliderPoint b)
        {
            return !(a==b);
        }
    }
}
