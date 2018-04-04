using System;
using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// Representation of LAB & Alpha colors.
    /// </summary>
    /// <remarks>
    /// Alpha [0f, 1f].
    /// Color space that describes mathematically all perceivable colors in the three dimensions.
    /// L for lightness and a and b for the color components green–red and blue–yellow.
    /// </remarks>
    public struct Lab
	{
        public Lab(float lightness, float greenRed, float blueYellow, float alpha) : this()
        {
            this.L = lightness;
            this.A = greenRed;
            this.B = blueYellow;
            this.Alpha = alpha;
        }

        private float l;

        public float L
        {
            get { return l; }
            set { l = Mathf.Clamp(value, 0f, 100f); }
        }

        private float a;

        public float A
        {
            get { return a; }
            set { a = Mathf.Clamp(value, -86.185f, 98.254f); }
        }

        private float b;

        public float B
        {
            get { return b; }
            set { b = Mathf.Clamp(value, -107.863f, 94.482f); }
        }

        private float alpha;

        public float Alpha
        {
            get { return alpha; }
            set { alpha = Mathf.Clamp01(value); }
        }

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2}, {3})", this.l, this.a, this.b, this.alpha);
        }
    }
}