Shader "Image Effects/Blur/LightTrails2" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _AccumTex ("Base (RGB)", 2D) = "black" {}
}
 
SubShader {
    Pass {
        ZTest Always Cull Off ZWrite Off
             
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"
 
uniform sampler2D _MainTex;
uniform sampler2D _AccumTex;
uniform float _AccumOrig;
 
float4 frag (v2f_img i) : SV_Target
{
    float4 original = tex2D(_MainTex, i.uv);
    float4 accum = tex2D(_AccumTex, i.uv);
    return max(accum*_AccumOrig, original);
}
ENDCG
 
    }
}
 
Fallback off
 
}