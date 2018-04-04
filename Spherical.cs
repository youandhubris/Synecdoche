using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// Spherical coordinates (r, θ, φ) as often used in mathematics: radial distance r, azimuthal angle θ, and polar angle φ.
    /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
    /// </summary>
    /// <remarks>
    /// The meanings of θ and φ have been swapped compared to the physics convention.
    /// Implicitly converts to a Vector3 cartesian equivalent.
    /// </remarks>
    /// seeaslse
    public struct Spherical
    {
        #region Constructors

        /// <summary>
        /// Constructs a new Polar, given a Vector3 cartesian. Angle in radians.
        /// </summary>
        public Spherical(Vector3 cartesian) : this()
        {
            ToPolar(cartesian);
        }

        /// <summary>
        /// Constructs a new Polar, given a radius and azimuth. Angle in radians.
        /// </summary>
        public Spherical(float radius, float azimuth, float polar) : this()
    {
            this.radius = radius;
            this.azimuth = azimuth;
            this.polar = polar;
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
        /// Angle in radians.
        /// </summary>
        public float polar;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets new values for a given Polar.
        /// </summary>
        public void Set(Vector3 cartesian)
        {
            ToPolar(cartesian);
        }

        /// <summary>
        /// Sets new values for a given Vector3 cartesian.
        /// </summary>
        public void Set(float radius, float azimuth, float polar)
        {
            this.radius = radius;
            this.azimuth = azimuth;
            this.polar = polar;
        }

        #endregion

        #region Public Statics

        public static Spherical CartesianToPolar(Vector3 cartesian)
        {
            Spherical polar = new Spherical(cartesian);
            return polar;
        }

        public static Vector3 PolarToCartesian(float radius, float azimuth, float polar)
        {
            Spherical cartesian = new Spherical(radius, azimuth, polar);
            return cartesian;
        }

        #endregion

        #region Public Operators

        public static Spherical operator +(Spherical a, Spherical b)
        {
            return new Spherical(a.ToCartesian + b.ToCartesian);
        }

        public static Spherical operator -(Spherical a, Spherical b)
        {
            return new Spherical(a.ToCartesian - b.ToCartesian);
        }

        public static Spherical operator *(Spherical a, float d)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Spherical(new Vector3(cartesian.x * d, cartesian.y * d));
        }

        public static Spherical operator *(float d, Spherical a)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Spherical(new Vector3(cartesian.x * d, cartesian.y * d));
        }

        public static Spherical operator /(Spherical a, float d)
        {
            Vector3 cartesian = a.ToCartesian;
            return new Spherical(new Vector3(cartesian.x / d, cartesian.y / d));
        }

        public static bool operator ==(Spherical lhs, Spherical rhs)
        {
            return (lhs.ToCartesian == rhs.ToCartesian);
        }

        public static bool operator !=(Spherical lhs, Spherical rhs)
        {
            return !(lhs.ToCartesian == rhs.ToCartesian);
        }

        public static implicit operator Spherical(Vector3 cartesian)
        {
            return new Spherical(cartesian);
        }

        public static implicit operator Vector2(Spherical polar)
        {
            return polar.ToCartesian;
        }

        public static implicit operator Vector3(Spherical polar)
        {
            return polar.ToCartesian;
        }

        #endregion

        #region Private Methods

        private void ToPolar(Vector3 cartesian)
        {
            if (cartesian.x == 0f) cartesian.x = Mathf.Epsilon;

            this.radius = cartesian.magnitude;
            this.azimuth = Mathf.Asin(cartesian.y / this.radius);
            this.polar = Mathf.Atan(cartesian.z / cartesian.x);
        }

        private Vector3 ToCartesian
        {
            get
            {
                Vector3 cartesian;
                float radCosPolar = this.radius * Mathf.Cos(this.polar);

                cartesian.x = radCosPolar * Mathf.Cos(this.azimuth);
                cartesian.y = this.radius * Mathf.Sin(this.polar);
                cartesian.z = radCosPolar * - Mathf.Sin(this.azimuth);

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
            if (!(other is Spherical)) return false;
            
            Spherical polar = (Spherical)other;
            return this.radius.Equals(polar.radius) && this.azimuth.Equals(polar.azimuth) && this.azimuth.Equals(polar.polar);
        }

        public override string ToString()
        {
            return string.Format(GetType().Name + "({0}, {1}, {2})", this.radius, this.azimuth, this.polar);
        }

        #endregion
    }
}