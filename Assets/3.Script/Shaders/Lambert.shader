Shader "Lect/Lambert"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "bump" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Test

		sampler2D _MainTex, _BumpMap;

		struct Input
		{
			float2 uv_MainTex, uv_BumpMap;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		float4 LightingTest(SurfaceOutput s, float3 lightDir, float atten) {
			float ndotl = saturate(dot(s.Normal, lightDir)) * 0.5 + 0.5;
			float4 final;
			final.rgb = pow(ndotl, 3) * s.Albedo * _LightColor0.rgb * atten;
			final.a = s.Alpha;
			return final;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
