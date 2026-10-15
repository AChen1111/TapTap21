Shader "Effects/SpriteEffects"
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
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "IgnoreProjector" = "True"
            "CanUseSpriteAtlas" = "True"
        }

        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "SpriteEffects2D"
            Tags { "LightMode" = "Universal2D" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "SpriteEffects.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "SpriteEffectsForward"
            Tags { "LightMode" = "UniversalForward" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "SpriteEffects.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "SpriteEffectsUnlit"
            Tags { "LightMode" = "SRPDefaultUnlit" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "SpriteEffects.hlsl"
            ENDHLSL
        }
    }
}
