using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hubris
{
    /// <summary>
    /// </summary>
	public class Paleta
    {
        #region Constructors

        public Paleta()
        {
            presetHarmonies = ScriptableObject.CreateInstance<Presets.Harmony>().harmonies;
            colors = new List<Color>();
            this.wheelModel = WheelModel.RYB;
        }

        public Paleta(WheelModel wheelModel) : this()
        {
            this.wheelModel = wheelModel;
        }

        #endregion

        #region Enum

        public enum WheelModel
        {
            RGB,
            RYB
        }

        public enum Harmony
        {
            Analogous,
            Complementary,
            ComplementarySlipt,
            ComplementaryDoubleMinus,
            ComplementaryDoublePlus,
            CompoundMinus,
            CompoundPlus,
            DiadMinus,
            DiadPlus,
            Mono,
            RectangleMinus,
            RectanglePlus,
            Shades,
            Tetra,
            Triad
        }

        #endregion

        #region Fields

        private WheelModel wheelModel;

        private Dictionary<string, List<HSV>> presetHarmonies;

        public List<Color> colors;

        #endregion

        #region Public Methods

        public void AddHarmony(Color baseColor, Harmony harmony)
        {
            colors.Add(baseColor);
            if (wheelModel == WheelModel.RYB) baseColor = RybAsUColor(baseColor);

            

            HSV baseHSV = new HSV(baseColor);
            List<HSV> colorTransforms = presetHarmonies[harmony.ToString()];

            // Debug.Log("base: " + baseHSV.h + " / " + baseHSV.s + " / " + baseHSV.v);


            for (int i = 0; i < colorTransforms.Count; i++)
            {
                HSV tempHSV = baseHSV;

                tempHSV.h += colorTransforms[i].h;
                tempHSV.h = Mathf.Repeat(tempHSV.h, 1f);

                tempHSV.s += colorTransforms[i].s;
                if (tempHSV.s > 1f || tempHSV.s < 0f) tempHSV.s = Mathf.PingPong(tempHSV.s, 1f);

                tempHSV.v += colorTransforms[i].v;
                if (tempHSV.v > 1f || tempHSV.v < 0f) tempHSV.v = Mathf.PingPong(tempHSV.v, 1f);

                // Debug.Log(tempHSV.h + " / " + tempHSV.s + " / " + tempHSV.v);

                Color outColor = Color.HSVToRGB(tempHSV.h, tempHSV.s, tempHSV.v);
                if (wheelModel == WheelModel.RYB) outColor = Khroma.RybToRgb(RybAsUColorReverse(outColor));

                colors.Add(outColor);
            }

        }

        #endregion

        #region Public Operators

        #endregion

        #region Private Methods

        private Color RybAsUColor(Color color)
        {
            RYB rybColor = Khroma.RgbToRyb(color);
            Color rybAsColor = new Color(rybColor.R, rybColor.Y, rybColor.B, rybColor.Alpha);
            return rybAsColor; 
        }

        private RYB RybAsUColorReverse(Color rgb)
        {
            RYB color = new RYB(rgb.r, rgb.g, rgb.b, rgb.a);
            return color;
        }

        #endregion

        #region Overrides

        #endregion
    }
}