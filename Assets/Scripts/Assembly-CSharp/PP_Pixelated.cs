using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Pixelated")]
public class PP_Pixelated : PostProcessBase
{
	public float pixWidth = 16f;

	public float pixHeight = 16f;

	private void Awake()
	{
		base.material.SetFloat("_PixWidth", pixWidth);
		base.material.SetFloat("_PixHeight", pixHeight);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/Pixelated");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_PixWidth", pixWidth);
		base.material.SetFloat("_PixHeight", pixHeight);
		Graphics.Blit(source, destination, base.material);
	}
}
