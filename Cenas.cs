using UnityEngine;

namespace Synecdoche
{
	public static class Cenas
	{
		/// <summary>
		/// Returns a random number between [-1.0, 1.0].
		/// </summary>
		public static float valueSigned
		{
			get
                { return Random.value * 2f + -1f;
            }
		}

		static bool hasNextGaussian = false;
		static float nextNextGaussian = 0;
        /// <summary>
        /// Returns the next pseudorandom, Gaussian ("normally") distributed value with mean 0.0 and standard deviation 1.0 from this random number generator's sequence.
        /// <see cref="https://docs.oracle.com/javase/7/docs/api/java/util/Random.html"/>
        /// </summary>
        public static float nextGaussian
		{
            get
            {
                if (hasNextGaussian)
                {
                    hasNextGaussian = false;
                    return nextNextGaussian;
                }

                else
                {
                    float sm;
                    Vector2 gaussian = new Vector2();

                    do
                    {
                        gaussian.Set(valueSigned, valueSigned);
                        sm = gaussian.sqrMagnitude;
                    }
                    while (sm >= 1f || sm == 0f);

                    float multiplier = Mathf.Sqrt(-2f * Mathf.Log(sm) / sm);
                    gaussian *= multiplier;
                    nextNextGaussian = gaussian.y;
                    hasNextGaussian = true;
                    return gaussian.x;
                }
            }
			
		}

		/// <summary>
		/// Returns a linear Monte Carlo method.
		/// </summary>
		public static float monteCarlo
		{
            get
            {
                while (true)
                {
                    float v1 = Random.value;
                    float v2 = Random.value;
                    if (v2 < v1) return v1;
                }
            }
			
		}

		/// <summary>
		/// Returns a exponential MC method. Values tend to 0.
		/// </summary>    
		public static float MonteCarloPow(float power)
		{
			while (true)
            {
				float v1 = Mathf.Pow(Random.value, power);
				float v2 = Mathf.Pow(Random.value, power);
				if (v2 < v1 && v1 < 1f) return v1;
			}
		}
	}
}