using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/BloodOverlay")]
public class BleedBehavior : MonoBehaviour
{
	public static float BloodAmount;

	public float TestingBloodAmount = 0.5f;

	public static float minBloodAmount;

	public float EdgeSharpness = 1f;

	public float minAlpha;

	public float maxAlpha = 1f;

	public float distortion = 0.2f;

	public bool autoFadeOut = true;

	public float autoFadeOutAbsReduc = 0.05f;

	public float autoFadeOutRelReduc = 0.5f;

	public float updateSpeed = 20f;

	private float prevBloodAmount;

	public Texture2D Image;

	public Texture2D Normals;

	public Shader Shader;

	private Material _material;

	private void Awake()
	{
		_material = new Material(Shader);
		_material.SetTexture("_BlendTex", Image);
		_material.SetTexture("_BumpMap", Normals);
	}

	public void Update()
	{
		if (autoFadeOut && BloodAmount > 0f)
		{
			BloodAmount -= autoFadeOutAbsReduc * Time.deltaTime;
			BloodAmount *= Mathf.Pow(1f - autoFadeOutRelReduc, Time.deltaTime);
			BloodAmount = Mathf.Max(BloodAmount, 0f);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!Application.isPlaying)
		{
			_material.SetTexture("_BlendTex", Image);
			_material.SetTexture("_BumpMap", Normals);
			float num = Mathf.Clamp01(TestingBloodAmount) * (1f - minBloodAmount) + minBloodAmount;
			num = Mathf.Clamp01(num * (maxAlpha - minAlpha) + minAlpha);
			num = Mathf.Lerp(prevBloodAmount, num, Mathf.Clamp01(updateSpeed * Time.deltaTime));
			_material.SetFloat("_BlendAmount", num);
			prevBloodAmount = num;
		}
		else
		{
			float num2 = Mathf.Clamp01(BloodAmount) * (1f - minBloodAmount) + minBloodAmount;
			num2 = Mathf.Clamp01(num2 * (maxAlpha - minAlpha) + minAlpha);
			num2 = Mathf.Lerp(prevBloodAmount, num2, Mathf.Clamp01(updateSpeed * Time.deltaTime));
			_material.SetFloat("_BlendAmount", num2);
			prevBloodAmount = num2;
		}
		_material.SetFloat("_EdgeSharpness", EdgeSharpness);
		_material.SetFloat("_Distortion", distortion);
		Graphics.Blit(source, destination, _material);
	}
}
