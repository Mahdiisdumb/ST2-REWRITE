Shader "Hidden/Dof/Bokeh34" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Source ("Base (RGB)", 2D) = "black" {}
	}
	SubShader {
		Pass {
			Blend OneMinusDstColor One, OneMinusDstColor One
			ZClip Off
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 31440
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _ArScale;
			float _Intensity;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			sampler2D _Source;
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0.xy = v.texcoord.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp0.xy = tmp0.xy * _ArScale.xy;
                tmp0.zw = v.texcoord1.xy * float2(1.0, -1.0) + float2(0.0, 1.0);
                tmp1 = tex2Dlod(_Source, float4(tmp0.zw, 0, 0.0));
                o.sv_position.xy = tmp0.xy * tmp1.ww + v.vertex.xy;
                o.sv_position.zw = v.vertex.zw;
                o.texcoord.xy = v.texcoord.xy;
                o.texcoord1.xyz = tmp1.xyz * _Intensity.xxx;
                o.texcoord1.w = tmp1.w;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0.xyz = inp.texcoord1.xyz * float3(0.25, 0.25, 0.25);
                tmp0.x = dot(tmp0.xyz, float3(0.22, 0.707, 0.071));
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                o.sv_target.w = tmp0.x * tmp1.w;
                o.sv_target.xyz = tmp1.xyz * inp.texcoord1.xyz;
                return o;
			}
			ENDCG
		}
	}
}