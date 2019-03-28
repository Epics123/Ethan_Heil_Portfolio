Shader "Custom/SpriteOutlineShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"{}
		_Color("Color", Color) = (1, 1, 1, 1)
	}

		SubShader
		{
			Cull Off //Does not ignore any pixels
			Blend One OneMinusSrcAlpha

			Pass
			{
			   CGPROGRAM

				 #pragma vertex vertexFunction
				 #pragma fragment fragmentFunction
				 #include "UnityCG.cginc"

				 sampler2D _MainTex;

				 struct v2f
				 {
					 float4 pos : SV_POSITION;
					 half4 uv : TEXCOORD0;
				 };

				 v2f vertexFunction(appdata_base v)
				 {
					 v2f output;
					 output.pos = UnityObjectToClipPos(v.vertex);//Converts mesh space to camera space
					 output.uv = v.texcoord;
					 return output;
				 }

				 fixed4 _Color;
				 float4 _MainTex_TexelSize;//Size information for texture

				 fixed4 fragmentFunction(v2f input) : COLOR
				 {
					 half4 color = tex2D(_MainTex, input.uv); //Gets the color of the current pixel
					 color.rgb *= 0;//color.a; //Multiply each pixel's color by it's alpha
					 half4 outlineColor = _Color;
					 outlineColor.a *= ceil(color.a);//Rounds outline alpha to a whole number
					 outlineColor.rgb *= outlineColor.a;

					 fixed upAlpha = tex2D(_MainTex, input.uv + fixed2(0, 8 * _MainTex_TexelSize.y)).a;//Gets alpha of 8 pixels above current pixel
					 fixed downAlpha = tex2D(_MainTex, input.uv - fixed2(0, 8 * _MainTex_TexelSize.y)).a;//Gets alpha of 8 pixels below current pixel
					 fixed rightAlpha = tex2D(_MainTex, input.uv + fixed2(8 * _MainTex_TexelSize.x, 0)).a;//Gets alpha of 8 pixels to the right of the current pixel
					 fixed leftAlpha = tex2D(_MainTex, input.uv - fixed2(8 * _MainTex_TexelSize.x, 0)).a;//Gets alpha of 8 pixels to the left of the current pixel

					 return lerp(outlineColor, color, ceil(upAlpha * downAlpha * rightAlpha * leftAlpha));//If 0, returns outline color, if 1, returns normal pixel color
				 }

			   ENDCG
			}
		}
}