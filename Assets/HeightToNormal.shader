Shader "Hidden/HeightToNormal" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Offset ("Sample Offset", Range(-5, 5)) = 1
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
			uniform half _Offset;

			fixed4 frag (v2f_img i) : SV_Target {
				float2 uvLeft  = i.uv + float2(-_MainTex_TexelSize.x * _Offset, 0);
				float2 uvRight = i.uv + float2( _MainTex_TexelSize.x * _Offset, 0);
				float left = Luminance(tex2D(_MainTex, uvLeft));
				float right = Luminance(tex2D(_MainTex, uvRight));

				float2 uvUp   = i.uv + float2(0,  _MainTex_TexelSize.y * _Offset);
				float2 uvDown = i.uv + float2(0, -_MainTex_TexelSize.y * _Offset);
				float up = Luminance(tex2D(_MainTex, uvUp));
				float down = Luminance(tex2D(_MainTex, uvDown));

				float3 vx = float3(1, 0, (left - right));
				float3 vy = float3(0, 1, (up - down));
				float3 normal = cross(vx, vy);
				normal = normalize(normal);
				normal += 1;
				normal *= .5;

				//return float4((up - down).xxx, 1);
				//return float4(normal.x, normal.y, normal.z, 1);
				//return float4(normal.x, normal.x, normal.x, normal.y);
				return float4(normal.y, normal.y, normal.y, normal.x);
			}
		ENDCG

		}
	}

	Fallback off

}
