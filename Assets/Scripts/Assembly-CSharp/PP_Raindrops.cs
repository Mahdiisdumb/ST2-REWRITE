using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/Raindrops")]
public class PP_Raindrops : PostProcessBase
{
	public Texture bumpTexture;

	public float bumpAmount = 0.7f;

	public float speedX;

	public float speedY = 0.1f;

	private void Awake()
	{
		if ((bool)bumpTexture)
		{
			base.material.SetTexture("_BumpTex", bumpTexture);
		}
	}

	private void OnEnable()
	{
		if (!bumpTexture)
		{
			Debug.LogWarning("You must set the bumpTexture Texture");
		}
		shader = Shader.Find("Hidden/Aubergine/Raindrops");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_BumpTex", bumpTexture);
		base.material.SetFloat("_Amount", bumpAmount);
		base.material.SetFloat("_SpeedX", speedX);
		base.material.SetFloat("_SpeedY", speedY);
		Graphics.Blit(source, destination, base.material);
	}
}
