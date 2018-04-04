using UnityEngine;
using System;

namespace Synecdoche
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <see cref="https://www.easyrgb.com/en/math.php"/>
    /// <see cref="http://www.deathbysoftware.com/colors/"/>
    /// </remarks>
    public static class Khroma
    {
        public enum DeltaEWeightType
        {
            Textile = 0,
            Graphic = 1
        }

        private struct DeltaEWeight
        {
            public float kL;
            public float kC;
            public float kH;

            public DeltaEWeight(float kL, float kC, float kH)
            {
                this.kL = kL;
                this.kC = kC;
                this.kH = kH;
            }
        }

        /// <summary>
        /// Representation of LCH & Alpha colors.
        /// </summary>
        /// <remarks>
        /// Color space similar to Lab cube color space, where instead of Cartesian coordinates a*, b*,
        /// the cylindrical coordinates C* (chroma, relative saturation) and h° (hue angle, angle of the hue in the CIELab color wheel) are specified.
        /// The CIELab lightness L* remains unchanged.
        /// </remarks>
        private struct LCh
        {
            public float l;
            public float c;
            public float h;
            public float alpha;

            public LCh(float lightness, float chroma, float hue) : this()
            {
                this.l = lightness;
                this.c = chroma;
                this.h = hue;
            }
        }

        #region Fields

        public static readonly Vector3 lumaWeight = new Vector3(0.2125f, 0.7154f, 0.0721f);

        #endregion

        #region Methods

        /// <summary>
        /// Returns a float, [0, 1], with weight RGB(0.2125f, 0.7154f, 0.0721f).
        /// </summary>
        public static float GetLuminance(ref Color color)
		{	
			Vector3 colorToVector3 = new Vector3(color.r, color.g, color.b);

            return Vector3.Dot(colorToVector3, lumaWeight);
		}

        #endregion

        #region Distance

        private static readonly DeltaEWeight weightGraphic = new DeltaEWeight(1f, 0.045f, 0.015f);
        private static readonly DeltaEWeight weightTextile = new DeltaEWeight(2f, 0.048f, 0.014f);

        /// <summary>
        /// Tips: 0.5f (really high contrast shapes, maybe 10+ bit monitors), 1f (high-quality monitors, indoors) 2.0f (else).
        /// </summary>
        public static float DeltaEmpfindung00(Lab lab1, Lab lab2, DeltaEWeightType weight = DeltaEWeightType.Graphic)
        {
            // Lab to LCh
            LCh lch1 = LabToLCh(lab1);
            LCh lch2 = LabToLCh(lab2);
            // Weight Factors
            DeltaEWeight weightFactors = Convert.ToBoolean((int)weight) ? weightGraphic : weightTextile;

            float CX = (lch1.c + lch2.c) / 2f;
            float GX = 0.5f * (1f - Mathf.Sqrt(Mathf.Pow(CX, 7f) / (Mathf.Pow(CX, 7f) + Mathf.Pow(25f, 7f))));

            float NN = (1f + GX) * lab1.A;
            lch1.c = Mathf.Sqrt(NN * NN + lab1.B * lab1.B);

            float hue1 = LabToHue(NN, lab1.B);
            NN = (1f + GX) * lab2.A;
            lch2.c = Mathf.Sqrt(NN * NN + lab2.B * lab2.B);

            float hue2 = LabToHue(NN, lab2.B);
            float deltaLightness = lch2.l - lch1.l;
            float deltaChroma = lch2.c - lch1.c;
            float deltaHue;
            if ((lch1.c * lch2.c) == 0f) deltaHue = 0f;
            else
            {
                NN = Mathf.Round((hue2 - hue1) * 1000f) / 1000f;
                if (Mathf.Abs(NN) <= 180f) deltaHue = hue2 - hue1;
                else
                {
                    if (NN > 180f) deltaHue = hue2 - hue1 - 360f;
                    else deltaHue = hue2 - hue1 + 360f;
                }
            }
            deltaHue = 2f * Mathf.Sqrt(lch1.c * lch2.c) * Mathf.Sin((deltaHue / 2f) * Mathf.Deg2Rad);

            float lightnessAverage = (lch1.l + lch2.l) / 2f;
            float chromaAverage = (lch1.c + lch2.c) / 2f;

            float hueNeutralsCompensation;

            if ((lch1.c * lch2.c) == 0f) hueNeutralsCompensation = hue1 + hue2;
            else
            {
                NN = Mathf.Abs(Mathf.Round((hue1 - hue2) * 1000f) / 1000f);
                if (NN > 180f)
                {
                    if ((hue2 + hue1) < 360f) hueNeutralsCompensation = hue1 + hue2 + 360f;
                    else hueNeutralsCompensation = hue1 + hue2 - 360f;
                }
                else hueNeutralsCompensation = hue1 + hue2;
                hueNeutralsCompensation /= 2f;
            }

            float T = 1f
                - 0.17f * Mathf.Cos((hueNeutralsCompensation - 30f) * Mathf.Deg2Rad)
                + 0.24f * Mathf.Cos((2f * hueNeutralsCompensation) * Mathf.Deg2Rad)
                + 0.32f * Mathf.Cos((3f * hueNeutralsCompensation + 6f) * Mathf.Deg2Rad)
                - 0.20f * Mathf.Cos((4f * hueNeutralsCompensation - 63f) * Mathf.Deg2Rad);

            // Compensations
            float lightnessCompensation = 1f + ((0.015f * ((lightnessAverage - 50f) * (lightnessAverage - 50f))) / Mathf.Sqrt(20f + ((lightnessAverage - 50f) * (lightnessAverage - 50f))));
            float chromaCompensation = 1f + weightFactors.kC * chromaAverage;
            float hueCompensation = 1f + weightFactors.kH * chromaAverage * T;
            // Rotation
            float PH = 30f * Mathf.Exp(-((hueNeutralsCompensation - 275f) / 25f) * ((hueNeutralsCompensation - 275f) / 25f));
            float RC = 2f * Mathf.Sqrt(Mathf.Pow(chromaAverage, 7f) / (Mathf.Pow(chromaAverage, 7f) + Mathf.Pow(25f, 7f)));
            float rotationTerm = -Mathf.Sin((2f * PH) * Mathf.Deg2Rad) * RC;
            // Weights Constants
            float weightFactorChroma = 1f;
            float weightFactorHue = 1f;
            // Corrections
            deltaLightness = deltaLightness / (weightFactors.kL * lightnessCompensation);
            deltaChroma = deltaChroma / (weightFactorChroma * chromaCompensation);
            deltaHue = deltaHue / (weightFactorHue * hueCompensation);

            float distance = Mathf.Sqrt(Mathf.Pow(deltaLightness, 2f) + Mathf.Pow(deltaChroma, 2f) + Mathf.Pow(deltaHue, 2f) + rotationTerm * deltaChroma * deltaHue);

            return distance;
        }

        #endregion

        #region Conversions

        private static readonly XYZ xyzWhiteRef = new XYZ(95.047f, 100.000f, 108.883f, 0f);

        public static XYZ RgbToXyz(Color rgb)
        {
			float r = rgb.r;
			float g = rgb.g;
			float b = rgb.b;

			r = (r > 0.04045f) ? Mathf.Pow((r + 0.055f) / 1.055f, 2.4f) : r / 12.92f;
			g = (g > 0.04045f) ? Mathf.Pow((g + 0.055f) / 1.055f, 2.4f) : g / 12.92f;
			b = (b > 0.04045f) ? Mathf.Pow((b + 0.055f) / 1.055f, 2.4f) : b / 12.92f;

			r *= 100f;
			g *= 100f;
			b *= 100f;

            XYZ xyz = new XYZ
            {
                X = r * 0.4124f + g * 0.3576f + b * 0.1805f,
                Y = r * 0.2126f + g * 0.7152f + b * 0.0722f,
                Z = r * 0.0193f + g * 0.1192f + b * 0.9505f,
                Alpha = rgb.a
            };
			
            return xyz;
		}

        public static Color XyzToRgb(XYZ xyz)
        {
            float x = xyz.X / 100f;
            float y = xyz.Y / 100f;
            float z = xyz.Z / 100f;

            float r = x * 3.2405843f + y * -1.5372f + z * -0.4986f;
            float g = x * -0.9689f + y * 1.8757459f + z * 0.0415f;
            float b = x * 0.0557f + y * -0.2040f + z * 1.0569855f;

            float p = 1f / 2.4f;

            r = (r > 0.0031308f) ? 1.055f * Mathf.Pow(r, p) - 0.055f : 12.92f * r;
            g = (g > 0.0031308f) ? 1.055f * Mathf.Pow(g, p) - 0.055f : 12.92f * g;
            b = (b > 0.0031308f) ? 1.055f * Mathf.Pow(b, p) - 0.055f : 12.92f * b;

            Color rgba = new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), Mathf.Clamp01(xyz.Alpha));

            return rgba;
        }

        public static Lab XyzToLab(XYZ xyz)
        {
			float x = xyz.X / xyzWhiteRef.X;
			float y = xyz.Y / xyzWhiteRef.Y;
			float z = xyz.Z / xyzWhiteRef.Z;

            float p = 1f / 3f;
            float f = 16f / 116f;

            x = (x > 0.008856f) ? Mathf.Pow(x, p) : (7.787f * x) + f;
            y = (y > 0.008856f) ? Mathf.Pow(y, p) : (7.787f * y) + f;
            z = (z > 0.008856f) ? Mathf.Pow(z, p) : (7.787f * z) + f;

			Lab lab = new Lab
            {
                L = (116f * y) - 16f,
                A = 500f * (x - y),
                B = 200f * (y - z),
                Alpha = xyz.Alpha
            };
			
			return lab;
		}

		public static XYZ LabToXyz(Lab lab)
        {
			float y = (lab.L + 16f) / 116f;
			float x = lab.A / 500f + y;
			float z = y - lab.B / 200f;

            float f = 16f / 116f;

            y = (Mathf.Pow(y , 3f) > 0.008856f) ? Mathf.Pow(y , 3f) : (y - f) / 7.787f;
            x = (Mathf.Pow(x , 3f) > 0.008856f) ? Mathf.Pow(x , 3f) : (x - f) / 7.787f;
            z = (Mathf.Pow(z , 3f) > 0.008856f) ? Mathf.Pow(z , 3f) : (z - f) / 7.787f;

            XYZ xyz = new XYZ
            {
                X = xyzWhiteRef.X * x,
                Y = xyzWhiteRef.Y * y,
                Z = xyzWhiteRef.Z * z,
                Alpha = lab.Alpha
            };

            return xyz;
		}

        public static Lab RgbToLab(Color rgba)
        {
            XYZ xyz = RgbToXyz(rgba);
            Lab lab = XyzToLab(xyz);

            return lab;
        }

        public static Color LabToRgb(Lab lab)
        {
            XYZ xyz = LabToXyz(lab);
            Color rgba = XyzToRgb(xyz);

            return rgba;
        }

        public static RYB RgbToRyb(Color rgba)
        {
            // Remove the white from the color
            float white = Mathf.Min(rgba.r, rgba.g, rgba.b);

            float r = rgba.r - white;
            float g = rgba.g - white;
            float b = rgba.b - white;

            float maxG = Mathf.Max(r, g, b);

            // Get the yellow out of the red+green

            float y = Mathf.Min(r, g);

            r -= y;
            g -= y;

            // If this unfortunate conversion combines blue and green, then cut each in half to
            // preserve the value's maximum range.
            if (b > 0 && g > 0)
            {
                b /= 2f;
                g /= 2f;
            }

            // Redistribute the remaining green.
            y += g;
            b += g;

            // Normalize to values.
            float maxY = Mathf.Max(r, y, b);

            if (maxY > 0)
            {
                float iN = maxG / maxY;

                r *= iN;
                y *= iN;
                b *= iN;
            }

            // Add the white back in.
            r += white;
            y += white;
            b += white;

            RYB ryb = new RYB(r, y, b, rgba.a);

            return ryb;
        }


        public static Color RybToRgb(RYB ryba)
        {
            // Remove the whiteness from the color.
            float white = Mathf.Min(ryba.R, ryba.Y, ryba.B);

            float r = ryba.R - white;
            float y = ryba.Y - white;
            float b = ryba.B - white;

            float maxY = Mathf.Max(r, y, b);

            // Get the green out of the yellow and blue
            float g = Mathf.Min(y, b);

            y -= g;
            b -= g;

            if (b > 0 && g > 0)
            {
                b *= 2f;
                g *= 2f;
            }

            // Redistribute the remaining yellow.
            r += y;
            g += y;

            // Normalize to values.
            float maxG = Mathf.Max(r, g, b);

            if (maxG > 0)
            {
                var iN = maxY / maxG;

                r *= iN;
                g *= iN;
                b *= iN;
            }

            // Add the white back in.
            r += white;
            g += white;
            b += white;

            Color rgb = new Color(r, g, b, ryba.Alpha);

            return rgb;
        }

        private static LCh LabToLCh(Lab lab)
        {
            LCh lch = new LCh();

            lch.l = lab.L;

            lch.c = Mathf.Sqrt(lab.A * lab.A + lab.B * lab.B);

            float h = Mathf.Rad2Deg * Mathf.Atan2(lab.B, lab.A);
            if (h < 0) h += 360f;
            else if (h >= 360) h -= 360f;
            lch.h = h;

            lch.alpha = lab.Alpha;

            return lch;
        }

        private static float LabToHue(float NN, float labB)
        {
            float bias = 0f;

            if (NN >= 0 && labB == 0) return 0f;
            if (NN < 0 && labB == 0) return 180f;
            if (NN == 0 && labB > 0) return 90f;
            if (NN == 0 && labB < 0) return 270f;
            if (NN > 0 && labB > 0) bias = 0f;
            if (NN < 0) bias = 180f;
            if (NN > 0 && labB < 0) bias = 360f;

            return (Mathf.Atan(labB / NN)) * Mathf.Deg2Rad + bias;
        }

        #endregion
    }
}