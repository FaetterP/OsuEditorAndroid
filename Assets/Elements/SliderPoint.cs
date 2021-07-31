using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Elements
{
    class SliderPoint 
    {
        public double x = 0, y = 0;
        public bool isStatic = false;

        public SliderPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            isStatic = false;
        }
        public void SwitchStatic()
        {
            if (isStatic) { isStatic = false; }
            else { isStatic = true; }
        }
    }
}
