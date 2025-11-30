Shader "Unlit/Diffuse"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (0,0,0,0)
        _Smoothness ("Smoothness", Float) = 1
        _TimeValue ("Time Value", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            float _Smoothness, _TimeValue;
            float4 _Color, _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                i.normal = normalize(i.normal);
                float dotproduct = dot(float3(0,1,0), i.normal);
                float sinIt = sin(_TimeValue * _Time.y + dotproduct);
                float3 lerpIt = lerp(_Color, _Color2, sinIt);

                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfvector = normalize(viewDirection + float3(0,1,0));
                float spec = pow(saturate(dot(i.normal, halfvector)), _Smoothness * 1000);
                float3 final = spec + lerpIt;
                return float4(final, 1);
            }
            ENDCG
        }
    }
}
