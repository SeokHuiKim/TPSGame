//got from http://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse
//modify by lect

Shader "Lect/CustomLight" {
	Properties{
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 0.05)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_TwinkleSpeed("TwinkleSpeed", Range(1, 10)) = 3
		_SpecCol ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecPow ("Specular Power", Range(10, 200)) = 100
		_FakeSpecCol("Specular2 Color", Color) = (0.2, 0.2, 0.2, 1)
		_FakeSpecPow("Specular2 Power", Range(10, 200)) = 100
		_GlossTex("Gloss Tex", 2D) = "white" {}
		_RimCol("RimColor", Color) = (0.5, 0.5, 0.5, 1)
		_RimPow("Rim Power", Range(0, 50)) = 30
	}

CGINCLUDE
#include "UnityCG.cginc"

struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};

uniform float _Outline;
uniform float4 _OutlineColor;
float _TwinkleSpeed;

v2f vert(appdata v) {
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);

	float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);

	o.pos.xy += offset * o.pos.z * _Outline;

	float alpha = (sin(_Time.y * _TwinkleSpeed) * 0.25 + 0.5);

	o.color = float4(_OutlineColor.r, _OutlineColor.g, _OutlineColor.b, alpha);
	return o;
}
ENDCG

	SubShader{
		Tags { "Queue" = "Transparent" }

		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Off
			ZWrite Off
			ZTest Always

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR {
				return i.color;
			}
			ENDCG
		}


		CGPROGRAM
		#pragma surface surf AA
		struct Input {
			float2 uv_MainTex, uv_BumpMap, uv_GlossTex;
		};

		sampler2D _MainTex, _BumpMap, _GlossTex;

		float4 _SpecCol, _RimCol, _FakeSpecCol;
		float _SpecPow, _RimPow, _FakeSpecPow;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			float4 m = tex2D(_GlossTex, IN.uv_GlossTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Gloss = m.a;
		}

		float4 LightingAA(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
			float4 final;

			//Lambert term
			float3 DiffColor;
			float ndotl = saturate(dot(s.Normal, lightDir));
			DiffColor = ndotl * s.Albedo * _LightColor0.rgb * atten;

			//Spec term
			float3 SpecColor;
			float3 H = normalize(lightDir + viewDir);
			float spec = saturate(dot(H, s.Normal));
			spec = pow(spec, 100);
			SpecColor = spec * _SpecCol.rgb * s.Gloss;

			//Rim term
			float3 rimColor;
			float rim = abs(dot(viewDir, s.Normal));
			float invrim = 1 - rim;
			rimColor = pow(invrim, _RimPow) * _RimCol.rgb;

			//Fake Spec term
			float3 SpecColor2;
			SpecColor2 = pow(rim, _FakeSpecPow) * _FakeSpecCol.rgb * s.Gloss;

			//Final term
			final.rgb = DiffColor.rgb + SpecColor.rgb + rimColor.rgb + SpecColor2.rgb;
			final.a = s.Alpha;

			return final;
		}
		ENDCG
	}	

	Fallback "Diffuse"
}