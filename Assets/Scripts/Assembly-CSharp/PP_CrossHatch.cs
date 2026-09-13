using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/CrossHatch")]
public class PP_CrossHatch : PostProcessBase
{
	public Color lineColor = Color.red;

	public float lineWidth = 0.005f;

	private void Awake()
	{
		base.material.SetVector("_LineColor", lineColor);
		base.material.SetFloat("_LineWidth", lineWidth);
	}

	private void OnEnable()
	{
		shader = Shader.Find("Hidden/Aubergine/CrossHatch");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_LineColor", lineColor);
		base.material.SetFloat("_LineWidth", lineWidth);
		Graphics.Blit(source, destination, base.material);
	}
}
