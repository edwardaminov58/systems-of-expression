#ifndef REALISTIC_WATER_CAUSTICS_INCLUDED
#define REALISTIC_WATER_CAUSTICS_INCLUDED

#include "UnityCG.cginc"

float3 _Color;
float _Tiled;
float _Density;
float _Intensity;
float _Lerp;
float _FadeHeight;
float _FadeFalloff;
float _RWC2_Tiled;
float _RWC2_Intensity;
float _RWC2_Wave;

struct v2f
{
	float4 pos : SV_POSITION;
	float2 tex : TEXCOORD0;
	float3 wldpos : TEXCOORD1;
	float3 wldnor : TEXCOORD2;
	UNITY_FOG_COORDS(3)
};
v2f vert (appdata_base v)
{
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.tex = v.texcoord;
	o.wldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
	o.wldnor = mul(unity_ObjectToWorld, SCALED_NORMAL);
	UNITY_TRANSFER_FOG(o, o.pos);
	return o;
}
float4 frag (v2f input) : SV_TARGET
{
	// height fade
	float fade = (input.wldpos.y - _FadeHeight) / _FadeFalloff;
	fade = 1 - fade;

	// below side fade
	float3 UP = float3(0, 1, 0);
	float3 N = normalize(input.wldnor);
	fade *= min(1, max(0, dot(N, UP) + 0.5));
	
	// two kinds of caustics
#ifdef RWC_TYPE1	
	float2 uv = input.tex;
	float2 p = fmod(uv * _Tiled, _Tiled) - 250.0;
	float2 i = p;
	float c = 1.0;
				
	for (int n = 0; n < ITER; n++)
	{
		float t = _Time.y * (1.0 - (3.5 / float(n + 1)));
		i = p + float2(cos(t - i.x) + sin(t + i.y), sin(t - i.y) + cos(t + i.x));
		c += 1.0 / length(float2(p.x / (sin(i.x + t) / _Density), p.y / (cos(i.y + t) / _Density)));
	}
	c /= float(ITER);
	c = 1.17 - pow(c, _Intensity);
	float tmp = pow(abs(c), 8.0);
	float3 cc = float3(tmp, tmp, tmp);
#endif
#ifdef RWC_TYPE2
	#define F length(0.5 - frac(cc.xyw = mul(float3x3(-2,-1,2, 3,-2,1, 1,2,2), cc.xyw)*
	float4 cc = float4(1, 1, 1, _Time.y) * 0.5;
	cc.xy = _RWC2_Tiled * input.tex * (sin(cc).w * _RWC2_Wave + 2.0) / 2e2;
	cc = pow(min(min(F 0.5)),F 0.4))),F 0.3))), 6.0) * _RWC2_Intensity;
#endif
	float4 col = float4(cc * _Color * fade, _Lerp);
	UNITY_APPLY_FOG(input.fogCoord, col);
	return col;
}

#endif