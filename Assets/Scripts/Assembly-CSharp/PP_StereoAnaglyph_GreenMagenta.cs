using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/StereoAnaglyph_GreenMagenta")]
public class PP_StereoAnaglyph_GreenMagenta : PostProcessBase
{
	public float distance = 0.001f;

	private void Awake()
	{
		base.material.SetFloat("_Distance", distance);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/StereoAnaglyph_GreenMagenta");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Distance", distance);
		base.material.SetFloat("_TexSizeX", source.width);
		base.material.SetFloat("_TexSizeY", source.height);
		Graphics.Blit(source, destination, base.material);
	}
}
