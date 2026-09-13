using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/ScreenWaves")]
public class PP_ScreenWaves : PostProcessBase
{
	public float speed = 10f;

	public float amplitude = 0.09f;

	private void Awake()
	{
		base.material.SetFloat("_Speed", speed);
		base.material.SetFloat("_Amplitude", amplitude);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/ScreenWaves");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Speed", speed);
		base.material.SetFloat("_Amplitude", amplitude);
		Graphics.Blit(source, destination, base.material);
	}
}
