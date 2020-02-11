Shader "Custom/Water2D"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Strength("Strength", Range(0,2)) = 1.0
		_Speed("Speed", Range(-200,200)) = 100
	}

		SubShader
		{
			Tags{"RenderType" = "Transparent"}

			// Regular color & lighting pass
			Pass
			{
				Cull Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				// Properties
				sampler2D _MainTex;
				float4 _Color;
				float  _Strength;
				float  _Speed;

				struct vertexInput
				{
					float4 vertex : POSITION; 
					float3 texCoord : TEXCOORD0;
				};

				struct vertexOutput
				{
					float4 pos : SV_POSITION;
					float3 texCoord : TEXCOORD0;
				};

				vertexOutput vert(vertexInput input)
				{
					vertexOutput output;

					float4 worldPos = mul(unity_ObjectToWorld, input.vertex); //Get world position of current vertex

					float displacement = (cos(worldPos.y) + cos((worldPos.x * 2) + (_Speed * _Time))); //Displace verticies on y-axis over a certain x-distance
					worldPos.y = worldPos.y + (displacement * _Strength);

					output.pos = mul(UNITY_MATRIX_VP, worldPos);
					output.texCoord = input.texCoord;

					return output;
				}

				float4 frag(vertexOutput input) : COLOR
				{
					float4 albedo = tex2D(_MainTex, input.texCoord.xy);
					return albedo;
				}

				ENDCG
			}
		}
}