Shader "Custom/Canvas_2.0"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Paint ("Paint", 2D) = "white" {}
        _Col1 ("Smell 1", Color) = (1,1,1,1)
        _Col2 ("Smell 1", Color) = (1,1,1,1)
        _Col3 ("Smell 1", Color) = (1,1,1,1)
        _Col4 ("Smell 1", Color) = (1,1,1,1)
        _Col5 ("Smell 1", Color) = (1,1,1,1)
        //_HideAreas ("Show areas", Float) = 0.0
    }
    SubShader
    {
        Pass
        {
            // Setup our pass to use Forward rendering, and only receive
            // data on the main directional light and ambient light.
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Compile multiple versions of this shader depending on lighting settings.
            #pragma multi_compile_fwdbase
            
            #include "UnityCG.cginc"
            // Files below include macros and functions to assist
            // with lighting and shadows.
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;               
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1)
            };

            sampler2D _Paint;
            half4 _MainTex_ST;
            half4 _Col1;
            half4 _Col2;
            half4 _Col3;
            half4 _Col4;
            half4 _Col5;
            //float _HideAreas;
            
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                TRANSFER_SHADOW(o)
                return o;
            }
            
            
            float4 frag (v2f i) : SV_Target
            {
                half4 col = 1 - tex2D(_Paint, i.uv);
                
                half3 delta1 = abs(col.rgb - _Col1.rgb);
                half3 delta2 = abs(col.rgb - _Col2.rgb);
                half3 delta3 = abs(col.rgb - _Col3.rgb);
                half3 delta4 = abs(col.rgb - _Col4.rgb);
                half3 delta5 = abs(col.rgb - _Col5.rgb);      
                
                half4 o = (1,1,1,1);
                o = (delta1.r + delta1.g + delta1.b) < 0.04 ? col : o;
                o = (delta2.r + delta2.g + delta2.b) < 0.04 ? col : o;
                o = (delta3.r + delta3.g + delta3.b) < 0.04 ? col : o;
                o = (delta4.r + delta4.g + delta4.b) < 0.04 ? col : o;
                o = (delta5.r + delta5.g + delta5.b) < 0.04 ? col : o;
                
                //o = saturate(o * 0.5 + 0.5 + _HideAreas);
                o *= 0.5;
                o += 0.5;
                half4 s = saturate(SHADOW_ATTENUATION(i) + 0.92);
            
                return o * s - 0.05;
            }
            ENDCG
        }

        // Shadow casting support.
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}