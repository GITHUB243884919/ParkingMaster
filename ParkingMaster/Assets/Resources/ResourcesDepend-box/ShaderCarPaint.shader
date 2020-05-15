Shader "Custom/ShaderCarPaint" 
{
	Properties 
    {
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _ColorLit ("Color Lit", Color) = (1,1,1,0.57)
        _FresnelColor ("Fresnel Color", Color) = (0,0,0,0.25)
        _Specularity ("Specularity", Range(0.01, 1)) = 0.2
        _SpecularPower ("SpecularPower", Range(0, 1)) = 0.3
        _Glossiness ("Smoothness", Range(0, 1)) = 1
        _FresnePow ("Fresnel power", Range(0, 1)) = 1
        _ReflectionNormalSmoothing ("Reflection Normal Smoothing", Range(0, 1)) = 0.3
        _ReflectionAdd ("Reflection Add Modifier", Range(0, 1)) = 0.05
        _LightOverflowOffset ("LightOverflowOffset", Range(0, 10)) = 1
        _MainLightOverflow ("MainLightOverflow", Range(0, 1)) = 0
        _Emissive ("Emissive Color", Color) = (0,0,0,0)
	}

    SubShader
    {
		Tags { "RenderType"="Opaque" }

	    Pass 
        {
            Tags { "LightMode" = "ForwardBase" "RenderType" = "Opaque"}

        CGPROGRAM
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            #include "UnityCG.cginc"

            #pragma multi_compile_fwdbase
            
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;
            float _Glossiness;
            float _FresnePow;
            float _Specularity;
            float _SpecularPower;
            float _MainLightOverflow;
            float _LightOverflowOffset;
            half4 _ColorLit;
            float _ReflectionAdd;
            half4 _FresnelColor;
            float _ReflectionNormalSmoothing;
            half4 _Emissive;

            struct appdata_t
            {
                float4 vertex : POSITION;
                half3 normal : NORMAL;
            };
                    
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 mainColor : TEXCOORD0;
                float4 specRefl : TEXCOORD1;
                UNITY_SHADOW_COORDS(2)
                float4 ambientColor : TEXCOORD3;
            };

            v2f vert(appdata_t v) 
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);

                o.pos = UnityObjectToClipPos(v.vertex);
                
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);

                half n2l = dot(worldNormal, _WorldSpaceLightPos0.xyz);
                half diffRefl = max(0, n2l);

                half litIntensity = _ColorLit.w * 20 + 1;
                litIntensity = pow(diffRefl, litIntensity);
                half3 colorCal = lerp(_Color, _ColorLit, litIntensity);

                half3 worldViewDir = normalize(_WorldSpaceCameraPos.xyz - worldPos.xyz);
                half n2v = max(0, dot(worldViewDir, worldNormal));
                half reverseN2v = 1 - n2v;
                
                half specReverseFactor = _FresnelColor.w * 5 + 1;
                specReverseFactor = max(0, 1 - pow(reverseN2v, specReverseFactor));
                
                colorCal = lerp(_FresnelColor.xyz, colorCal.xyz, specReverseFactor);

                half reverseN2l = dot(-_WorldSpaceLightPos0.xyz, worldNormal);
                reverseN2l = reverseN2l + reverseN2l;
                float3 insideLDir = worldNormal.xyz * (-reverseN2l) + (-_WorldSpaceLightPos0.xyz);
                half specInsideLFactor = max(0, dot(insideLDir, worldViewDir));
                specInsideLFactor = pow(specInsideLFactor, _Specularity * 128) * _SpecularPower;
                
                colorCal = colorCal.xyz * diffRefl + specInsideLFactor;

                o.mainColor.xyz = colorCal.xyz * _LightColor0.xyz;
                o.mainColor.w = step(n2l, 0);

                half oppoN2v = dot(-worldViewDir, worldNormal);
                oppoN2v = oppoN2v + oppoN2v;
                half3 insidVdir = worldNormal * (-oppoN2v) + (-worldViewDir);
                
                float3 worldVertex = UnityObjectToWorldNormal(v.vertex);
                half oppov2P = dot(-worldViewDir, worldVertex);
                oppov2P = oppov2P + oppov2P;
                float3 insidRefl = worldVertex * (-oppov2P) + (-worldViewDir);
                o.specRefl.xyz = lerp(insidVdir, insidRefl, _ReflectionNormalSmoothing);

                half fresneFactor = (1- _FresnePow * n2v) * _Glossiness;
                fresneFactor = pow(fresneFactor, _FresnePow * 4 + 1);
                fresneFactor = fresneFactor + _ReflectionAdd;
                o.specRefl.w = min(fresneFactor, 1);

                TRANSFER_SHADOW(o);

                float4 worldNormalNew = normalize(float4(worldNormal, 1));
                float3 shColor = ShadeSH9(worldNormalNew);

                half overflowFactor = (-reverseN2v) * _FresnelColor.w + 1;
                half3 overflowColor = lerp(_FresnelColor.xyz, _Color.xyz, overflowFactor);
                half reverseRefl = 1 - diffRefl;
                half overflowOffsetFactor = _LightOverflowOffset * _MainLightOverflow;
                reverseRefl = reverseRefl * overflowOffsetFactor;
                half3 color = _Color.xyz * _LightColor0.xyz * reverseRefl;

                o.ambientColor.xyz = shColor * overflowColor + color;
                o.ambientColor.w = _Color.w;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                half shadowAtten = SHADOW_ATTENUATION(i);
                shadowAtten = max(shadowAtten, i.mainColor.w);

                half3 reflColor = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, i.specRefl.xyz).xyz;
                reflColor.xyz = reflColor * i.specRefl.w;

                half3 col = i.mainColor.xyz * shadowAtten + reflColor + i.ambientColor.xyz + _Emissive.xyz;                
                
                return float4(col, i.ambientColor.w);
            }

        ENDCG
        }
    }

    FallBack "Diffuse"
}
