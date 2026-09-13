using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Pencil")]
public class PP_Pencil : PostProcessBase
{
	public Texture pencilTexture;

	public float effectStrength = 0.5f;

	public float brightness = 0.5f;

	private void Awake()
	{
		if ((bool)pencilTexture)
		{
			base.material.SetTexture("_PencilTex", pencilTexture);
		}
		base.material.SetFloat("_Amount", effectStrength);
		base.material.SetFloat("_Brightness", brightness);
	}

	private void OnEnable()
	{
		if (!pencilTexture)
		{
			Debug.LogWarning("You must set the pencilTexture Texture");
		}
		shader = Shader.Find("Hidden/Aubergine/Pencil");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_PencilTex", pencilTexture);
		base.material.SetFloat("_Amount", effectStrength);
		base.material.SetFloat("_Brightness", brightness);
		Graphics.Blit(source, destination, base.material);
	}
}
