using System;
using System.Collections.Generic;
using UnityEngine;

namespace Synecdoche
{
    /// <summary>
    /// A collection of math related helpers.
    /// </summary>
	public struct Numero
    {
        #region Constants

        /// <summary>
        /// The number π, as originally defined as UnityEngine.
        /// </summary>
        public const float PI = 3.14159274f;

        /// <summary>
        /// Half of number π.
        /// </summary>
        public const float HALF_PI = PI / 2f;

        /// <summary>
        /// Third of number π.
        /// </summary>
        public const float THIRD_PI = PI / 3f;

        /// <summary>
        /// Quarter of number π.
        /// </summary>
        public const float QUARTER_PI = PI / 4f;

        /// <summary>
        /// Double the number π.
        /// </summary>
        public const float TWO_PI = PI * 2f;

        #endregion

        /// <summary>
        /// Return the normalized sine of angle f in radians.
        /// </summary>
        public static float SinNormalized(float f)
        {
            return (Mathf.Sin(f) + 1f) * 0.5f;
        }

        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        public static void Shuffle<T>(List<T> list)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				int index = UnityEngine.Random.Range(0, i);
				T temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
		}

		/// <summary>
		/// Given a list, gets the mean.
		/// </summary>
		public static float GetMean<T>(List<T> list)
		{
			double mean = 0f;

			for (int i = 0; i < list.Count; i++)
			{
				mean += Convert.ToDouble(list[i]);
			}

			mean /= list.Count;

			return (float) mean;
		}

        /// <summary>
        /// Given a list, gets the median.
        /// </summary>
        public static double GetMedian<T>(List<T> list)
		{
			double median = 0f;

			List <T> sortedList = list;
			sortedList.Sort();
            int sortedListCount = (int)sortedList.Count;

            if (sortedListCount % 2 == 0)
			{
				double midV1 = Convert.ToDouble(sortedList[sortedListCount / 2]);
				double midV2 = Convert.ToDouble(sortedList[sortedListCount / 2 + 1]);

				median = (midV1 + midV2) / 2d;
			}
			else median = Convert.ToDouble(sortedList[sortedListCount / 2]);

			return (float) median;
		}

        /// <summary>
        /// <see cref="http://floating-point-gui.de/errors/comparison/"/>
        /// </summary>
        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            bool isNearlyEqual = false;
            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            // shortcut, handles infinities
            if (a == b)
            {
                Debug.Log("(a == b)");
                isNearlyEqual = true;
            }

            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            else if (a == 0f || b == 0f || (absA + absB < float.Epsilon))
            {
                Debug.Log(diff + " < " + (epsilon * float.Epsilon));
                isNearlyEqual = diff < (epsilon * float.Epsilon);
            }

            // use relative error
            else
            {
                Debug.Log("else: " + diff / Mathf.Min((absA + absB), float.MaxValue) + " < " + epsilon);
                isNearlyEqual = diff / Mathf.Min((absA + absB), float.MaxValue) < epsilon;
            }
            
            return isNearlyEqual;
        }
    }
}