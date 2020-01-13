Shader "Lect/Hologram"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump" {}
		_RimColor("RimColor", Color) = (0, 0, 0, 0)
		_RimPower("RimPower", Range(1, 10)) = 3
		_TwinkleSpeed("TwinkleSpeed", Range(1, 10)) = 3
		_HoloInterval("HoloInterval", Range(1, 30)) = 10
		_HoloSpeed("HoloSpeed", Range(1, 20)) = 10
		_HoloLine("_HoloLine", Range(1, 10)) = 1
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

			CGPROGRAM
			#pragma surface surf nolight noambient alpha:fade

			sampler2D _MainTex, _BumpMap;
			float4 _RimColor;
			float _RimPower, _TwinkleSpeed, _HoloSpeed, _HoloInterval, _HoloLine;

			struct Input
			{
				float2 uv_MainTex, uv_BumpMap;
				float3 viewDir;
				float3 worldPos;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				float rim = saturate(dot(o.Normal, IN.viewDir));
				rim = pow(1 - rim, _RimPower) + pow(frac(
					IN.worldPos.g * _HoloLine - _Time.x * _HoloSpeed), _HoloInterval);
				o.Emission = _RimColor;
				o.Alpha = rim * (sin(_Time.y * _TwinkleSpeed) * 0.25 + 0.75);
			}

			float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
				return float4(0, 0, 0, s.Alpha);
			}
			ENDCG
		}
			FallBack "Transparent/Diffuse"
}
