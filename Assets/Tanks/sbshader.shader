Shader "Custom/Tanks" {
	Properties
	{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_BackGround("BackGround", 2D) = "white" {}
		[KeywordEnum(OFF,ON,NULL)] _CLIPPING("_CLIPPING", Float) = 0
		[KeywordEnum(OFF,ON,NULL)] _HAVECOLOR("_HAVECOLOR", Float) = 0
	}
		SubShader
		{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}

			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite off

			Pass
			{
				CGPROGRAM
				#include "UnityCustomRenderTexture.cginc"
				#pragma vertex InitCustomRenderTextureVertexShader
				#pragma fragment frag
				#pragma shader_feature _CLIPPING_ON
				#pragma shader_feature _CLIPPING_OFF
				#pragma shader_feature _CLIPPING_NULL

				#pragma shader_feature _HAVECOLOR_ON
				#pragma shader_feature _HAVECOLOR_OFF
				#pragma shader_feature _HAVECOLOR_NULL
				sampler2D _MainTex;
				sampler2D _BackGround;
				float4 _MainTex_ST;
				float4 _BackGround_ST;

				fixed4 _Color;

				struct v2f
				{
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				fixed4 frag(v2f_customrendertexture i) : COLOR
				{
					// sample the texture
					float2 uv = i.globalTexcoord;
					fixed4 color = tex2D(_MainTex, i.localTexcoord.xy);
					fixed4 color1 = tex2D(_BackGround,i.localTexcoord.xy);
					fixed4 Mixcolors;
					fixed scale = 0.2;
					fixed alpha = dot(color1.rgb, fixed3(.222, .707, .071)) * scale;
					color1 = color1 * scale;
					fixed maxc = max(max(color1.r, color1.g), color1.b);
					alpha = max(1 - color.r + alpha, maxc);
					fixed r1 = color1.r / alpha;
					fixed g1 = color1.g / alpha;
					fixed b1 = color1.b / alpha;
					Mixcolors = fixed4(r1, g1, b1, alpha);
					return Mixcolors;
				
				}
				ENDCG
			}
		}
}
