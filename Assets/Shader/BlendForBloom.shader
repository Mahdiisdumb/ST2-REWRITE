Shader "Hidden/BlendForBloom" {
	Properties {
		_MainTex ("Screen Blended", 2D) = "" {}
		_ColorBuffer ("Color", 2D) = "" {}
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
			GpuProgramID 16839
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float _Intensity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0 = -tmp0 * _Intensity.xxxx + float4(1.0, 1.0, 1.0, 1.0);
                tmp1 = tex2D(_ColorBuffer, inp.texcoord1.xy);
                tmp1 = float4(1.0, 1.0, 1.0, 1.0) - tmp1;
                o.sv_target = -tmp0 * tmp1 + float4(1.0, 1.0, 1.0, 1.0);
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 121664
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float _Intensity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tex2D(_ColorBuffer, inp.texcoord1.xy);
                o.sv_target = _Intensity.xxxx * tmp0 + tmp1;
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 136852
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
				float2 texcoord3 : TEXCOORD3;
				float2 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
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
                o.texcoord.xy = _MainTex_TexelSize.xy * float2(0.5, 0.5) + v.texcoord.xy;
                o.texcoord1.xy = -_MainTex_TexelSize.xy * float2(0.5, 0.5) + v.texcoord.xy;
                o.texcoord2.xy = -_MainTex_TexelSize.xy * float2(0.5, -0.5) + v.texcoord.xy;
                o.texcoord3.xy = _MainTex_TexelSize.xy * float2(0.5, -0.5) + v.texcoord.xy;
                o.texcoord4.xy = v.texcoord.xy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord4.xy);
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0 = max(tmp0, tmp1);
                tmp1 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp0 = max(tmp0, tmp1);
                tmp1 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp0 = max(tmp0, tmp1);
                tmp1 = tex2D(_MainTex, inp.texcoord3.xy);
                o.sv_target = max(tmp0, tmp1);
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 221460
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tex2D(_ColorBuffer, inp.texcoord.xy);
                o.sv_target = tmp0 * tmp1;
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 281913
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float _Intensity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0 = -tmp0 * _Intensity.xxxx + float4(1.0, 1.0, 1.0, 1.0);
                tmp1 = tex2D(_ColorBuffer, inp.texcoord1.xy);
                tmp1 = float4(1.0, 1.0, 1.0, 1.0) - tmp1;
                o.sv_target = -tmp0 * tmp1 + float4(1.0, 1.0, 1.0, 1.0);
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 342369
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float _Intensity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tex2D(_ColorBuffer, inp.texcoord1.xy);
                o.sv_target = _Intensity.xxxx * tmp0 + tmp1;
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 418785
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
				float2 texcoord3 : TEXCOORD3;
				float2 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
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
                o.texcoord.xy = _MainTex_TexelSize.xy * float2(0.5, 0.5) + v.texcoord.xy;
                o.texcoord1.xy = -_MainTex_TexelSize.xy * float2(0.5, 0.5) + v.texcoord.xy;
                o.texcoord2.xy = -_MainTex_TexelSize.xy * float2(0.5, -0.5) + v.texcoord.xy;
                o.texcoord3.xy = _MainTex_TexelSize.xy * float2(0.5, -0.5) + v.texcoord.xy;
                o.texcoord4.xy = v.texcoord.xy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp0 = tmp0 + tmp1;
                tmp1 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp0 = tmp0 + tmp1;
                tmp1 = tex2D(_MainTex, inp.texcoord3.xy);
                tmp0 = tmp0 + tmp1;
                o.sv_target = tmp0 * float4(0.25, 0.25, 0.25, 0.25);
                return o;
			}
			ENDCG
		}
		Pass {
			Blend Zero SrcAlpha, Zero SrcAlpha
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 504143
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _ColorBuffer;
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0 = tex2D(_ColorBuffer, inp.texcoord.xy);
                o.sv_target.w = tmp0.x;
                o.sv_target.xyz = float3(1.0, 1.0, 1.0);
                return o;
			}
			ENDCG
		}
		Pass {
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 539133
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                o.sv_target = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			ENDCG
		}
		Pass {
			Blend One One, One One
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 624019
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float _Intensity;
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                o.sv_target = tmp0 * _Intensity.xxxx;
                return o;
			}
			ENDCG
		}
		Pass {
			Blend One One, One One
			BlendOp Max, Max
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 704747
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ColorBuffer_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
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
                tmp0.x = _ColorBuffer_TexelSize.y < 0.0;
                tmp0.y = 1.0 - v.texcoord.y;
                o.texcoord1.y = tmp0.x ? tmp0.y : v.texcoord.y;
                o.texcoord1.x = v.texcoord.x;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                o.sv_target = tex2D(_MainTex, inp.texcoord.xy);
                return o;
			}
			ENDCG
		}
	}
}