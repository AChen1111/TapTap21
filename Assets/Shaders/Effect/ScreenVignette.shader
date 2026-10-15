Shader "Effects/ScreenVignette"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 0.41
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "ScreenVignette"
            ZWrite Off
            ZTest Always
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Intensity;

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = input.texcoord.xy;
                half4 color = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, uv, _BlitMipLevel);

                float vignette = 1.0 - length(uv - 0.5);
                vignette = lerp(1.0, saturate(vignette), saturate(_Intensity));
                color.rgb *= vignette;
                return color;
            }
            ENDHLSL
        }
    }
}
