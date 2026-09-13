using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Aubergine/ThermalVisionV2")]
public class PP_ThermalVisionV2 : PostProcessBase
{
	public Texture thermalTex;

	public Texture noiseTex;

	public float noiseAmount = 0.3f;

	private void Awake()
	{
		if ((bool)thermalTex)
		{
			base.material.SetTexture("_ThermalTex", thermalTex);
		}
		if ((bool)noiseTex)
		{
			base.material.SetTexture("_NoiseTex", noiseTex);
		}
		base.material.SetFloat("_NoiseAmount", noiseAmount);
	}

	private void OnEnable()
	{
		if (!noiseTex || !thermalTex)
		{
			Debug.LogWarning("You must set the necessary textures");
		}
		shader = Shader.Find("Hidden/Aubergine/ThermalVisionV2");
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_ThermalTex", thermalTex);
		base.material.SetTexture("_NoiseTex", noiseTex);
		base.material.SetFloat("_NoiseAmount", noiseAmount);
		Matrix4x4 inverse = (GetComponent<Camera>().projectionMatrix * GetComponent<Camera>().worldToCameraMatrix).inverse;
		base.material.SetMatrix("_ViewProjectInverse", inverse);
		Graphics.Blit(source, destination, base.material);
	}
}
