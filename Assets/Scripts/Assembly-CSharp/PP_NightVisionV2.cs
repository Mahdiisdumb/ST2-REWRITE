using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/NightVisionV2")]
public class PP_NightVisionV2 : PostProcessBase
{
	public Texture noiseTex;

	public float noiseAmount = 0.9f;

	public float lumThreshold = 0.2f;

	public float brightenFactor = 2f;

	public Color visionColor = Color.green;

	private void Awake()
	{
		if ((bool)noiseTex)
		{
			base.material.SetTexture("_NoiseTex", noiseTex);
		}
		base.material.SetFloat("_NoiseAmount", noiseAmount);
		base.material.SetFloat("_LumThreshold", lumThreshold);
		base.material.SetFloat("_BrightenFactor", brightenFactor);
		base.material.SetVector("_VisionColor", visionColor);
	}

	private void OnEnable()
	{
		if (!noiseTex)
		{
			Debug.LogWarning("You must set the noiseTex Texture");
		}
		shader = Shader.Find("Hidden/Aubergine/NightVisionV2");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_NoiseTex", noiseTex);
		base.material.SetFloat("_NoiseAmount", noiseAmount);
		base.material.SetFloat("_LumThreshold", lumThreshold);
		base.material.SetFloat("_BrightenFactor", brightenFactor);
		base.material.SetVector("_VisionColor", visionColor);
		Graphics.Blit(source, destination, base.material);
	}
}
