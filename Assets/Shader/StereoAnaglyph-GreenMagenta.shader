Shader "Hidden/Aubergine/StereoAnaglyph_GreenMagenta" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TexSizeX ("TexSizeX", Float) = 800
		_TexSizeY ("TexSizeY", Float) = 600
		_Distance ("Distance", Float) = 0.005
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
			GpuProgramID 14945
			// No subprograms found
		}
	}
}