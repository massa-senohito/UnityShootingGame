Shader "TargetMarker" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Bump (RGB) Illumin (A)", 2D) = "bump" {}
	}
	SubShader {
		UsePass "Self-Illumin/VertexLit/BASE"
		UsePass "Bumped Diffuse/PPL"
		Pass{
			CGPROGRAM
			#pragma surface surf Lambert
			#include "UnityCG.cginc"	// ビルトインCgファイルをインクルード
			struct Input {
				float2 uv_MainTex;
				float4 screenPos;
			};

			half4 surf (Input i) : COLOR
			{
				MainTex=i.screenPos
				return i. float2(8,6) //multiply vec2 8,6
			}
			ENDCG
		}
	} 

	FallBack "Diffuse"
}

