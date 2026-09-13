using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Bleach")]
public class PP_Bleach : PostProcessBase
{
	public float opacity = 0.1f;

	private void Awake()
	{
		base.material.SetFloat("_Opacity", opacity);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/Bleach");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Opacity", opacity);
		Graphics.Blit(source, destination, base.material);
	}
}
