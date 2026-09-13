using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Noise")]
public class PP_Noise : PostProcessBase
{
	public float noiseScale = 0.5f;

	private void Awake()
	{
		base.material.SetFloat("_NoiseScale", noiseScale);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/Noise");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_NoiseScale", noiseScale);
		Graphics.Blit(source, destination, base.material);
	}
}
