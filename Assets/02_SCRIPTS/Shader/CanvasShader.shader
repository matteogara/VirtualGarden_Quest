Shader "Unlit/CanvasShader"
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
        _HideAreas ("Show areas", Float) = 0.0
        //_DiscOn ("Show discontinuities", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            //sampler2D _MainTex;
            sampler2D _Paint;
            half4 _MainTex_ST;
            half4 _Col1;
            half4 _Col2;
            half4 _Col3;
            half4 _Col4;
            half4 _Col5;
            float _HideAreas;
            //float _DiscOn;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {            
                // sample the texture
                half4 col = 1 - tex2D(_Paint, i.uv);
                
                //float range = 0.01;
                //fixed4 n = abs(paint - tex2D(_Paint, i.uv + float3(0, -range, 0)));
                //fixed4 no = abs(paint - tex2D(_Paint, i.uv + float3(-range, -range, 0)));
                //fixed4 o = abs(paint - tex2D(_Paint, i.uv + float3(-range, 0, 0)));
                //fixed4 so = abs(paint - tex2D(_Paint, i.uv + float3(-range, range, 0)));
                //fixed4 s = abs(paint - tex2D(_Paint, i.uv + float3(0, range, 0)));
                //fixed4 se = abs(paint - tex2D(_Paint, i.uv + float3(range, range, 0)));
                //fixed4 e = abs(paint - tex2D(_Paint, i.uv + float3(range, 0, 0)));
                //fixed4 ne = abs(paint - tex2D(_Paint, i.uv + float3(range, -range, 0)));
                //fixed4 diff = (n + no + o + so + s + se + e + ne) / 8.0 * 10;
                //fixed luma = (diff.r + diff.g + diff.b) / 3;
                
                //if (_DiscOn == 1.0) {
                //    return luma;
                //    if (luma > 0.3) {
                //        return 1;
                //    } else {
                //        return 0;
                //    }
                //}
                
                //if (_DiscOn == 2.0) {
                //    if (luma > 0.3) {
                //        return 0;
                //    } else {
                //        return 1 - paint;
                //    }
                //}
                
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
                
                //if (o > 0) {
                //    return col;
                //} else {
                //    return 1;
                //}
                
                o = saturate(o * 0.5 + 0.5 + _HideAreas);
                
                return o;
                
            }
            ENDCG
        }
    }
}
