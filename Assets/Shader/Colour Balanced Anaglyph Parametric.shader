Shader "Colour Balanced Anaglyph Parametric" {
	Properties {
		_LeftTex ("Left (RGB)", 2D) = "white" {}
		_RightTex ("Right (RGB)", 2D) = "white" {}
		_Balance_Left_R ("Balance Left R", Vector) = (0,0,0,0)
		_Balance_Left_G ("Balance Left G", Vector) = (0,0,0,0)
		_Balance_Left_B ("Balance Left B", Vector) = (0,0,0,0)
		_Balance_Right_R ("Balance Right R", Vector) = (0,0,0,0)
		_Balance_Right_G ("Balance Right G", Vector) = (0,0,0,0)
		_Balance_Right_B ("Balance Right B", Vector) = (0,0,0,0)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
}