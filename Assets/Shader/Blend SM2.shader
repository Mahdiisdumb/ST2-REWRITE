Shader "Enviro/BumpedDiffuseOverlaySM2" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_Opacity ("Color over opacity", Range(0, 1)) = 1
		_MainTex ("Color over (RGBA)", 2D) = "white" {}
		_BumpMap ("Normalmap over", 2D) = "bump" {}
		_MainTex2 ("Color under (RGBA)", 2D) = "white" {}
		_BumpMap2 ("Normalmap under", 2D) = "bump" {}
	}
	SubShader {
		LOD 400
		Tags { "RenderType" = "Opaque" }
		Pass {
			Name "FORWARD"
			LOD 400
			Tags { "LIGHTMODE" = "FORWARDBASE" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
			ZClip Off
			GpuProgramID 33534
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord4 : TEXCOORD4;
				float3 texcoord5 : TEXCOORD5;
				float2 texcoord6 : TEXCOORD6;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float4 _MainTex2_ST;
			float4 _BumpMap2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _LightColor0;
			float4 _Color;
			float _Opacity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainTex2;
			sampler2D _BumpMap;
			sampler2D _BumpMap2;
			
			// Keywords: DIRECTIONAL
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                o.texcoord1.xy = v.texcoord.xy * _MainTex2_ST.xy + _MainTex2_ST.zw;
                o.texcoord1.zw = v.texcoord.xy * _BumpMap2_ST.xy + _BumpMap2_ST.zw;
                o.texcoord2.w = tmp0.x;
                tmp1.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp1.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp1.xyz;
                tmp1.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp1.xyz;
                tmp0.x = dot(tmp1.xyz, tmp1.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp1.xyz = tmp0.xxx * tmp1.xyz;
                o.texcoord2.x = tmp1.z;
                tmp0.x = v.tangent.w * unity_WorldTransformParams.w;
                tmp2.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp2.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp2.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp2.xyz, tmp2.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp2.xyz = tmp0.www * tmp2.xyz;
                tmp3.xyz = tmp1.xyz * tmp2.zxy;
                tmp3.xyz = tmp2.yzx * tmp1.yzx + -tmp3.xyz;
                tmp3.xyz = tmp0.xxx * tmp3.xyz;
                o.texcoord2.y = tmp3.x;
                o.texcoord2.z = tmp2.x;
                o.texcoord3.x = tmp1.x;
                o.texcoord4.x = tmp1.y;
                o.texcoord3.w = tmp0.y;
                o.texcoord4.w = tmp0.z;
                o.texcoord3.y = tmp3.y;
                o.texcoord4.y = tmp3.z;
                o.texcoord3.z = tmp2.y;
                o.texcoord4.z = tmp2.z;
                tmp0.x = tmp2.y * tmp2.y;
                tmp0.x = tmp2.x * tmp2.x + -tmp0.x;
                tmp1 = tmp2.yzzx * tmp2.xyzz;
                tmp3.x = dot(unity_SHBr, tmp1);
                tmp3.y = dot(unity_SHBg, tmp1);
                tmp3.z = dot(unity_SHBb, tmp1);
                tmp0.xyz = unity_SHC.xyz * tmp0.xxx + tmp3.xyz;
                tmp2.w = 1.0;
                tmp1.x = dot(unity_SHAr, tmp2);
                tmp1.y = dot(unity_SHAg, tmp2);
                tmp1.z = dot(unity_SHAb, tmp2);
                tmp0.xyz = tmp0.xyz + tmp1.xyz;
                tmp0.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
                tmp0.xyz = log(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(0.4166667, 0.4166667, 0.4166667);
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(1.055, 1.055, 1.055) + float3(-0.055, -0.055, -0.055);
                o.texcoord5.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
                o.texcoord6.xy = float2(0.0, 0.0);
                return o;
			}
			// Keywords: DIRECTIONAL
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tex2D(_MainTex2, inp.texcoord1.xy);
                tmp0.w = tmp0.w * _Opacity;
                tmp2.xyz = tmp1.xyz <= float3(0.5, 0.5, 0.5);
                tmp3.xyz = tmp0.xyz + tmp0.xyz;
                tmp3.xyz = tmp1.xyz * tmp3.xyz;
                tmp0.xyz = float3(1.0, 1.0, 1.0) - tmp0.xyz;
                tmp0.xyz = tmp0.xyz + tmp0.xyz;
                tmp4.xyz = float3(1.0, 1.0, 1.0) - tmp1.xyz;
                tmp0.xyz = -tmp0.xyz * tmp4.xyz + float3(1.0, 1.0, 1.0);
                tmp0.xyz = tmp2.xyz ? tmp3.xyz : tmp0.xyz;
                tmp0.xyz = tmp0.xyz - tmp1.xyz;
                tmp0.xyz = tmp0.www * tmp0.xyz + tmp1.xyz;
                tmp0.xyz = tmp0.xyz * _Color.xyz;
                tmp1 = tex2D(_BumpMap, inp.texcoord.zw);
                tmp2 = tex2D(_BumpMap2, inp.texcoord1.zw);
                tmp1.xz = tmp2.wy <= float2(0.5, 0.5);
                tmp2.xz = tmp1.wy + tmp1.wy;
                tmp2.xz = tmp2.wy * tmp2.xz;
                tmp1.yw = float2(1.0, 1.0) - tmp1.wy;
                tmp1.yw = tmp1.yw + tmp1.yw;
                tmp3.xy = float2(1.0, 1.0) - tmp2.wy;
                tmp1.yw = -tmp1.yw * tmp3.xy + float2(1.0, 1.0);
                tmp1.xy = tmp1.xz ? tmp2.xz : tmp1.yw;
                tmp1.xy = tmp1.xy - tmp2.wy;
                tmp1.xy = tmp0.ww * tmp1.xy + tmp2.wy;
                tmp1.xy = tmp1.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp0.w = dot(tmp1.xy, tmp1.xy);
                tmp0.w = min(tmp0.w, 1.0);
                tmp0.w = 1.0 - tmp0.w;
                tmp1.z = sqrt(tmp0.w);
                tmp0.w = unity_ProbeVolumeParams.x == 1.0;
                if (tmp0.w) {
                    tmp0.w = unity_ProbeVolumeParams.y == 1.0;
                    tmp2.xyz = inp.texcoord3.www * unity_ProbeVolumeWorldToObject._m01_m11_m21;
                    tmp2.xyz = unity_ProbeVolumeWorldToObject._m00_m10_m20 * inp.texcoord2.www + tmp2.xyz;
                    tmp2.xyz = unity_ProbeVolumeWorldToObject._m02_m12_m22 * inp.texcoord4.www + tmp2.xyz;
                    tmp2.xyz = tmp2.xyz + unity_ProbeVolumeWorldToObject._m03_m13_m23;
                    tmp3.y = inp.texcoord2.w;
                    tmp3.z = inp.texcoord3.w;
                    tmp3.w = inp.texcoord4.w;
                    tmp2.xyz = tmp0.www ? tmp2.xyz : tmp3.yzw;
                    tmp2.xyz = tmp2.xyz - unity_ProbeVolumeMin;
                    tmp2.yzw = tmp2.xyz * unity_ProbeVolumeSizeInv;
                    tmp0.w = tmp2.y * 0.25 + 0.75;
                    tmp1.w = unity_ProbeVolumeParams.z * 0.5 + 0.75;
                    tmp2.x = max(tmp0.w, tmp1.w);
                    tmp2 = UNITY_SAMPLE_TEX3D_SAMPLER(unity_ProbeVolumeSH, unity_ProbeVolumeSH, tmp2.xzw);
                } else {
                    tmp2 = float4(1.0, 1.0, 1.0, 1.0);
                }
                tmp0.w = saturate(dot(tmp2, unity_OcclusionMaskSelector));
                tmp2.x = dot(inp.texcoord2.xyz, tmp1.xyz);
                tmp2.y = dot(inp.texcoord3.xyz, tmp1.xyz);
                tmp2.z = dot(inp.texcoord4.xyz, tmp1.xyz);
                tmp1.xyz = tmp0.www * _LightColor0.xyz;
                tmp0.w = dot(tmp2.xyz, _WorldSpaceLightPos0.xyz);
                tmp0.w = max(tmp0.w, 0.0);
                tmp1.xyz = tmp0.xyz * tmp1.xyz;
                tmp0.xyz = tmp0.xyz * inp.texcoord5.xyz;
                o.sv_target.xyz = tmp1.xyz * tmp0.www + tmp0.xyz;
                o.sv_target.w = 1.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "FORWARD"
			LOD 400
			Tags { "LIGHTMODE" = "FORWARDADD" "RenderType" = "Opaque" }
			Blend One One, One One
			ZClip Off
			ZWrite Off
			GpuProgramID 100800
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float3 texcoord2 : TEXCOORD2;
				float3 texcoord3 : TEXCOORD3;
				float3 texcoord4 : TEXCOORD4;
				float3 texcoord5 : TEXCOORD5;
				float2 texcoord6 : TEXCOORD6;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float4 _MainTex2_ST;
			float4 _BumpMap2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4x4 unity_WorldToLight;
			float4 _LightColor0;
			float4 _Color;
			float _Opacity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainTex2;
			sampler2D _BumpMap;
			sampler2D _BumpMap2;
			sampler2D _LightTexture0;
			
			// Keywords: POINT
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord5.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                o.texcoord1.xy = v.texcoord.xy * _MainTex2_ST.xy + _MainTex2_ST.zw;
                o.texcoord1.zw = v.texcoord.xy * _BumpMap2_ST.xy + _BumpMap2_ST.zw;
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp1.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp1.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp1.xyz;
                tmp1.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp1.xyz;
                tmp0.w = dot(tmp1.xyz, tmp1.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp1.xyz = tmp0.www * tmp1.xyz;
                tmp2.xyz = tmp0.xyz * tmp1.xyz;
                tmp2.xyz = tmp0.zxy * tmp1.yzx + -tmp2.xyz;
                tmp0.w = v.tangent.w * unity_WorldTransformParams.w;
                tmp2.xyz = tmp0.www * tmp2.xyz;
                o.texcoord2.y = tmp2.x;
                o.texcoord2.x = tmp1.z;
                o.texcoord2.z = tmp0.y;
                o.texcoord3.x = tmp1.x;
                o.texcoord4.x = tmp1.y;
                o.texcoord3.z = tmp0.z;
                o.texcoord4.z = tmp0.x;
                o.texcoord3.y = tmp2.y;
                o.texcoord4.y = tmp2.z;
                o.texcoord6.xy = float2(0.0, 0.0);
                return o;
			}
			// Keywords: POINT
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                tmp0.xyz = _WorldSpaceLightPos0.xyz - inp.texcoord5.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp2 = tex2D(_MainTex2, inp.texcoord1.xy);
                tmp0.w = tmp1.w * _Opacity;
                tmp3.xyz = tmp2.xyz <= float3(0.5, 0.5, 0.5);
                tmp4.xyz = tmp1.xyz + tmp1.xyz;
                tmp4.xyz = tmp2.xyz * tmp4.xyz;
                tmp1.xyz = float3(1.0, 1.0, 1.0) - tmp1.xyz;
                tmp1.xyz = tmp1.xyz + tmp1.xyz;
                tmp5.xyz = float3(1.0, 1.0, 1.0) - tmp2.xyz;
                tmp1.xyz = -tmp1.xyz * tmp5.xyz + float3(1.0, 1.0, 1.0);
                tmp1.xyz = tmp3.xyz ? tmp4.xyz : tmp1.xyz;
                tmp1.xyz = tmp1.xyz - tmp2.xyz;
                tmp1.xyz = tmp0.www * tmp1.xyz + tmp2.xyz;
                tmp1.xyz = tmp1.xyz * _Color.xyz;
                tmp2 = tex2D(_BumpMap, inp.texcoord.zw);
                tmp3 = tex2D(_BumpMap2, inp.texcoord1.zw);
                tmp2.xz = tmp3.wy <= float2(0.5, 0.5);
                tmp3.xz = tmp2.wy + tmp2.wy;
                tmp3.xz = tmp3.wy * tmp3.xz;
                tmp2.yw = float2(1.0, 1.0) - tmp2.wy;
                tmp2.yw = tmp2.yw + tmp2.yw;
                tmp4.xy = float2(1.0, 1.0) - tmp3.wy;
                tmp2.yw = -tmp2.yw * tmp4.xy + float2(1.0, 1.0);
                tmp2.xy = tmp2.xz ? tmp3.xz : tmp2.yw;
                tmp2.xy = tmp2.xy - tmp3.wy;
                tmp2.xy = tmp0.ww * tmp2.xy + tmp3.wy;
                tmp2.xy = tmp2.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp0.w = dot(tmp2.xy, tmp2.xy);
                tmp0.w = min(tmp0.w, 1.0);
                tmp0.w = 1.0 - tmp0.w;
                tmp2.z = sqrt(tmp0.w);
                tmp3.xyz = inp.texcoord5.yyy * unity_WorldToLight._m01_m11_m21;
                tmp3.xyz = unity_WorldToLight._m00_m10_m20 * inp.texcoord5.xxx + tmp3.xyz;
                tmp3.xyz = unity_WorldToLight._m02_m12_m22 * inp.texcoord5.zzz + tmp3.xyz;
                tmp3.xyz = tmp3.xyz + unity_WorldToLight._m03_m13_m23;
                tmp0.w = unity_ProbeVolumeParams.x == 1.0;
                if (tmp0.w) {
                    tmp0.w = unity_ProbeVolumeParams.y == 1.0;
                    tmp4.xyz = inp.texcoord5.yyy * unity_ProbeVolumeWorldToObject._m01_m11_m21;
                    tmp4.xyz = unity_ProbeVolumeWorldToObject._m00_m10_m20 * inp.texcoord5.xxx + tmp4.xyz;
                    tmp4.xyz = unity_ProbeVolumeWorldToObject._m02_m12_m22 * inp.texcoord5.zzz + tmp4.xyz;
                    tmp4.xyz = tmp4.xyz + unity_ProbeVolumeWorldToObject._m03_m13_m23;
                    tmp4.xyz = tmp0.www ? tmp4.xyz : inp.texcoord5.xyz;
                    tmp4.xyz = tmp4.xyz - unity_ProbeVolumeMin;
                    tmp4.yzw = tmp4.xyz * unity_ProbeVolumeSizeInv;
                    tmp0.w = tmp4.y * 0.25 + 0.75;
                    tmp1.w = unity_ProbeVolumeParams.z * 0.5 + 0.75;
                    tmp4.x = max(tmp0.w, tmp1.w);
                    tmp4 = UNITY_SAMPLE_TEX3D_SAMPLER(unity_ProbeVolumeSH, unity_ProbeVolumeSH, tmp4.xzw);
                } else {
                    tmp4 = float4(1.0, 1.0, 1.0, 1.0);
                }
                tmp0.w = saturate(dot(tmp4, unity_OcclusionMaskSelector));
                tmp1.w = dot(tmp3.xyz, tmp3.xyz);
                tmp3 = tex2D(_LightTexture0, tmp1.ww);
                tmp0.w = tmp0.w * tmp3.x;
                tmp3.x = dot(inp.texcoord2.xyz, tmp2.xyz);
                tmp3.y = dot(inp.texcoord3.xyz, tmp2.xyz);
                tmp3.z = dot(inp.texcoord4.xyz, tmp2.xyz);
                tmp2.xyz = tmp0.www * _LightColor0.xyz;
                tmp0.x = dot(tmp3.xyz, tmp0.xyz);
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.yzw = tmp1.xyz * tmp2.xyz;
                o.sv_target.xyz = tmp0.xxx * tmp0.yzw;
                o.sv_target.w = 1.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "PREPASS"
			LOD 400
			Tags { "LIGHTMODE" = "PREPASSBASE" "RenderType" = "Opaque" }
			ZClip Off
			GpuProgramID 196438
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float4 _BumpMap2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float _Opacity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _BumpMap2;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                o.texcoord1.xy = v.texcoord.xy * _BumpMap2_ST.xy + _BumpMap2_ST.zw;
                o.texcoord2.w = tmp0.x;
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.x = dot(tmp1.xyz, tmp1.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp1.xyz = tmp0.xxx * tmp1.xyz;
                tmp2.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp2.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp2.xyz;
                tmp2.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp2.xyz;
                tmp0.x = dot(tmp2.xyz, tmp2.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp2.xyz = tmp0.xxx * tmp2.xyz;
                tmp3.xyz = tmp1.xyz * tmp2.xyz;
                tmp3.xyz = tmp1.zxy * tmp2.yzx + -tmp3.xyz;
                tmp0.x = v.tangent.w * unity_WorldTransformParams.w;
                tmp3.xyz = tmp0.xxx * tmp3.xyz;
                o.texcoord2.y = tmp3.x;
                o.texcoord2.x = tmp2.z;
                o.texcoord2.z = tmp1.y;
                o.texcoord3.x = tmp2.x;
                o.texcoord4.x = tmp2.y;
                o.texcoord3.z = tmp1.z;
                o.texcoord4.z = tmp1.x;
                o.texcoord3.w = tmp0.y;
                o.texcoord4.w = tmp0.z;
                o.texcoord3.y = tmp3.y;
                o.texcoord4.y = tmp3.z;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0 = tex2D(_BumpMap, inp.texcoord.zw);
                tmp0.xz = float2(1.0, 1.0) - tmp0.wy;
                tmp0 = tmp0.xwzy + tmp0.xwzy;
                tmp1 = tex2D(_BumpMap2, inp.texcoord1.xy);
                tmp1.xz = float2(1.0, 1.0) - tmp1.wy;
                tmp0.xz = -tmp0.xz * tmp1.xz + float2(1.0, 1.0);
                tmp0.yw = tmp0.yw * tmp1.wy;
                tmp1.xz = tmp1.wy <= float2(0.5, 0.5);
                tmp0.xy = tmp1.xz ? tmp0.yw : tmp0.xz;
                tmp0.xy = tmp0.xy - tmp1.wy;
                tmp2 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0.z = tmp2.w * _Opacity;
                tmp0.xy = tmp0.zz * tmp0.xy + tmp1.wy;
                tmp0.xy = tmp0.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp0.w = dot(tmp0.xy, tmp0.xy);
                tmp0.w = min(tmp0.w, 1.0);
                tmp0.w = 1.0 - tmp0.w;
                tmp0.z = sqrt(tmp0.w);
                tmp1.x = dot(inp.texcoord2.xyz, tmp0.xyz);
                tmp1.y = dot(inp.texcoord3.xyz, tmp0.xyz);
                tmp1.z = dot(inp.texcoord4.xyz, tmp0.xyz);
                o.sv_target.xyz = tmp1.xyz * float3(0.5, 0.5, 0.5) + float3(0.5, 0.5, 0.5);
                o.sv_target.w = 0.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "PREPASS"
			LOD 400
			Tags { "LIGHTMODE" = "PREPASSFINAL" "RenderType" = "Opaque" }
			ZClip Off
			ZWrite Off
			GpuProgramID 213560
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float3 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float3 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _MainTex2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _Color;
			float _Opacity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainTex2;
			sampler2D _LightBuffer;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _MainTex2_ST.xy + _MainTex2_ST.zw;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord2.zw = tmp0.zw;
                o.texcoord2.xy = tmp1.zz + tmp1.xw;
                o.texcoord3 = float4(0.0, 0.0, 0.0, 0.0);
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp1.x = tmp0.y * tmp0.y;
                tmp1.x = tmp0.x * tmp0.x + -tmp1.x;
                tmp2 = tmp0.yzzx * tmp0.xyzz;
                tmp3.x = dot(unity_SHBr, tmp2);
                tmp3.y = dot(unity_SHBg, tmp2);
                tmp3.z = dot(unity_SHBb, tmp2);
                tmp1.xyz = unity_SHC.xyz * tmp1.xxx + tmp3.xyz;
                tmp0.w = 1.0;
                tmp2.x = dot(unity_SHAr, tmp0);
                tmp2.y = dot(unity_SHAg, tmp0);
                tmp2.z = dot(unity_SHAb, tmp0);
                tmp0.xyz = tmp1.xyz + tmp2.xyz;
                tmp0.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
                tmp0.xyz = log(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(0.4166667, 0.4166667, 0.4166667);
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(1.055, 1.055, 1.055) + float3(-0.055, -0.055, -0.055);
                o.texcoord4.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
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
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1.xyz = float3(1.0, 1.0, 1.0) - tmp0.xyz;
                tmp1.xyz = tmp1.xyz + tmp1.xyz;
                tmp2 = tex2D(_MainTex2, inp.texcoord.zw);
                tmp3.xyz = float3(1.0, 1.0, 1.0) - tmp2.xyz;
                tmp1.xyz = -tmp1.xyz * tmp3.xyz + float3(1.0, 1.0, 1.0);
                tmp0.xyz = tmp0.xyz * tmp2.xyz;
                tmp0.w = tmp0.w * _Opacity;
                tmp0.xyz = tmp0.xyz + tmp0.xyz;
                tmp3.xyz = tmp2.xyz <= float3(0.5, 0.5, 0.5);
                tmp0.xyz = tmp3.xyz ? tmp0.xyz : tmp1.xyz;
                tmp0.xyz = tmp0.xyz - tmp2.xyz;
                tmp0.xyz = tmp0.www * tmp0.xyz + tmp2.xyz;
                tmp0.xyz = tmp0.xyz * _Color.xyz;
                tmp1.xy = inp.texcoord2.xy / inp.texcoord2.ww;
                tmp1 = tex2D(_LightBuffer, tmp1.xy);
                tmp1.xyz = log(tmp1.xyz);
                tmp1.xyz = inp.texcoord4.xyz - tmp1.xyz;
                o.sv_target.xyz = tmp0.xyz * tmp1.xyz;
                o.sv_target.w = 1.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "DEFERRED"
			LOD 400
			Tags { "LIGHTMODE" = "DEFERRED" "RenderType" = "Opaque" }
			ZClip Off
			GpuProgramID 263047
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord4 : TEXCOORD4;
				float4 texcoord5 : TEXCOORD5;
				float3 texcoord6 : TEXCOORD6;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
				float4 sv_target1 : SV_Target1;
				float4 sv_target2 : SV_Target2;
				float4 sv_target3 : SV_Target3;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float4 _MainTex2_ST;
			float4 _BumpMap2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _Color;
			float _Opacity;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainTex2;
			sampler2D _BumpMap;
			sampler2D _BumpMap2;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                o.texcoord1.xy = v.texcoord.xy * _MainTex2_ST.xy + _MainTex2_ST.zw;
                o.texcoord1.zw = v.texcoord.xy * _BumpMap2_ST.xy + _BumpMap2_ST.zw;
                o.texcoord2.w = tmp0.x;
                tmp1.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp1.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp1.xyz;
                tmp1.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp1.xyz;
                tmp0.x = dot(tmp1.xyz, tmp1.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp1.xyz = tmp0.xxx * tmp1.xyz;
                o.texcoord2.x = tmp1.z;
                tmp0.x = v.tangent.w * unity_WorldTransformParams.w;
                tmp2.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp2.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp2.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp2.xyz, tmp2.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp2.xyz = tmp0.www * tmp2.xyz;
                tmp3.xyz = tmp1.xyz * tmp2.zxy;
                tmp3.xyz = tmp2.yzx * tmp1.yzx + -tmp3.xyz;
                tmp3.xyz = tmp0.xxx * tmp3.xyz;
                o.texcoord2.y = tmp3.x;
                o.texcoord2.z = tmp2.x;
                o.texcoord3.x = tmp1.x;
                o.texcoord4.x = tmp1.y;
                o.texcoord3.w = tmp0.y;
                o.texcoord4.w = tmp0.z;
                o.texcoord3.y = tmp3.y;
                o.texcoord4.y = tmp3.z;
                o.texcoord3.z = tmp2.y;
                o.texcoord4.z = tmp2.z;
                o.texcoord5 = float4(0.0, 0.0, 0.0, 0.0);
                tmp0.x = tmp2.y * tmp2.y;
                tmp0.x = tmp2.x * tmp2.x + -tmp0.x;
                tmp1 = tmp2.yzzx * tmp2.xyzz;
                tmp3.x = dot(unity_SHBr, tmp1);
                tmp3.y = dot(unity_SHBg, tmp1);
                tmp3.z = dot(unity_SHBb, tmp1);
                tmp0.xyz = unity_SHC.xyz * tmp0.xxx + tmp3.xyz;
                tmp2.w = 1.0;
                tmp1.x = dot(unity_SHAr, tmp2);
                tmp1.y = dot(unity_SHAg, tmp2);
                tmp1.z = dot(unity_SHAb, tmp2);
                tmp0.xyz = tmp0.xyz + tmp1.xyz;
                tmp0.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
                tmp0.xyz = log(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(0.4166667, 0.4166667, 0.4166667);
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * float3(1.055, 1.055, 1.055) + float3(-0.055, -0.055, -0.055);
                o.texcoord6.xyz = max(tmp0.xyz, float3(0.0, 0.0, 0.0));
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
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1.xyz = float3(1.0, 1.0, 1.0) - tmp0.xyz;
                tmp1.xyz = tmp1.xyz + tmp1.xyz;
                tmp2 = tex2D(_MainTex2, inp.texcoord1.xy);
                tmp3.xyz = float3(1.0, 1.0, 1.0) - tmp2.xyz;
                tmp1.xyz = -tmp1.xyz * tmp3.xyz + float3(1.0, 1.0, 1.0);
                tmp0.xyz = tmp0.xyz + tmp0.xyz;
                tmp0.w = tmp0.w * _Opacity;
                tmp0.xyz = tmp2.xyz * tmp0.xyz;
                tmp3.xyz = tmp2.xyz <= float3(0.5, 0.5, 0.5);
                tmp0.xyz = tmp3.xyz ? tmp0.xyz : tmp1.xyz;
                tmp0.xyz = tmp0.xyz - tmp2.xyz;
                tmp0.xyz = tmp0.www * tmp0.xyz + tmp2.xyz;
                tmp0.xyz = tmp0.xyz * _Color.xyz;
                o.sv_target.xyz = tmp0.xyz;
                tmp0.xyz = tmp0.xyz * inp.texcoord6.xyz;
                o.sv_target3.xyz = exp(-tmp0.xyz);
                o.sv_target.w = 1.0;
                o.sv_target1 = float4(0.0, 0.0, 0.0, 0.0);
                tmp1 = tex2D(_BumpMap, inp.texcoord.zw);
                tmp0.xy = float2(1.0, 1.0) - tmp1.wy;
                tmp1.xy = tmp1.wy + tmp1.wy;
                tmp0.xy = tmp0.xy + tmp0.xy;
                tmp2 = tex2D(_BumpMap2, inp.texcoord1.zw);
                tmp1.zw = float2(1.0, 1.0) - tmp2.wy;
                tmp0.xy = -tmp0.xy * tmp1.zw + float2(1.0, 1.0);
                tmp1.xy = tmp1.xy * tmp2.wy;
                tmp1.zw = tmp2.wy <= float2(0.5, 0.5);
                tmp0.xy = tmp1.zw ? tmp1.xy : tmp0.xy;
                tmp0.xy = tmp0.xy - tmp2.wy;
                tmp0.xy = tmp0.ww * tmp0.xy + tmp2.wy;
                tmp0.xy = tmp0.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp0.w = dot(tmp0.xy, tmp0.xy);
                tmp0.w = min(tmp0.w, 1.0);
                tmp0.w = 1.0 - tmp0.w;
                tmp0.z = sqrt(tmp0.w);
                tmp1.x = dot(inp.texcoord2.xyz, tmp0.xyz);
                tmp1.y = dot(inp.texcoord3.xyz, tmp0.xyz);
                tmp1.z = dot(inp.texcoord4.xyz, tmp0.xyz);
                o.sv_target2.xyz = tmp1.xyz * float3(0.5, 0.5, 0.5) + float3(0.5, 0.5, 0.5);
                o.sv_target2.w = 1.0;
                o.sv_target3.w = 1.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "META"
			LOD 400
			Tags { "LIGHTMODE" = "META" "RenderType" = "Opaque" }
			ZClip Off
			Cull Off
			GpuProgramID 380142
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float4 _MainTex2_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _Color;
			float _Opacity;
			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;
			// Custom ConstantBuffers for Vertex Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaVertexControl;
			CBUFFER_END
			// Custom ConstantBuffers for Fragment Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaFragmentControl;
			CBUFFER_END
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainTex2;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0.x = v.vertex.z > 0.0;
                tmp0.z = tmp0.x ? 0.0001 : 0.0;
                tmp0.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.xxx ? tmp0.xyz : v.vertex.xyz;
                tmp0.w = tmp0.z > 0.0;
                tmp1.z = tmp0.w ? 0.0001 : 0.0;
                tmp1.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.yyy ? tmp1.xyz : tmp0.xyz;
                tmp1 = tmp0.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1 = unity_ObjectToWorld._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord.zw = v.texcoord.xy * _MainTex2_ST.xy + _MainTex2_ST.zw;
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp1.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp1.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp1.xyz;
                tmp1.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp1.xyz;
                tmp0.w = dot(tmp1.xyz, tmp1.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp1.xyz = tmp0.www * tmp1.xyz;
                tmp2.xyz = tmp0.xyz * tmp1.xyz;
                tmp2.xyz = tmp0.zxy * tmp1.yzx + -tmp2.xyz;
                tmp0.w = v.tangent.w * unity_WorldTransformParams.w;
                tmp2.xyz = tmp0.www * tmp2.xyz;
                o.texcoord1.y = tmp2.x;
                tmp3.xyz = v.vertex.yyy * unity_ObjectToWorld._m01_m11_m21;
                tmp3.xyz = unity_ObjectToWorld._m00_m10_m20 * v.vertex.xxx + tmp3.xyz;
                tmp3.xyz = unity_ObjectToWorld._m02_m12_m22 * v.vertex.zzz + tmp3.xyz;
                tmp3.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp3.xyz;
                o.texcoord1.w = tmp3.x;
                o.texcoord1.x = tmp1.z;
                o.texcoord1.z = tmp0.y;
                o.texcoord2.x = tmp1.x;
                o.texcoord3.x = tmp1.y;
                o.texcoord2.z = tmp0.z;
                o.texcoord3.z = tmp0.x;
                o.texcoord2.w = tmp3.y;
                o.texcoord3.w = tmp3.z;
                o.texcoord2.y = tmp2.y;
                o.texcoord3.y = tmp2.z;
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
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1.xyz = float3(1.0, 1.0, 1.0) - tmp0.xyz;
                tmp1.xyz = tmp1.xyz + tmp1.xyz;
                tmp2 = tex2D(_MainTex2, inp.texcoord.zw);
                tmp3.xyz = float3(1.0, 1.0, 1.0) - tmp2.xyz;
                tmp1.xyz = -tmp1.xyz * tmp3.xyz + float3(1.0, 1.0, 1.0);
                tmp0.xyz = tmp0.xyz * tmp2.xyz;
                tmp0.w = tmp0.w * _Opacity;
                tmp0.xyz = tmp0.xyz + tmp0.xyz;
                tmp3.xyz = tmp2.xyz <= float3(0.5, 0.5, 0.5);
                tmp0.xyz = tmp3.xyz ? tmp0.xyz : tmp1.xyz;
                tmp0.xyz = tmp0.xyz - tmp2.xyz;
                tmp0.xyz = tmp0.www * tmp0.xyz + tmp2.xyz;
                tmp0.xyz = tmp0.xyz * _Color.xyz;
                tmp0.xyz = log(tmp0.xyz);
                tmp0.w = saturate(unity_OneOverOutputBoost);
                tmp0.xyz = tmp0.xyz * tmp0.www;
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = min(tmp0.xyz, unity_MaxOutputValue.xxx);
                tmp0.w = 1.0;
                tmp0 = unity_MetaFragmentControl ? tmp0 : float4(0.0, 0.0, 0.0, 0.0);
                o.sv_target = unity_MetaFragmentControl ? float4(0.0, 0.0, 0.0, 0.0235294) : tmp0;
                return o;
			}
			ENDCG
		}
	}
	SubShader {
		LOD 400
		Tags { "RenderType" = "Opaque" }
		Pass {
			Name "FORWARD"
			LOD 400
			Tags { "LIGHTMODE" = "FORWARDBASE" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
			ZClip Off
			GpuProgramID 449480
			// No subprograms found
		}
		Pass {
			Name "FORWARD"
			LOD 400
			Tags { "LIGHTMODE" = "FORWARDADD" "RenderType" = "Opaque" }
			Blend One One, One One
			ZClip Off
			ZWrite Off
			GpuProgramID 489030
			// No subprograms found
		}
		Pass {
			Name "PREPASS"
			LOD 400
			Tags { "LIGHTMODE" = "PREPASSBASE" "RenderType" = "Opaque" }
			ZClip Off
			GpuProgramID 579979
			// No subprograms found
		}
		Pass {
			Name "PREPASS"
			LOD 400
			Tags { "LIGHTMODE" = "PREPASSFINAL" "RenderType" = "Opaque" }
			ZClip Off
			ZWrite Off
			GpuProgramID 612315
			// No subprograms found
		}
		Pass {
			Name "DEFERRED"
			LOD 400
			Tags { "LIGHTMODE" = "DEFERRED" "RenderType" = "Opaque" }
			ZClip Off
			GpuProgramID 660797
			// No subprograms found
		}
		Pass {
			Name "META"
			LOD 400
			Tags { "LIGHTMODE" = "META" "RenderType" = "Opaque" }
			ZClip Off
			Cull Off
			GpuProgramID 740230
			// No subprograms found
		}
	}
	Fallback "Bumped Diffuse"
}