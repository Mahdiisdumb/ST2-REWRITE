using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class OVRImageEffectBase : MonoBehaviour
{
	public Material material;

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
	}
}
