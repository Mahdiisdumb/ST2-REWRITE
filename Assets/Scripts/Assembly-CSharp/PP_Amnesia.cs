using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Amnesia")]
public class PP_Amnesia : PostProcessBase
{
	public float density = 1f;

	public float speed = 3f;

	private void Awake()
	{
		base.material.SetFloat("_Density", density);
		base.material.SetFloat("_Speed", speed);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/Amnesia");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Density", density);
		base.material.SetFloat("_Speed", speed);
		Graphics.Blit(source, destination, base.material);
	}
}
