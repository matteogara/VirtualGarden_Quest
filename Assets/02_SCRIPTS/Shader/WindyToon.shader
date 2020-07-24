// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/WindToon" {
 
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _wind_dir ("Wind Direction", Vector) = (0.5,0.05,0.5,0)
        _wind_size ("Wind Wave Size", range(0,50)) = 15

        _tree_sway_stutter_influence("Tree Sway Stutter Influence", range(0,1)) = 0.2
        _tree_sway_stutter ("Tree Sway Stutter", range(0,10)) = 1.5
        _tree_sway_speed ("Tree Sway Speed", range(0,10)) = 1
        _tree_sway_disp ("Tree Sway Displacement", range(0,2)) = 0.3

        _branches_disp ("Branches Displacement", range(0,0.5)) = 0.3

        _leaves_wiggle_disp ("Leaves Wiggle Displacement", float) = 0.07
        _leaves_wiggle_speed ("Leaves Wiggle Speed", float) = 0.01

        _r_influence ("Red Vertex Influence", range(0,1)) = 1
        _b_influence ("Blue Vertex Influence", range(0,1)) = 1
        
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        // Controls the size of the specular reflection.
        _Glossiness("Glossiness", Float) = 32
        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        // Control how smoothly the rim blends when approaching unlit
        // parts of the surface.
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1    

    }
 
    SubShader {
        
        Pass {
        
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }
            
            CGPROGRAM
            #pragma target 3.0
            //#pragma surface surf Lambert vertex:vert addshadow
            #pragma vertex vert
            #pragma fragment frag
            //// Compile multiple versions of this shader depending on lighting settings.
            //#pragma multi_compile_fwdbase
            
            #include "UnityCG.cginc"
            // Files below include macros and functions to assist
            // with lighting and shadows.
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;               
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1; 
                // Macro found in Autolight.cginc. Declares a vector4
                // into the TEXCOORD2 semantic with varying precision 
                // depending on platform target.
                SHADOW_COORDS(2)
            };


            //Declared Variables
            float4 _wind_dir;
            float _wind_size;
            float _tree_sway_speed;
            float _tree_sway_disp;
            float _leaves_wiggle_disp;
            float _leaves_wiggle_speed;
            float _branches_disp;
            float _tree_sway_stutter;
            float _tree_sway_stutter_influence;
            float _r_influence;
            float _b_influence;

            //Structs
            struct Input {
                float2 uv_MainTex;
            };
            
            float4 _MainTex_ST;

            // Vertex Manipulation Function
            v2f vert (appdata v) {                 
                v2f o;
                
                 //Gets the vertex's World Position 
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;

                //Tree Movement and Wiggle
                float newX = v.vertex.x + (cos(_Time.z * _tree_sway_speed + (worldPos.x/_wind_size) + (sin(_Time.z * _tree_sway_stutter * _tree_sway_speed + (worldPos.x/_wind_size)) * _tree_sway_stutter_influence) ) + 1)/2 * _tree_sway_disp * _wind_dir.x * (v.vertex.y / 10) + 
                cos(_Time.w * v.vertex.x * _leaves_wiggle_speed + (worldPos.x/_wind_size)) * _leaves_wiggle_disp * _wind_dir.x * _b_influence;

                float newZ = v.vertex.z + (cos(_Time.z * _tree_sway_speed + (worldPos.z/_wind_size) + (sin(_Time.z * _tree_sway_stutter * _tree_sway_speed + (worldPos.z/_wind_size)) * _tree_sway_stutter_influence) ) + 1)/2 * _tree_sway_disp * _wind_dir.z * (v.vertex.y / 10) + 
                cos(_Time.w * v.vertex.z * _leaves_wiggle_speed + (worldPos.x/_wind_size)) * _leaves_wiggle_disp * _wind_dir.z * _b_influence;

                float newY = v.vertex.y + cos(_Time.z * _tree_sway_speed + (worldPos.z/_wind_size)) * _tree_sway_disp * _wind_dir.y * (v.vertex.y / 10);

                //Branches Movement
                newY += sin(_Time.w * _tree_sway_speed + _wind_dir.x + (worldPos.z/_wind_size)) * _branches_disp * _r_influence;
                
                o.pos = UnityObjectToClipPos(float4(newX, newY, newZ, 0));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);     
                o.viewDir = WorldSpaceViewDir(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // Defined in Autolight.cginc. Assigns the above shadow coordinate
                // by transforming the vertex from world space to shadow-map space.
                //TRANSFER_SHADOW(o);
                
                return o;
            }
            
            
            sampler2D _MainTex;
            float4 _Color;

            float4 _AmbientColor;

            float4 _SpecularColor;
            float _Glossiness;      

            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;    

            float4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);

                // Lighting below is calculated using Blinn-Phong,
                // with values thresholded to creat the "toon" look.
                // https://en.wikipedia.org/wiki/Blinn-Phong_shading_model

                // Calculate illumination from directional light.
                // _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
                // direction of the main directional light.
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                // Samples the shadow map, returning a value in the 0...1 range,
                // where 0 is in the shadow, and 1 is not.
                float shadow = SHADOW_ATTENUATION(i);
                // Partition the intensity into light and dark, smoothly interpolated
                // between the two to avoid a jagged break.
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow); 
                // Multiply by the main directional light's intensity and color.
                float4 light = lightIntensity * _LightColor0;

                // Calculate specular reflection.
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                // Multiply _Glossiness by itself to allow artist to use smaller
                // glossiness values in the inspector.
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;             

                // Calculate rim lighting.
                float rimDot = 1 - dot(viewDir, normal);
                // We only want rim to appear on the lit side of the surface,
                // so multiply it by NdotL, raised to a power to smoothly blend it.
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor * 0.4;

                float4 sample = tex2D(_MainTex, i.uv);
                
                return (light + _AmbientColor + specular + rim) * _Color * sample;
            }
            ENDCG
        }
        
        Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
           
            Fog {Mode Off}
            ZWrite On ZTest Less Cull Off
            Offset [_ShadowBias], [_ShadowBiasSlope]
   
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile SHADOWS_NATIVE SHADOWS_CUBE
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            #include "TerrainEngine.cginc"
           
            struct v2f {
                V2F_SHADOW_CASTER;
            };
           
            struct appdata {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };
            
            float4 _wind_dir;
            float _wind_size;
            float _tree_sway_speed;
            float _tree_sway_disp;
            float _leaves_wiggle_disp;
            float _leaves_wiggle_speed;
            float _branches_disp;
            float _tree_sway_stutter;
            float _tree_sway_stutter_influence;
            float _r_influence;
            float _b_influence;
            
            v2f vert( appdata v )
            {
                v2f o;
                
                TRANSFER_SHADOW_CASTER(o)
                
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                float newX = v.vertex.x + (cos(_Time.z * _tree_sway_speed + (worldPos.x/_wind_size) + (sin(_Time.z * _tree_sway_stutter * _tree_sway_speed + (worldPos.x/_wind_size)) * _tree_sway_stutter_influence) ) + 1)/2 * _tree_sway_disp * _wind_dir.x * (v.vertex.y / 10) + 
                cos(_Time.w * v.vertex.x * _leaves_wiggle_speed + (worldPos.x/_wind_size)) * _leaves_wiggle_disp * _wind_dir.x * _b_influence;
                float newZ = v.vertex.z + (cos(_Time.z * _tree_sway_speed + (worldPos.z/_wind_size) + (sin(_Time.z * _tree_sway_stutter * _tree_sway_speed + (worldPos.z/_wind_size)) * _tree_sway_stutter_influence) ) + 1)/2 * _tree_sway_disp * _wind_dir.z * (v.vertex.y / 10) + 
                cos(_Time.w * v.vertex.z * _leaves_wiggle_speed + (worldPos.x/_wind_size)) * _leaves_wiggle_disp * _wind_dir.z * _b_influence;
                float newY = v.vertex.y + cos(_Time.z * _tree_sway_speed + (worldPos.z/_wind_size)) * _tree_sway_disp * _wind_dir.y * (v.vertex.y / 10);
                newY += sin(_Time.w * _tree_sway_speed + _wind_dir.x + (worldPos.z/_wind_size)) * _branches_disp * _r_influence;
                o.pos = UnityObjectToClipPos(float4(newX, newY, newZ, 0));

                return o;
            }
           
            float4 frag( v2f i ) : COLOR
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG  
        }
    }
} 