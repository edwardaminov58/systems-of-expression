Shader "Realistic Water Caustics/Texture Based 2" {
	Properties {
		_MainTex    ("Main", 2D) = "white" {}
		_BumpTex    ("Bump", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0
		_Metallic   ("Metallic", Range(0, 1)) = 0
		[Space(10)][Header(Caustics)]
		_CausticsTex        ("Caustics", 2D) = "black" {}
		_CausticsIntensity0 ("Intensity A", Range(0, 1)) = 1
		_CausticsIntensity1 ("Intensity B", Range(0, 1)) = 0
		_CausticsPosition0  ("World Position Y A", Float) = 2
		_CausticsPosition1  ("World Position Y B", Float) = 4
		_CausticsBoost      ("Boost", Range(1, 4)) = 0

		_Caustics1_ST      ("Caustics1 ST", Vector) = (1, 1, 0, 0)
		_Caustics2_ST      ("Caustics2 ST", Vector) = (1, 1, 0, 0)
		_Caustics1Speed    ("Caustics1 Speed", Float) = 1
		_Caustics2Speed    ("Caustics2 Speed", Float) = 1
		_SplitRGB          ("Split", Range(0, 0.02)) = 0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		#include "UnityPBSLighting.cginc"
		
		sampler2D _MainTex, _BumpTex, _CausticsTex;
		float _CausticsIntensity0, _CausticsIntensity1, _CausticsPosition0, _CausticsPosition1, _CausticsBoost, _Glossiness, _Metallic;
		
		float4 _Caustics1_ST, _Caustics2_ST;
		float _Caustics1Speed, _Caustics2Speed, _SplitRGB;

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
		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			float3 wldnrm = WorldNormalVector(IN, o.Normal);
			float3 wldpos = IN.worldPos;

			float ci = CausticsIntensity(wldpos, wldnrm);

			float4 c = tex2D (_MainTex, IN.uv_MainTex);

			// caustics begin
			float s = _SplitRGB;
			
			fixed2 uv1 = IN.uv_MainTex * _Caustics1_ST.xy + _Caustics1_ST.zw;
			uv1 += _Caustics1Speed * _Time.y;
			float r = tex2D(_CausticsTex, uv1 + fixed2(+s, +s)).r;
			float g = tex2D(_CausticsTex, uv1 + fixed2(+s, -s)).g;
			float b = tex2D(_CausticsTex, uv1 + fixed2(-s, -s)).b;
			float3 c1 = float3(r, g, b);
			
			fixed2 uv2 = IN.uv_MainTex * _Caustics2_ST.xy + _Caustics2_ST.zw;
			uv2 += _Caustics2Speed * _Time.y;
			r = tex2D(_CausticsTex, uv2 + fixed2(+s, +s)).r;
			g = tex2D(_CausticsTex, uv2 + fixed2(+s, -s)).g;
			b = tex2D(_CausticsTex, uv2 + fixed2(-s, -s)).b;
			float3 c2 = float3(r, g, b);
			
			float3 caustics = min(c1, c2);
			c.rgb = lerp(c.rgb, c.rgb * caustics * _CausticsBoost, ci);
			// caustics end
			
			o.Albedo = c.rgb;
			o.Metallic = lerp(_Metallic, _Metallic * caustics, ci);
			o.Smoothness = lerp(_Glossiness, _Glossiness * caustics, ci);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack Off
}
