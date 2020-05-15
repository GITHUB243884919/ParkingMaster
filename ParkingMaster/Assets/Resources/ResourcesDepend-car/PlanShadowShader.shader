Shader "Unlit/PlanShadowShader"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_AlbedoAmount("Albedo Amount", Range(0.03, 1)) = 0.7

		_ShadowInvLen ("ShadowInvLen", float) = 0 //0.4449261
	}
	SubShader
	{
		Tags{"Queue" = "Geometry+10" "RenderType" = "Opaque" }
		Pass{
			ColorMask 0
		}
		LOD 100

		Pass
		{		
			Blend SrcAlpha  OneMinusSrcAlpha
			ZWrite Off
			Cull Back
			ColorMask RGB
			
			Stencil
			{
				Ref 0			
				Comp Equal			
				WriteMask 255		
				ReadMask 255
				//Pass IncrSat
				Pass Invert
				Fail Keep
				ZFail Keep
			}
			
			CGPROGRAM
			#pragma multi_compile_fog
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _ShadowPlane;
			float4 _ShadowProjDir;
			float4 _WorldPos;
			float _ShadowInvLen;
			float4 _ShadowFadeParams;
			float _ShadowFalloff;
			
			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 xlv_TEXCOORD0 : TEXCOORD0;
				float3 xlv_TEXCOORD1 : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			v2f vert(appdata v)
			{
				v2f o;

				float3 lightdir = normalize(_ShadowProjDir);
				float3 worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
				// _ShadowPlane.w = p0 * n  // 平面的w分量就是p0 * n
				float distance = (_ShadowPlane.w - dot(_ShadowPlane.xyz, worldpos)) / dot(_ShadowPlane.xyz, lightdir.xyz);
				worldpos = worldpos + distance * lightdir.xyz;
				o.vertex = mul(unity_MatrixVP, float4(worldpos, 1.0));
				o.xlv_TEXCOORD0 = _WorldPos.xyz;
				o.xlv_TEXCOORD1 = worldpos;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			float4 frag(v2f i) : SV_Target
			{
				float3 posToPlane_2 = (i.xlv_TEXCOORD0 - i.xlv_TEXCOORD1);
				float4 color;
				color.xyz = float3(0.0, 0.0, 0.0);
				
				// 下面两种阴影衰减公式都可以使用(当然也可以自己写衰减公式)
				// 1. 王者荣耀的衰减公式
				color.w = (pow((1.0 - clamp(((sqrt(dot(posToPlane_2, posToPlane_2)) * _ShadowInvLen) - _ShadowFadeParams.x), 0.0, 1.0)), _ShadowFadeParams.y) * _ShadowFadeParams.z);
				float3 lightColor = UNITY_LIGHTMODEL_AMBIENT.xyz;
				// 2. https://zhuanlan.zhihu.com/p/31504088 这篇文章介绍的另外的阴影衰减公式
				//color.w = 1.0 - saturate(distance(i.xlv_TEXCOORD0, i.xlv_TEXCOORD1) * _ShadowFalloff);
				UNITY_APPLY_FOG(i.fogCoord, color);
				return color;
			}
			
			ENDCG
		}
		CGPROGRAM
#pragma surface surf Lambert
		//float _AlbedoAmount;

		//inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		//{
		//	fixed diff = dot(s.Normal, lightDir) *0.5 + 0.5;
		//	fixed nh = max(0, dot(s.Normal, halfDir));
		//	fixed spec = pow(nh, s.Specular * 128) * s.Specular;

		//	fixed4 c;
		//	c.rgb = (s.Albedo * _LightColor0.rgb * diff * _AlbedoAmount + _LightColor0.rgb * spec) * atten;
		//	UNITY_OPAQUE_ALPHA(c.a);
		//	return c;
		//}
	
		sampler2D _MainTex;
		fixed4 _Color;
		//samplerCUBE _CubeTex;
		//half _ReflectAmount;

		struct Input {
			float2 uv_MainTex;
			//float3 viewDir;
			//float3 worldNormal;
			//float3 worldRefl;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)* _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			//half4 tex = tex2D(_MainTex, IN.uv_MainTex);
			//o.Albedo = tex;
			//o.Gloss = tex.a;
			//o.Specular = max(0.01, tex.a);
			//o.Emission = texCUBE(_CubeTex, IN.worldRefl).rgb * tex.a * _ReflectAmount;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Diffuse"
}
