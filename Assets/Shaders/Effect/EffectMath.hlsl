#ifndef EFFECTS_MATH_INCLUDED
#define EFFECTS_MATH_INCLUDED

float EffectsHash21(float2 p)
{
    p = frac(p * float2(123.34, 345.45));
    p += dot(p, p + 34.345);
    return frac(p.x * p.y);
}

float EffectsValueNoise(float2 p)
{
    float2 i = floor(p);
    float2 f = frac(p);
    float a = EffectsHash21(i);
    float b = EffectsHash21(i + float2(1, 0));
    float c = EffectsHash21(i + float2(0, 1));
    float d = EffectsHash21(i + float2(1, 1));
    float2 u = f * f * (3.0 - 2.0 * f);
    return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
}

float EffectsNoise(float2 uv, float noiseScale, float noiseFromTexture, float noiseTexR)
{
    float proc = EffectsValueNoise(uv * noiseScale);
    return lerp(proc, noiseTexR, saturate(noiseFromTexture));
}

float2 EffectsDistortOffset(float2 uv, float noiseScale, float distortAmount, float noiseFromTexture, float time, float2 noiseRG)
{
    float2 cell = uv * noiseScale;
    float2 offset = float2(
        EffectsValueNoise(cell + time),
        EffectsValueNoise(cell + float2(5.2, 1.3) + time)) - 0.5;
    if (noiseFromTexture > 0.5)
        offset = noiseRG - 0.5;
    return offset * distortAmount;
}

float4 EffectsApply(float4 color, float noise, float dissolveAmount, 
    float edgeWidth, float3 edgeColor, float flashAmount, float3 flashColor, float premultiplied)
{
    float amount = saturate(dissolveAmount);
    if (amount > 0.001)
    {
        clip(noise - amount);
        float edgeEnd = amount + max(edgeWidth, 0.0001);
        float edge = 1.0 - smoothstep(amount, edgeEnd, noise);
        float3 edgeRgb = premultiplied > 0.5 ? edgeColor * color.a : edgeColor;
        color.rgb = lerp(color.rgb, edgeRgb, edge);
    }

    float3 flashRgb = premultiplied > 0.5 ? flashColor * color.a : flashColor;
    color.rgb = lerp(color.rgb, flashRgb, saturate(flashAmount));
    return color;
}

#endif