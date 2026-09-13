using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/HDR")]
public class PP_HDR : PostProcessBase
{
	public float amount = 0.045f;

	public float multiplier = 1f;

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/HDR");
		if (Application.loadedLevelName.Contains("(Classic") || Application.loadedLevelName.Contains("TubbyCraft") || Application.loadedLevelName.Contains("Sandbox"))
		{
			base.enabled = false;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Amount", amount);
		base.material.SetFloat("_Multiplier", multiplier);
		Graphics.Blit(source, destination, base.material);
	}
}
