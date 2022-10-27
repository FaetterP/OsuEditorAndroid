using Assets.Scripts.OsuEditor.HitObjects;
using Assets.Scripts.OsuEditor.HitObjects.SliderStuff;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    class OsuMath
    {
        private static int comb(int n, int k)
        {
            if (k == 0) return 1;
            if (n == 0) return 0;
            return comb(n - 1, k - 1) + comb(n - 1, k);
        }

        public static List<Vector2> GetInterPointBeze(List<SliderPoint> points, int n)
        {
            List<Vector2> ret = new List<Vector2>();
            double h = 1.0 / n;

            for (int i = 0; i <= n; i++)
            {
                ret.Add(new Vector2());
                for (int i0 = 0; i0 < points.Count; i0++)
                {
                    var vec = ret[i];
                    vec.x += (float)(Math.Pow(1 - (i) * h, i0) * Math.Pow((i) * h, points.Count - 1 - i0) * comb(points.Count - 1, i0) * points[i0].x);
                    vec.y += (float)(Math.Pow(1 - (i) * h, i0) * Math.Pow((i) * h, points.Count - 1 - i0) * comb(points.Count - 1, i0) * points[i0].y);
                    ret[i] = vec;
                }
            }

            return ret;
        }

        public static Vector2 OsuCoordsToUnity(Vector2 osuCoords)
        {
            Vector2 ret = new Vector2();
            ret.x = (osuCoords.x * 3 / 2) - 384;
            ret.y = (osuCoords.y * 3 / -2) + 288;
            return ret;
        }

        public static Vector2 UnityCoordsToOsu(Vector2 unityCoords)
        {
            Vector2 ret = new Vector2();
            ret.x = (unityCoords.x + 384) * 2 / 3;
            ret.y = (unityCoords.y - 288) * 2 / -3;
            return ret;
        }

        public static int GetMarkX(int timestamp, int XLeft, int XRigth, int timeLeft, int timeRight)
        {
            double ret = XLeft + (XRigth - XLeft) * ((double)(timestamp - timeLeft) / (timeRight - timeLeft));
            return (int)ret;

        }

        public static string ConvertTimestampToSring(int timestamp)
        {
            string ret = "";
            ret += timestamp / 60000 + ":";
            ret += (timestamp % 60000) / 1000 + ":";
            if (((timestamp % 60000) % 1000).ToString().Length == 1) { ret += "00" + (timestamp % 60000) % 1000; }
            else if (((timestamp % 60000) % 1000).ToString().Length == 2) { ret += "0" + (timestamp % 60000) % 1000; }
            else { ret += (timestamp % 60000) % 1000; }
            return ret;
        }

        public static double GetLengthOfSlider(OsuSlider slider)
        {
            double ret = 0;
            for (int i = 0; i < slider.BezePoints.Count - 1; i++)
            {
                Vector2 vec1 = UnityCoordsToOsu(slider.BezePoints[i]);
                Vector2 vec2 = UnityCoordsToOsu(slider.BezePoints[i + 1]);
                ret += Math.Sqrt(Math.Pow(vec1.x - vec2.x, 2) + Math.Pow(vec1.y - vec2.y, 2));
            }
            return ret;
        }

        public static int GetNearestSliderSide(OsuSlider slider, Vector2 point)
        {
            List<Vector2> Centers = new List<Vector2>();
            for (int i = 0; i < slider.SliderPoints.Count - 1; i++)
            {
                double xsum = slider.SliderPoints[i].x + slider.SliderPoints[i + 1].x;
                double ysum = slider.SliderPoints[i].y + slider.SliderPoints[i + 1].y;
                Vector2 centre = new Vector2((float)(xsum / 2f), (float)(ysum / 2f));
                Centers.Add(centre);
            }

            int imin = 0, i0 = 0;
            double min = 0;
            foreach (var t in Centers)
            {
                if (Vector2.Distance(t, point) < min)
                {
                    min = Vector2.Distance(t, point);
                    imin = i0;
                }
                i0++;
            }
            return imin;
        }

        public static int ResizeValue(int startFrom, int endFrom, int startTo, int endTo, int value)
        {
            int lengthFrom = endFrom - startFrom;
            int lengthTo = endTo - startTo;

            double mult = 1.0 * lengthTo / lengthFrom;

            int newValue = (int)((value - startFrom) * mult);

            return newValue + startTo;
        }
    }
}
