Shader "Realistic Water Caustics/Texture Based" {
	Properties {
		_MainTex    ("Main", 2D) = "white" {}
		_BumpTex    ("Bump", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0
		_Metallic   ("Metallic", Range(0, 1)) = 0
		[Space(10)][Header(Caustics)]
		_CausticsTex        ("Caustics", 2D) = "black" {}
		_CausticsCoord      ("Coord", Vector) = (0, 0, 0, 0)
		_CausticsSpeed      ("Speed", Float) = 1
		_CausticsIntensity0 ("Intensity A", Range(0, 1)) = 1
		_CausticsIntensity1 ("Intensity B", Range(0, 1)) = 0
		_CausticsPosition0  ("World Position Y A", Float) = 2
		_CausticsPosition1  ("World Position Y B", Float) = 4
		_CausticsBoost      ("Boost", Range(0, 1)) = 0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf StandardCaustics fullforwardshadows
		#pragma target 3.0
		#include "UnityPBSLighting.cginc"
		
		sampler2D _MainTex, _BumpTex, _CausticsTex;
		float4 _CausticsCoord;
		float _CausticsSpeed, _CausticsIntensity0, _CausticsIntensity1, _CausticsPosition0, _CausticsPosition1, _CausticsBoost, _Glossiness, _Metallic;
		
		float3 CausticsColor (float3 wldpos)
		{
			float4 f0 = tex2D(_CausticsTex, wldpos.zx * _CausticsCoord.xy + float2(+(_Time.x * _CausticsSpeed), 0));
			float4 f1 = tex2D(_CausticsTex, wldpos.zx * _CausticsCoord.xy + float2(-(_Time.x * _CausticsSpeed), 0));
			float4 f2 = tex2D(_CausticsTex, wldpos.zx * _CausticsCoord.xy + float2(0, +(_Time.x * _CausticsSpeed)));
			float4 f3 = tex2D(_CausticsTex, wldpos.zx * _CausticsCoord.xy + float2(0, -(_Time.x * _CausticsSpeed)));
			float3 r = saturate(f0.r + f1.g + f2.b + f3.a);
			return r;
		}
		float CausticsIntensity (float3 wldpos, float3 wldnrm)
		{
			float inten = clamp((wldpos.y - _CausticsPosition0) / (_CausticsPosition1 - _CausticsPosition0), 0.0, 1.0);
			inten = lerp(_CausticsIntensity0, _CausticsIntensity1, inten);
			inten = inten * min(1.0, max(0.0, dot(wldnrm, float3(0, 1, 0)) + 0.5));
			return inten;
		}
		
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpTex;
			float3 worldPos;
			INTERNAL_DATA
		};
		struct SurfaceOutputStandardCaustics
		{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			fixed Alpha;
			float3 worldPos;
			float3 worldNormal;
		};
		void surf (Input IN, inout SurfaceOutputStandardCaustics o)
		{
			o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			o.worldNormal = WorldNormalVector(IN, o.Normal);
			o.worldPos = IN.worldPos;

			float3 caustics = CausticsColor(o.worldPos);
			float ci = CausticsIntensity(o.worldPos, o.worldNormal);

			float4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb;
			o.Metallic = lerp(_Metallic, _Metallic * caustics, ci);
			o.Smoothness = lerp(_Glossiness, _Glossiness * caustics, ci);
			o.Alpha = c.a;
		}
		half4 LightingStandardCaustics (SurfaceOutputStandardCaustics s, half3 viewDir, UnityGI gi)
		{
			s.Normal = normalize(s.Normal);

			half oneMinusReflectivity;
			half3 specColor;
			s.Albedo = DiffuseAndSpecularFromMetallic(s.Albedo, s.Metallic, /*out*/ specColor, /*out*/ oneMinusReflectivity);
			
			// Caustics
			float4 c0 = tex2D(_CausticsTex, s.worldPos.zx * _CausticsCoord.xy + float2(+(_Time.x * _CausticsSpeed), 0));
			float4 c1 = tex2D(_CausticsTex, s.worldPos.zx * _CausticsCoord.xy + float2(-(_Time.x * _CausticsSpeed), 0));
			float4 c2 = tex2D(_CausticsTex, s.worldPos.zx * _CausticsCoord.xy + float2(0, +(_Time.x * _CausticsSpeed)));
			float4 c3 = tex2D(_CausticsTex, s.worldPos.zx * _CausticsCoord.xy + float2(0, -(_Time.x * _CausticsSpeed)));
			float3 caustics = ((saturate(c0.r + c1.g + c2.b + c3.a) * (_CausticsBoost + 1)) + _CausticsBoost);

			float ci = CausticsIntensity(s.worldPos, s.worldNormal);

			s.Albedo = lerp(s.Albedo, s.Albedo * caustics, ci);

			half outputAlpha;
			s.Albedo = PreMultiplyAlpha(s.Albedo, s.Alpha, oneMinusReflectivity, /*out*/ outputAlpha);

			half4 c = UNITY_BRDF_PBS(s.Albedo, specColor, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi.light, gi.indirect);
			c.rgb += UNITY_BRDF_GI(s.Albedo, specColor, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, 1.0, gi);
			c.a = outputAlpha;
			return c;
		}
		void LightingStandardCaustics_GI (SurfaceOutputStandardCaustics s, UnityGIInput data, inout UnityGI gi)
		{
			gi = UnityGlobalIllumination(data, 1.0, s.Smoothness, s.Normal);
		}
		ENDCG
	} 
	FallBack Off
}
