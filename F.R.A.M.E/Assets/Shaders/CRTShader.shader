﻿Shader "Custom/CRTShader"
{
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_VertsColor("Verts fill color", Float) = 0
		_VertsColor2("Verts fill color 2", Float) = 0
		_Contrast("Contrast", Float) = 0
		_Br("Brightness", Float) = 0
		_ScansColor("ScansColor", Float) = 0
	}

		SubShader{
			Pass {
				ZTest Always Cull Off ZWrite Off Fog { Mode off }

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				#pragma target 3.0

				uniform float _VertsColor;
				uniform float _VertsColor2;
				uniform float _Contrast;
				uniform float _Br;
				uniform float _ScansColor;

				struct v2f
				{
					float4 pos      : POSITION;
					float2 uv       : TEXCOORD0;
					float4 scr_pos : TEXCOORD1;
				};
				
				uniform sampler2D _MainTex;

				v2f vert(appdata_img v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
					o.scr_pos = ComputeScreenPos(o.pos);
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					half4 color = tex2D(_MainTex, i.uv);
					float2 ps = i.scr_pos.xy *_ScreenParams.xy / i.scr_pos.w;
					int pp = (int)ps.y % 3;
					color += (_Br / 255);
					color = color - _Contrast * (color - 1.0) * color *(color - 0.5);
					float4 outcolor = float4(0, 0, 0, 1);
					float4 muls = outcolor;
					if (pp == 1) { muls.r = 1; muls.g = _VertsColor2; }
					else if (pp == 2) { muls.g = 1; muls.b = _VertsColor2; }
					else { muls.b = 1; muls.r = _VertsColor2; } color = color * muls * 2;//if ((int)ps.x % 3 == 0) muls *= float4(_ScansColor, _ScansColor, _ScansColor, 1); color = color * muls * 2;
					return color;
				}

				ENDCG
			}
		}
		//fire prevention program???
		FallBack "Diffuse"
}