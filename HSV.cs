using System;
using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public struct HSV
    {
        public HSV(float hue, float saturation, float value, float alpha) : this()
        {
            this.h = hue;
            this.s = saturation;
            this.v = value;
            this.alpha = alpha;
        }

        public HSV(Color color) : this()
        {
            float hue, saturation, value;

            Color.RGBToHSV(color, out hue, out saturation, out value);

            this.h = hue;
            this.s = saturation;
            this.v = value;
            this.alpha = color.a;
        }

        public float h;

        public float s;

        public float v;

        public float alpha;

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2}, {3})", this.h, this.s, this.v, this.alpha);
        }
    }
}