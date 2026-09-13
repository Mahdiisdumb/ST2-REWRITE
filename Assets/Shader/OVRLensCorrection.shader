Shader "OVRLensCorrection" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
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
			GpuProgramID 33999
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float2 _Center;
			float2 _ScaleIn;
			float2 _Scale;
			float4 _HmdWarpParam;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = unity_ObjectToWorld._m11_m11_m11_m11 * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * unity_ObjectToWorld._m01_m01_m01_m01 + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * unity_ObjectToWorld._m21_m21_m21_m21 + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * unity_ObjectToWorld._m31_m31_m31_m31 + tmp0;
                tmp0 = tmp0 * v.vertex.yyyy;
                tmp1 = unity_ObjectToWorld._m10_m10_m10_m10 * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * unity_ObjectToWorld._m00_m00_m00_m00 + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * unity_ObjectToWorld._m20_m20_m20_m20 + tmp1;
                tmp1 = unity_MatrixVP._m03_m13_m23_m33 * unity_ObjectToWorld._m30_m30_m30_m30 + tmp1;
                tmp0 = tmp1 * v.vertex.xxxx + tmp0;
                tmp1 = unity_ObjectToWorld._m12_m12_m12_m12 * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * unity_ObjectToWorld._m02_m02_m02_m02 + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * unity_ObjectToWorld._m22_m22_m22_m22 + tmp1;
                tmp1 = unity_MatrixVP._m03_m13_m23_m33 * unity_ObjectToWorld._m32_m32_m32_m32 + tmp1;
                tmp0 = tmp1 * v.vertex.zzzz + tmp0;
                tmp1 = unity_ObjectToWorld._m13_m13_m13_m13 * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * unity_ObjectToWorld._m03_m03_m03_m03 + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * unity_ObjectToWorld._m23_m23_m23_m23 + tmp1;
                tmp1 = unity_MatrixVP._m03_m13_m23_m33 * unity_ObjectToWorld._m33_m33_m33_m33 + tmp1;
                o.sv_position = tmp1 * v.vertex.wwww + tmp0;
                o.texcoord.xy = v.texcoord.xy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0.xy = inp.texcoord.xy - _Center;
                tmp0.xy = tmp0.xy * _ScaleIn;
                tmp0.z = dot(tmp0.xy, tmp0.xy);
                tmp0.w = _HmdWarpParam.y * tmp0.z + _HmdWarpParam.x;
                tmp0.z = tmp0.z * tmp0.z;
                tmp0.z = tmp0.z * _HmdWarpParam.z + tmp0.w;
                tmp0.xy = tmp0.zz * tmp0.xy;
                tmp0.xy = _Scale * tmp0.xy + _Center;
                tmp0.zw = saturate(tmp0.xy);
                tmp0.zw = tmp0.zw - tmp0.xy;
                tmp0.z = dot(tmp0.xy, tmp0.xy);
                tmp0.z = tmp0.z != 0.0;
                if (tmp0.z) {
                    o.sv_target = float4(0.0, 0.0, 0.0, 0.0);
                } else {
                    o.sv_target = tex2D(_MainTex, tmp0.xy);
                }
                return o;
			}
			ENDCG
		}
	}
}