using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/LineArt")]
public class PP_LineArt : PostProcessBase
{
	public Color lineColor = Color.black;

	public float lineAmount = 80f;

	private void Awake()
	{
		base.material.SetVector("_LineColor", lineColor);
		base.material.SetFloat("_LineAmount", lineAmount);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/LineArt");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_LineColor", lineColor);
		base.material.SetFloat("_LineAmount", lineAmount);
		Graphics.Blit(source, destination, base.material);
	}
}
