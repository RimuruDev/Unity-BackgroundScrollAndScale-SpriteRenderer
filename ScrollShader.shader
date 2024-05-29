// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
// **************************************************************** //

Shader "Custom/ScrollShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollX ("Scroll X Speed", Range(-1, 1)) = 0.1
        _ScrollY ("Scroll Y Speed", Range(-1, 1)) = 0.1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            float _ScrollX;
            float _ScrollY;
            float _TimeY;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                o.uv.x += _ScrollX * _TimeY;
                o.uv.y += _ScrollY * _TimeY;

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
