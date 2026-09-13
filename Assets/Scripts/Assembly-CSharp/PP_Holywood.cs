using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Holywood")]
public class PP_Holywood : PostProcessBase
{
	public float lumThreshold = 0.13f;

	private void Awake()
	{
		base.material.SetFloat("_LumThreshold", lumThreshold);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/Holywood");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_LumThreshold", lumThreshold);
		Graphics.Blit(source, destination, base.material);
	}
}
