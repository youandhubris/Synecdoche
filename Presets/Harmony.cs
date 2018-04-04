using System;
using System.Collections.Generic;

using UnityEngine;

namespace Synecdoche.Presets
{
    [Serializable] 
    public class Harmony : ScriptableObject
    {
        private const float ONE_TWELFTH = 0.08333333f;

        public Dictionary<string, List<HSV>> harmonies = new Dictionary<string, List<HSV>>()
        {
            { "Analogous", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 0.5f,     0.05f,  0.1f, 1.0f),
                new HSV(ONE_TWELFTH,            0.05f,  0.0f, 1.0f),
                new HSV(ONE_TWELFTH * 11f,      0.05f,  0.0f, 1.0f),
                new HSV(ONE_TWELFTH * 11.5f,    0.05f,  0.1f, 1.0f)

                }
            },
            { "Complementary", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 6f, 0.025f, 0.025f, 1.0f)
                }
            },
            { "ComplementaryDoubleMinus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 5f,   0.025f, 0.05f,  1.0f),
                new HSV(ONE_TWELFTH * 6f,   0.05f,  0f,     1.0f),
                new HSV(ONE_TWELFTH * 11f,  0.025f, 0.1f,  1.0f)
                }
            },
            { "ComplementaryDoublePlus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH,        0.025f, 0.1f,  1.0f),
                new HSV(ONE_TWELFTH * 6f,   0.05f,  0f,     1.0f),
                new HSV(ONE_TWELFTH * 7f,   0.025f, 0.05f,  1.0f)
                }
            },
            { "ComplementarySlipt", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 5f, 0.05f, 0.1f, 1.0f),
                new HSV(ONE_TWELFTH * 7f, 0.05f, 0.1f, 1.0f)
                }
            },
            { "CompoundMinus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 7f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 7f, 0.20f, 0.25f, 1.0f),
                new HSV(ONE_TWELFTH * 11f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 11f, 0.20f, 0.25f, 1.0f)
                }
            },
            { "CompoundPlus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 2f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 2f, 0.20f, 0.25f, 1.0f),
                new HSV(ONE_TWELFTH * 5f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 5f, 0.20f, 0.25f, 1.0f)
                }
            },
            { "DiadMinus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 10f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 10f, 0.40f, 0.35f, 1.0f)
                }
            },
            { "DiadPlus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 2f, 0.05f, 0.05f, 1.0f),
                new HSV(ONE_TWELFTH * 2f, 0.40f, 0.35f, 1.0f)
                }
            },
            { "Mono", new List<HSV>()
                {
                new HSV(0f, 0.15f, 0.1f, 1.0f),
                new HSV(0f, 0.40f, 0.45f, 1.0f),
                }
            },
            { "RectangleMinus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 4f, 0.1f, 0.15f, 1.0f),
                new HSV(ONE_TWELFTH * 6f, 0.1f, 0.15f, 1.0f),
                new HSV(ONE_TWELFTH * 10f, 0.1f, 0.15f, 1.0f)
                }
            },
            { "RectanglePlus", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 2f, 0.1f, 0.15f, 1.0f),
                new HSV(ONE_TWELFTH * 6f, 0.1f, 0.15f, 1.0f),
                new HSV(ONE_TWELFTH * 8f, 0.1f, 0.15f, 1.0f)
                }
            },
            { "Shades", new List<HSV>()
                {
                new HSV(0.0f, 0f, 0.2f, 1.0f),
                new HSV(0.0f, 0f, 0.5f, 1.0f)
                }
            },
            { "Tetra", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 3f, 0.1f, 0.5f, 1.0f),
                new HSV(ONE_TWELFTH * 6f, 0.1f, 0.5f, 1.0f),
                new HSV(ONE_TWELFTH * 9f, 0.1f, 0.5f, 1.0f)
                }
            },
            { "Triad", new List<HSV>()
                {
                new HSV(ONE_TWELFTH * 4f, 0.1f, 0.5f, 1.0f),
                new HSV(ONE_TWELFTH * 8f, 0.1f, 0.5f, 1.0f)
                }
            }
        };
    }
}