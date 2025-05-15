Shader "Custom/MaskShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        [HDR] _MainColor("Main color", Color) = (1,1,1,1)
        _MaskTex ("Mask Texture", 2D) = "white" {}
        [HDR] _MaskColor("Mask Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue" = "AlphaTest"
        }
        LOD 100
        Cull Back
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
                return col;
            }
            ENDCG
        }

        pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MaskTex;
            float4 _MainTex_ST;
            float4 _MaskColor;
            float _Transparency;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MaskTex, i.uv) * _MaskColor;
                mask.a *= _Transparency;
                return mask;
            }
            ENDCG
        }
    }
}

