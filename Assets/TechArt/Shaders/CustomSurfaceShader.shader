Shader "Custom/CustomSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Specularity ("Specularity", Range(0,2)) = 0.0
        _SpecColor("Specular Color", Color) = (1, 1, 1,1)
        _Smoothness("Smoothness", Range(0, 1)) = 0
        _BumpMap("Normal Map", 2D) = "bump"{}
        _BumpScale("Normal Scale", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
//            "ForceNoShadowCasting" = "True"
        }
        LOD 200
        
        ZWrite On

        CGPROGRAM
      
        #pragma surface surf BlinnPhong 
        #pragma multi_compile_instancing

        sampler2D _MainTex, _MaskTex;
        sampler2D _BumpMap;
        float4 _MaskColor;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        half _Specularity;
        half _Smoothness;
        float _BumpScale;
        
        UNITY_INSTANCING_BUFFER_START(Props)
           UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)).rgb;
            normal.xy *= _BumpScale;
            normal = normalize(normal);
            o.Normal = normal;
            o.Specular = _Specularity;
            // o.Specular *= UNITY_L.rgb;
            o.Gloss = _Smoothness;
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}