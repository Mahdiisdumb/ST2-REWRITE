using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/SobelOutlineV5")]
public class PP_SobelOutlineV5 : PostProcessBase
{
	public float threshold = 0.7f;

	private void Awake()
	{
		base.material.SetFloat("_Threshold", threshold);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/SobelOutlineV5");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Threshold", threshold);
		Graphics.Blit(source, destination, base.material);
	}
}
