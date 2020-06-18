#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
    
void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
   Direction = half3(0.5, 0.5, 0);
   Color = 1;
   DistanceAtten = 1;
   ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
   half4 clipPos = TransformWorldToHClip(WorldPos);
   half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out half DistanceAtten, out float ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
   Direction = half3(0.5, 0.5, 0);
   Color = 1;
   DistanceAtten = 1;
   ShadowAtten = 1;
#else
    #if SHADOWS_SCREEN
       half4 clipPos = TransformWorldToHClip(WorldPos);
       half4 shadowCoord = ComputeScreenPos(clipPos);
    #else
        half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    #endif
    
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
    
#endif
}

void Attentuation_float(float1 A, float1 B, float3 Color, out float3 Result)
{
    Result = A * B * Color;
}

void Attentuation_half(half1 A, half1 B, half3 Color, out half3 Result)
{
    Result = A * B * Color;
}

void DirectSpecular_float(float3 Specular, float Smoothness, float3 Direction, float3 Color, float3 WorldNormal, float3 WorldView, out float3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, float4(Specular, 0), Smoothness);
#endif
}

void DirectSpecular_half(half3 Specular, half Smoothness, half3 Direction, half3 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, half4(Specular, 0), Smoothness);
#endif
}


void AdditionalLights_float(float3 SpecColor, float Smoothness, float MaxBrightness,float3 WorldPosition, float3 WorldNormal, float3 WorldView, out float3 Diffuse, out float3 Specular)
{
    float3 diffuseColor = 0;
    float3 specularColor = 0;
    float3 diffuse = 0;
    float3 specular = 0;
    float3 directSpecular = 0;
    float3 specularOut = 0;
    
#ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        //float3 NdotL= saturate(dot(light.direction, WorldNormal));
        //float3 attenuation = mul(mul(light.shadowAttenuation, light.distanceAttenuation), SpecColor);
        //diffuse += mul(attenuation, NdotL);
        float3 lightPositionWS = _AdditionalLightsPosition[i].xyz;
        float3 lightDirection = float3(0, 0, 1);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        diffuseColor += LightingLambert(attenuatedLightColor, lightDirection, WorldNormal);
        specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecColor, 0), Smoothness);
        //DirectSpecular_float(SpecColor, Smoothness, light.direction, light.color, WorldNormal, WorldView, directSpecular);
        //specular += directSpecular;
    }
#endif
    Diffuse = diffuseColor > 0.9 ? 0.9 : diffuseColor;
    //Diffuse = diffuseColor;
    Specular = specularColor;
}
void AdditionalLights_half(half3 SpecColor, half Smoothness, half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Diffuse, out half3 Specular)
{
    half3 diffuseColor = 0;
    half3 specularColor = 0;
    half shadowAtten = 0;
    #ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    
    int pixelLightCount = GetAdditionalLightsCount();

    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        
        diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
        specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, half4(SpecColor, 0), Smoothness);
    }
    
    #endif
    Diffuse = diffuseColor > 0.7 ? 0.7 : diffuseColor;
    
    //Diffuse = diffuseColor;
    Specular = specularColor;
}

#endif