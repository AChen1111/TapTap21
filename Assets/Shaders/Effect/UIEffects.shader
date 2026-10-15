Shader "Effects/UIEffects"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _FlashColor ("Flash Color", Color) = (1, 1, 1, 1)
        _FlashAmount ("Flash Amount", Range(0, 1)) = 0
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _DissolveEdgeWidth ("Dissolve Edge Width", Range(0, 0.5)) = 0.08
        _DissolveEdgeColor ("Dissolve Edge Color", Color) = (1, 0.45, 0.1, 1)
        _NoiseTex ("Noise", 2D) = "white" {}
        _NoiseScale ("Noise Scale", Float) = 4
        _NoiseFromTexture ("Noise From Texture", Float) = 0
        _DistortAmount ("Distort Amount", Range(0, 0.2)) = 0

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "EffectMath.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float2 positionXY : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _FlashColor;
                float4 _DissolveEdgeColor;
                float4 _MainTex_ST;
                float4 _NoiseTex_ST;
                float _FlashAmount;
                float _DissolveAmount;
                float _DissolveEdgeWidth;
                float _NoiseScale;
                float _NoiseFromTexture;
                float _DistortAmount;
            CBUFFER_END

            float4 _ClipRect;
            float4 _TextureSampleAdd;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.positionXY = input.positionOS.xy;
                output.uv = input.uv;
                output.color = input.color * _Color;
                return output;
            }

            float UIClip(float2 position, float4 clipRect)
            {
                float2 inside = step(clipRect.xy, position.xy) * step(position.xy, clipRect.zw);
                return inside.x * inside.y;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float time = _Time.y * 0.5;
                float2 noiseRG = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, input.uv * _NoiseScale + time * 0.1).rg;
                float2 uv = input.uv + EffectsDistortOffset(input.uv, _NoiseScale, _DistortAmount, _NoiseFromTexture, time, noiseRG);

                half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                half4 color = (tex + (half4)_TextureSampleAdd) * input.color;

                float noiseR = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, input.uv * _NoiseScale).r;
                float noise = EffectsNoise(input.uv, _NoiseScale, _NoiseFromTexture, noiseR);
                color = (half4)EffectsApply(color, noise, _DissolveAmount, _DissolveEdgeWidth, _DissolveEdgeColor.rgb, _FlashAmount, _FlashColor.rgb, 0);

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UIClip(input.positionXY, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
                #endif

                return color;
            }
            ENDHLSL
        }
    }
    Fallback "UI/Default"
}
