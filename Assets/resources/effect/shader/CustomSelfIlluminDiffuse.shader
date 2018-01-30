// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Legacy Shaders/Self-Illumin/Diffuse" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Illum("Illumin (A)", 2D) = "white" {}
		_RltTex("RltTex", 2D) = "black"{}
		//[MaterialToggle]_Relation("Enemy", Int) = 0
		_Emission("Emission (Lightmapper)", Float) = 0
	}
		SubShader{
			Tags { "RenderType" = "Transparent" }
			LOD 200

		CGPROGRAM
		#pragma surface surf Unlit alphatest:_Cutoff

		half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten) {
			return half4(s.Albedo, s.Alpha);
		}

		sampler2D _MainTex;
		sampler2D _Illum;
		sampler2D _RltTex;
		fixed4 _Color;
		fixed _Emission;
		//int _Relation;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
		};

		float3 RGB2HSV(float3 c) {
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
			float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

			float d = q.x - min(q.w, q.y);
			float e = 1.0e-10;
			return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		float3 HSV2RGB(float3 c) {
			float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			/*fixed4 r = tex2D(_RltTex, IN.uv_MainTex);
			fixed3 hsv = RGB2HSV(c.rgb);
			hsv.x = (1 - _Relation) * hsv.x;
			o.Albedo = r * HSV2RGB(hsv) + (1 - r) * c.rgb;*/
			o.Albedo = c.rgb;
			o.Emission = _Emission;// o.Albedo * tex2D(_Illum, IN.uv_Illum).a;
		#if defined (UNITY_PASS_META)
			o.Emission = _Emission;
		#endif
			o.Alpha = c.a;
		}

		ENDCG
		}
			FallBack "Legacy Shaders/Self-Illumin/VertexLit"
			CustomEditor "LegacyIlluminShaderGUI"
}
