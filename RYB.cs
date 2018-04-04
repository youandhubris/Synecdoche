using System;
using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// Alpha [0f, 1f].
    /// </remarks>
    public struct RYB
    {
        public RYB(float r, float y, float b, float alpha) : this()
        {
            this.R = r;
            this.Y = y;
            this.B = b;
            this.Alpha = alpha;
        }

        private float r;

        public float R
        {
            get { return r; }
            set { r = Mathf.Clamp01(value); }
        }

        private float y;

        public float Y
        {
            get { return y; }
            set { y = Mathf.Clamp01(value); }
        }

        private float b;

        public float B
        {
            get { return b; }
            set { b = Mathf.Clamp01(value); }
        }

        private float alpha;

        public float Alpha
        {
            get { return alpha; }
            set { alpha = Mathf.Clamp01(value); }
        }

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2}, {3})", this.r, this.y, this.b, this.alpha);
        }
    }
}