Shader "Hidden/Aubergine/NightVisionV2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Noise Map", 2D) = "white" {}
		_NoiseAmount ("Noise Amount", Float) = 0.9
		_LumThreshold ("LumThreshold", Float) = 0.2
		_BrightenFactor ("BrightenFactor", Float) = 2
		_VisionColor ("Vision Color", Vector) = (0.1,0.95,0.2,1)
	}
	SubShader {
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 45693
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float _NoiseAmount;
			float _LumThreshold;
			float _BrightenFactor;
			float4 _VisionColor;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.texcoord.xy = v.texcoord.xy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0.x = _Time.y * 50.0;
                tmp0.x = sin(tmp0.x);
                tmp0.xy = tmp0.xx + inp.texcoord.xy;
                tmp0.xy = tmp0.xy * _NoiseAmount.xx;
                tmp0 = tex2D(_NoiseTex, tmp0.xy);
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0.w = dot(tmp1.xyz, float3(0.3, 0.59, 0.11));
                tmp0.w = tmp0.w < _LumThreshold;
                tmp2.xyz = tmp1.xyz * _BrightenFactor.xxx;
                tmp1.xyz = tmp0.www ? tmp2.xyz : tmp1.xyz;
                o.sv_target.w = tmp1.w;
                o.sv_target.xyz = saturate(tmp1.xyz * _VisionColor.xyz + tmp0.xyz);
                return o;
			}
			ENDCG
		}
	}
}