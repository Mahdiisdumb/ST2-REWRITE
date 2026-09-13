using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/NightVision")]
public class PP_NightVision : PostProcessBase
{
	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/NightVision");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
