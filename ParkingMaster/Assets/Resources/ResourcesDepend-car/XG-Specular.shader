Shader "XG/Hero_Spec3" {
	Properties{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		_AlbedoAmount("Albedo Amount", Range(0.03, 1)) = 0.7
		//_ShadowInvLen ("ShadowInvLen", float) = 0 //0.4449261
	}
	SubShader{
		Tags{ "Queue" = "Geometry+10" "RenderType" = "Opaque" }
	
		LOD 100
		
		CGPROGRAM
#pragma surface surf MobileBlinnPhong exclude_path:prepass nolightmap noforwardadd halfasview interpolateview addshadow nofog
		float _AlbedoAmount;

		inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		{
			fixed diff = dot(s.Normal, lightDir) *0.5 + 0.5;
			fixed nh = max(0, dot(s.Normal, halfDir));
			fixed spec = pow(nh, s.Specular * 128) * s.Specular;

			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff * _AlbedoAmount + _LightColor0.rgb * spec) * atten;
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
	
		sampler2D _MainTex;
		//samplerCUBE _CubeTex;
		//half _ReflectAmount;
		fixed4 _Color;
		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
			float3 worldRefl;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			half4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = tex.rgb;
			o.Gloss = tex.a;
			o.Specular = max(0.01, tex.a);
			//o.Emission = texCUBE(_CubeTex, IN.worldRefl).rgb * tex.a * _ReflectAmount;
		}
		ENDCG
	}
	Fallback "Mobile/Diffuse"
}



