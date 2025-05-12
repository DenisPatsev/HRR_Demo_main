Shader "Custom/MaskShader"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        [HDR] _MaskColor ("Mask Color", Color) = (1,1,1,1)
        [HDR] _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MaskTex ("Mask tex", 2D) = "white" {}
        _MaskTransparency("Mask transparency", Range(0, 1)) = 1
        _Emission("Emission", Range(0, 10)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FresnelExponent("Fresnel exponent", Range(0, 4)) = 0
    }
    SubShader
    {
        Tags
        {
           "RenderType"="Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
        }
        LOD 200
         Cull Back
        ZTest LEqual
        ZWrite On

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade

        sampler2D _MainTex;
        sampler2D _MaskTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MaskTex;
            float3 worldNormal;
            float3 viewDir;
        };

        half _Glossiness, _Emission;
        half _Metallic, _MaskTransparency, _FresnelExponent;
        fixed4 _Color, _MaskColor, _FresnelColor;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 mask = tex2D(_MaskTex, IN.uv_MaskTex) * _MaskColor * _Emission;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            float fresnel = dot(IN.worldNormal, IN.viewDir);
            fresnel = saturate(1 - fresnel);
            fresnel = pow(fresnel, _FresnelExponent);
            float3 fresnelColor = fresnel * _FresnelColor;
            
            mask.a *= _MaskTransparency;
            c += mask;
            o.Albedo = c.rgb;
            o.Emission += fresnelColor;
            o.Alpha = _MaskTransparency;
        }
        ENDCG
    }
    FallBack "Diffuse"
}