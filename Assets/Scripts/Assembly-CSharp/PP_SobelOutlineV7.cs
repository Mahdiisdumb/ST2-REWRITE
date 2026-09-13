using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/SobelOutlineV7")]
public class PP_SobelOutlineV7 : PostProcessBase
{
	public float threshold = 0.7f;

	private void Awake()
	{
		base.material.SetFloat("_Threshold", threshold);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/SobelOutlineV7");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Threshold", threshold);
		Graphics.Blit(source, destination, base.material);
	}
}
