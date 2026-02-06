// Compiled shader for Windows, Mac, Linux

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Unlit/Texture" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
 
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" }


 // Stats for Vertex shader:
 //        d3d11: 9 math
 // Stats for Fragment shader:
 //        d3d11: 0 math, 1 texture
 Pass {
  Tags { "RenderType"="Opaque" }

 }
}
}