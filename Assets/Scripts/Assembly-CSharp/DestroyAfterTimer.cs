using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
	public float thetimer = 0.5f;

	private void Start()
	{
		Object.Destroy(base.gameObject, thetimer);
	}
}
