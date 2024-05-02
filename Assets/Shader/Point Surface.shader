Shader "Graph/Point Surface"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface ConfigureSurface Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

        float _Glossiness;

        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface)
        {
            surface.Albedo=input.worldPos*0.5+0.5;
            surface.Smoothness=_Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
