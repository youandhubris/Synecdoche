namespace Synecdoche.HLSL
{
    /// <summary>
    /// </summary>
    public enum NoiseSimplex
    {
        None,

        NoiseSimplex1D,
        NoiseSimplex2D,
        NoiseSimplex3D,
        NoiseSimplex4D,

        RidgedNoise1D,
        RidgedNoise2D,
        RidgedNoise3D,
        RidgedNoise4D,

        DerivateNoise1D,
        DerivateNoise2D,
        DerivateNoise3D,
        DerivateNoise4D,

        WorleyNoise2D,
        WorleyNoise3D,
        WorleyNoise2DF,
        WorleyNoise3DF,

        FlowNoise2D,
        FlowNoise3D,

        DerivativeFlowNoise2D,
        DerivativeFlowNoise3D,

        CurlNoise2D,
        CurlNoise2DT,
        CurlNoise2DO,
        CurlNoise3D,
        CurlNoise3DT,
        CurlNoise3DO,

        FBM1D,
        FBM2D,
        FBM3D,
        FBM4D,

        WorleyFBM2DO,
        WorleyFBM3DO,
        WorleyFBM2DF,
        WorleyFBM3DF,

        DerivativeFBM1D,
        DerivativeFBM2D,
        DerivativeFBM3D,
        DerivativeFBM4D,

        RidgedMF1D,
        RidgedMF2D,
        RidgedMF3D,
        RidgedMF4D,

        iqFBM2D,
        iqFBM3D,

        iqMatFBM2D
    }
}