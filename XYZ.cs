using System;
using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// Representation of XYZ & Alpha colors.
    /// </summary>
    /// <remarks>
    /// Alpha [0f, 1f].
    /// </remarks>
    public struct XYZ
    {
        public XYZ(float x, float y, float z, float alpha) : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Alpha = alpha;
        }

        private float x;

        public float X
        {
            get { return x; }
            set { x = Mathf.Clamp(value, 0f, 100f); }
        }

        private float y;

        public float Y
        {
            get { return y; }
            set { y = Mathf.Clamp(value, 0f, 100f); }
        }

        private float z;

        public float Z
        {
            get { return z; }
            set { z = Mathf.Clamp(value, 0f, 100f); }
        }

        private float alpha;

        public float Alpha
        {
            get { return alpha; }
            set { alpha = Mathf.Clamp01(value); }
        }

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2}, {3})", this.x, this.y, this.z, this.alpha);
        }
    }
}