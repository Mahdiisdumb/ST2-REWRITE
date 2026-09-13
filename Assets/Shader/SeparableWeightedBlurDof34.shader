Shader "Hidden/SeparableWeightedBlurDof34" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_TapMedium ("TapMedium (RGB)", 2D) = "" {}
		_TapLow ("TapLow (RGB)", 2D) = "" {}
		_TapHigh ("TapHigh (RGB)", 2D) = "" {}
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
			GpuProgramID 51741
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 offsets;
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
                o.texcoord1 = offsets * float4(1.0, 1.0, -1.0, -1.0) + v.texcoord.xyxy;
                o.texcoord2 = offsets * float4(2.0, 2.0, -2.0, -2.0) + v.texcoord.xyxy;
                o.texcoord3 = offsets * float4(3.0, 3.0, -3.0, -3.0) + v.texcoord.xyxy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = tex2D(_MainTex, inp.texcoord1.zw);
                tmp1.xyz = tmp0.yyy * tmp0.xzw;
                tmp2 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.x = tmp2.w;
                tmp2.xyz = tmp2.xyz * float3(1.25, 1.25, 1.25);
                tmp3 = tex2D(_MainTex, inp.texcoord.xy);
                tmp2.xyz = tmp3.xyz * tmp3.www + tmp2.xyz;
                tmp1.xyz = tmp1.xyz * float3(1.25, 1.25, 1.25) + tmp2.xyz;
                tmp2 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.z = tmp2.w;
                tmp1.xyz = tmp2.xyz * float3(1.5, 1.5, 1.5) + tmp1.xyz;
                tmp2 = tex2D(_MainTex, inp.texcoord2.zw);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.w = tmp2.w;
                tmp0.x = dot(float4(1.25, 1.25, 1.5, 1.5), tmp0);
                tmp0.x = tmp0.x + tmp3.w;
                o.sv_target.w = tmp3.w;
                tmp0.yzw = tmp2.xyz * float3(1.5, 1.5, 1.5) + tmp1.xyz;
                o.sv_target.xyz = tmp0.yzw / tmp0.xxx;
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
			GpuProgramID 90920
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 offsets;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _TapHigh;
			
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
                o.texcoord1 = offsets * float4(1.0, 1.0, -1.0, -1.0) + v.texcoord.xyxy;
                o.texcoord2 = offsets * float4(2.0, 2.0, -2.0, -2.0) + v.texcoord.xyxy;
                o.texcoord3 = offsets * float4(3.0, 3.0, -3.0, -3.0) + v.texcoord.xyxy;
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
                tmp1 = tex2D(_MainTex, inp.texcoord1.zw);
                tmp0 = tmp0 + tmp1;
                tmp1 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp0 = tmp0 + tmp1;
                tmp1 = tex2D(_MainTex, inp.texcoord2.zw);
                tmp0 = tmp0 + tmp1;
                tmp0 = tmp0 * float4(0.2, 0.2, 0.2, 0.2);
                tmp1 = tex2D(_TapHigh, inp.texcoord.xy);
                o.sv_target.w = max(tmp0.w, tmp1.w);
                o.sv_target.xyz = tmp0.xyz;
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
			GpuProgramID 190829
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 offsets;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _TapHigh;
			
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
                o.texcoord1 = offsets * float4(1.0, 1.0, -1.0, -1.0) + v.texcoord.xyxy;
                o.texcoord2 = offsets * float4(2.0, 2.0, -2.0, -2.0) + v.texcoord.xyxy;
                o.texcoord3 = offsets * float4(3.0, 3.0, -3.0, -3.0) + v.texcoord.xyxy;
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
                tmp0 = tmp1 * float4(0.75, 0.75, 0.75, 0.75) + tmp0;
                tmp1 = tex2D(_MainTex, inp.texcoord1.zw);
                tmp0 = tmp1 * float4(0.75, 0.75, 0.75, 0.75) + tmp0;
                tmp1 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp0 = tmp1 * float4(0.5, 0.5, 0.5, 0.5) + tmp0;
                tmp1 = tex2D(_MainTex, inp.texcoord2.zw);
                tmp0 = tmp1 * float4(0.5, 0.5, 0.5, 0.5) + tmp0;
                tmp0 = tmp0 * float4(0.2857143, 0.2857143, 0.2857143, 0.2857143);
                tmp1 = tex2D(_TapHigh, inp.texcoord.xy);
                o.sv_target.w = max(tmp0.w, tmp1.w);
                o.sv_target.xyz = tmp0.xyz;
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
			GpuProgramID 210336
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
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _TapMedium;
			sampler2D _TapLow;
			
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
                float4 tmp1;
                tmp0 = tex2D(_TapMedium, inp.texcoord.xy);
                tmp0.w = tmp0.w * tmp0.w;
                tmp0.w = tmp0.w * tmp0.w;
                tmp1 = tex2D(_TapLow, inp.texcoord.xy);
                tmp1.xyz = tmp1.xyz - tmp0.xyz;
                o.sv_target.w = tmp1.w;
                o.sv_target.xyz = tmp0.www * tmp1.xyz + tmp0.xyz;
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
			GpuProgramID 298586
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 offsets;
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
                o.texcoord1 = offsets * float4(1.0, 1.0, -1.0, -1.0) + v.texcoord.xyxy;
                o.texcoord2 = offsets * float4(2.0, 2.0, -2.0, -2.0) + v.texcoord.xyxy;
                o.texcoord3 = offsets * float4(3.0, 3.0, -3.0, -3.0) + v.texcoord.xyxy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = tex2D(_MainTex, inp.texcoord1.zw);
                tmp1.xyz = tmp0.yyy * tmp0.xzw;
                tmp2 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.x = tmp2.w;
                tmp2.xyz = tmp2.xyz * float3(0.75, 0.75, 0.75);
                tmp3 = tex2D(_MainTex, inp.texcoord.xy);
                tmp2.xyz = tmp3.xyz * tmp3.www + tmp2.xyz;
                tmp1.xyz = tmp1.xyz * float3(0.75, 0.75, 0.75) + tmp2.xyz;
                tmp2 = tex2D(_MainTex, inp.texcoord2.xy);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.z = tmp2.w;
                tmp1.xyz = tmp2.xyz * float3(0.5, 0.5, 0.5) + tmp1.xyz;
                tmp2 = tex2D(_MainTex, inp.texcoord2.zw);
                tmp2.xyz = tmp2.www * tmp2.xyz;
                tmp0.w = tmp2.w;
                tmp0.x = dot(float4(0.75, 0.75, 0.5, 0.5), tmp0);
                tmp0.x = tmp0.x + tmp3.w;
                o.sv_target.w = tmp3.w;
                tmp0.yzw = tmp2.xyz * float3(0.5, 0.5, 0.5) + tmp1.xyz;
                o.sv_target.xyz = tmp0.yzw / tmp0.xxx;
                return o;
			}
			ENDCG
		}
	}
}