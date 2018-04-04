using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// Polar coordinates (r, θ) as often used in mathematics: radial distance r, azimuthal angle θ.
    /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
    /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
    /// </summary>
    /// <remarks>
    /// Implicitly converts to a Vector2 or Vector3 (z=0) cartesian equivalent.
    /// </remarks>
	public struct Polar
    {
        #region Enums

        public enum BasePlane
        {
            XY,
            XZ
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Polar, given a Vector2 cartesian. Angle in radians.
        /// </summary>
        public Polar(Vector2 cartesian, BasePlane plane = BasePlane.XZ) : this()
        {
            Set(cartesian, plane);
        }

        /// <summary>
        /// Constructs a new Polar, given a Vector3 cartesian. Angle in radians.
        /// </summary>
        public Polar(Vector3 cartesian, BasePlane plane = BasePlane.XZ) : this()
        {
            Set(cartesian, plane);
        }

        /// <summary>
        /// Constructs a new Polar, given a radius and azimuth. Angle in radians.
        /// </summary>
        public Polar(float radius, float azimuth, float height = 0f, BasePlane plane = BasePlane.XZ) : this()
        {
            Set(radius, azimuth, height, plane);
        }

        #endregion

        #region Fields

        /// <summary>
        /// Distance from polar center.
        /// </summary>
        public float radius;

        /// <summary>
        /// Angle in radians.
        /// </summary>
        public float azimuth;

        /// <summary>
        /// Height, for cylindrical.
        /// </summary>
        public float height;

        /// <summary>
        /// </summary>
        private BasePlane plane;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets new values for a given Polar.
        /// </summary>
        public void Set(Vector2 cartesian, BasePlane plane = BasePlane.XZ)
        {
            this.plane = plane;
            ToPolar(cartesian);
            this.height = 0f;
        }

        /// <summary>
        /// Sets new values for a given Polar.
        /// </summary>
        public void Set(Vector3 cartesian, BasePlane plane = BasePlane.XZ)
        {
            this.plane = plane;
            ToPolar(cartesian);
            this.height = (plane == BasePlane.XY) ? cartesian.z : cartesian.y;
        }

        /// <summary>
        /// Sets new values for a given Vector2 cartesian.
        /// </summary>
        public void Set(float radius, float azimuth, float height, BasePlane plane = BasePlane.XZ)
        {
            this.radius = radius;
            this.azimuth = azimuth;
            this.height = height;
            this.plane = plane;
        }

        #endregion

        #region Public Statics

        public static Polar CartesianToPolar(Vector2 cartesian, BasePlane plane = BasePlane.XZ)
        {
            Polar polar = new Polar(cartesian, plane);
            return polar;
        }

        public static Polar CartesianToPolar(Vector3 cartesian, BasePlane plane = BasePlane.XZ)
        {
            Polar polar = new Polar(cartesian, plane);
            return polar;
        }

        public static Vector2 PolarToCartesian(float radius, float azimuth, float height = 0f, BasePlane plane = BasePlane.XZ)
        {
            Polar cartesian = new Polar(radius, azimuth, height, plane);
            return cartesian;
        }

        #endregion

        #region Public Operators

        public static Polar operator +(Polar a, Polar b)
        {
            return new Polar(a.ToCartesian + b.ToCartesian);
        }

        public static Polar operator -(Polar a, Polar b)
        {
            return new Polar(a.ToCartesian - b.ToCartesian);
        }

        public static Polar operator *(Polar a, float d)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Polar(new Vector3(cartesian.x * d, cartesian.y * d, cartesian.z * d));
        }

        public static Polar operator *(float d, Polar a)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Polar(new Vector3(cartesian.x * d, cartesian.y * d, cartesian.z * d));
        }

        public static Polar operator /(Polar a, float d)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Polar(new Vector3(cartesian.x / d, cartesian.y / d, cartesian.z / d));
        }

        public static bool operator ==(Polar lhs, Polar rhs)
        {
            return (lhs.ToCartesian == rhs.ToCartesian);
        }

        public static bool operator !=(Polar lhs, Polar rhs)
        {
            return !(lhs.ToCartesian == rhs.ToCartesian);
        }

        public static implicit operator Polar(Vector2 cartesian)
        {
            return new Polar(cartesian);
        }

        public static implicit operator Polar(Vector3 cartesian)
        {
            return new Polar(cartesian);
        }

        public static implicit operator Vector2(Polar polar)
        {
            return polar.ToCartesian;
        }

        public static implicit operator Vector3(Polar polar)
        {
            return polar.ToCartesian;
        }

        #endregion

        #region Private Methods

        private void ToPolar(Vector3 cartesian)
        {
            this.radius = cartesian.magnitude;
            if (cartesian.x == 0f) this.azimuth = 0f;
            else
            {
                this.azimuth = (plane == BasePlane.XY) ? Mathf.Atan2(cartesian.y, cartesian.x) : - Mathf.Atan2(cartesian.z, cartesian.x);
                if (this.azimuth < 0f) this.azimuth = Numero.TWO_PI + this.azimuth;
            }
        }

        private Vector3 ToCartesian
        {
            get
            {
                Vector3 cartesian;
                cartesian.x = this.radius * Mathf.Cos(this.azimuth);
                cartesian.y = (plane == BasePlane.XY) ? this.radius * Mathf.Sin(this.azimuth) : this.height;
                cartesian.z = (plane == BasePlane.XY) ? this.height : this.radius * - Mathf.Sin(this.azimuth);
                return cartesian;
            }
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return this.radius.GetHashCode() ^ this.azimuth.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Polar)) return false;
            
            Polar polar = (Polar)other;
            return this.radius.Equals(polar.radius) && this.azimuth.Equals(polar.azimuth) && this.azimuth.Equals(polar.height);
        }

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2})", this.radius, this.azimuth, this.height);
        }

        #endregion
    }
}