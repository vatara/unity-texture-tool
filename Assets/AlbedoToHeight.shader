Shader "Hidden/AlbedoToHeight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Contrast ("Contrast", Range(0, 10)) = 1
		_Offset ("Offset", Range (-1, 1)) = 0
	}

	SubShader {
		Pass {
			ZTest Always 
			Cull Off 
			ZWrite Off
				
		CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform float _Contrast;
			uniform float _Offset;

			fixed4 frag (v2f_img i) : SV_Target {
				float4 original = tex2D(_MainTex, i.uv);
				fixed grayscale = (Luminance(original.rgb) - .5) * _Contrast + .5;
				grayscale += _Offset.xxx;
				return float4(grayscale.xxx, 1);
			}
		ENDCG

		}
	}

	Fallback off

}
