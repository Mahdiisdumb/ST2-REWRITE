using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/LightWave")]
public class PP_LightWave : PostProcessBase
{
	public float red = 4f;

	public float green = 4f;

	public float blue = 4f;

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/LightWave");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Red", red);
		base.material.SetFloat("_Green", green);
		base.material.SetFloat("_Blue", blue);
		Graphics.Blit(source, destination, base.material);
	}
}
