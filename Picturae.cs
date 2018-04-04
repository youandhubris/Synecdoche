using UnityEngine;

namespace Synecdoche
{
	public static class Picturae
	{
		/// <summary>
		/// Returns the average color on a area of the texture. If Kernel Size == 1, Area == 9 pixels.
		/// </summary>
		public static Color GetKernel(ref Texture2D texture, Vector2 position, int kSize = 1, bool normalizeCoords = true, bool flipYAxis = true)
		{
			Vector4 kernelColor = new Vector4(0, 0, 0, 0);
			Vector2 normalizeShift = normalizeCoords ? new Vector2(1f / texture.width, 1f / texture.height) : new Vector2(texture.width, texture.height);
			Vector2 kernelOrigin = flipYAxis ? new Vector2(position.x, 1f - position.y) : position;
			float kValid = 0f;

			for (int x = -kSize; x < kSize + 1; x++)
			{
				for (int y = -kSize; y < kSize + 1; y++)
				{
					Vector2 kernelPosition = kernelOrigin + new Vector2(x * normalizeShift.x, y * normalizeShift.y);

					if (CheckKernel(kernelPosition))
					{
						kernelColor += (Vector4) texture.GetPixelBilinear(kernelPosition.x, kernelPosition.y);
						kValid++;
					}
				}
			}

			kernelColor /= kValid;

			return kernelColor;
		}

		/// <summary>
		/// Checks if inside texture on normalized coordinates.
		/// </summary>
		private static bool CheckKernel(Vector2 pos)
		{
            return (pos.x >= 0 && pos.x < 1 && pos.y >= 0 && pos.y < 1) ? true : false;
		}
	
	}
}