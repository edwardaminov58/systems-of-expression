Shader "Realistic Water Caustics/GPU Based" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Tiled ("Tiled", Range(8, 24)) = 6.283
		_Density ("Density", Range(0.002, 0.02)) = 0.005
		_Intensity ("Intensity", Range(0, 4)) = 1.4
		_Lerp ("Lerp", Range(0, 1)) = 0.5
		_FadeHeight ("Fade Height", Range(0, 6)) = 2
		_FadeFalloff ("Fade Falloff", Range(0.1, 2)) = 1
		_RWC2_Tiled ("RWC2 Tiled", Float) = 512
		_RWC2_Intensity ("RWC2 Intensity", Range(0, 30)) = 25
		_RWC2_Wave ("RWC2 Wave", Float) = 0.3
		[Header(RenderState)]
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendSrc ("Blend Src", Int) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDst ("Blend Dst", Int) = 0
	}	
	SubShader {
		Tags { "RenderType" = "Transparent - 1" }
		Pass {
			Blend [_BlendSrc] [_BlendDst]
			ZWrite Off
			Offset -1, -1
			Cull Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile RWC_TYPE1 RWC_TYPE2
			#define ITER 5
			#include "RealisticWaterCaustics.cginc"
			ENDCG
		}
	} 
	FallBack Off
	CustomEditor "RealisticWaterCausticsShaderGUI"
}
