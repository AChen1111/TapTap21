#ifndef EFFECTS_SPRITE_INCLUDED
#define EFFECTS_SPRITE_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/Core2D.hlsl"
#include "EffectMath.hlsl"

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);
TEXTURE2D(_NoiseTex);
SAMPLER(sampler_NoiseTex);

CBUFFER_START(UnityPerMaterial)
    float4 _MainTex_ST;
    float4 _NoiseTex_ST;
    float4 _Color;
    float4 _FlashColor;
    float4 _DissolveEdgeColor;
    float _FlashAmount;
    float _DissolveAmount;
    float _DissolveEdgeWidth;
    float _NoiseScale;
    float _NoiseFromTexture;
    float _DistortAmount;
CBUFFER_END

struct Attributes
{
    float3 positionOS : POSITION;
    float4 color : COLOR;
    float2 uv : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float4 positionCS : SV_POSITION;
    float4 color : COLOR;
    float2 uv : TEXCOORD0;
};

Varyings vert(Attributes input)
{
    Varyings output = (Varyings)0;
    UNITY_SETUP_INSTANCE_ID(input);
    SetUpSpriteInstanceProperties();
    input.positionOS = UnityFlipSprite(input.positionOS, unity_SpriteProps.xy);
    output.positionCS = TransformObjectToHClip(input.positionOS);
    output.uv = input.uv;
    output.color = input.color * _Color * unity_SpriteColor;
    return output;
}

half4 frag(Varyings input) : SV_Target
{
    // 形变只偏移 UV。Sprite 通常只有四个顶点，改顶点会把整张图拉开。
    float time = _Time.y * 0.5;
    float2 noiseRG = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, input.uv * _NoiseScale + time * 0.1).rg;
    float2 uv = input.uv + EffectsDistortOffset(input.uv, _NoiseScale, _DistortAmount, _NoiseFromTexture, time, noiseRG);
    half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
    half4 color = tex * input.color;

    float noiseR = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, input.uv * _NoiseScale).r;
    float noise = EffectsNoise(input.uv, _NoiseScale, _NoiseFromTexture, noiseR);
    color = (half4)EffectsApply(color, noise, _DissolveAmount, _DissolveEdgeWidth, _DissolveEdgeColor.rgb, _FlashAmount, _FlashColor.rgb, 0);
    return color;
}

#endif
