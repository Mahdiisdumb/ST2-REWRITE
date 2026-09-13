using UnityEngine;

public class CamBob : MonoBehaviour
{
	public float amount = 0.02f;

	public float maxAmount = 0.03f;

	public float smooth = 3f;

	private Vector3 def;

	private void Start()
	{
		def = base.transform.localPosition;
	}

	private void Update()
	{
		float value = (0f - Input.GetAxis("Mouse X")) * amount;
		float value2 = (0f - Input.GetAxis("Mouse Y")) * amount * 10f;
		value = Mathf.Clamp(value, 0f - maxAmount, maxAmount);
		value2 = Mathf.Clamp(value2, 0f - maxAmount, maxAmount) * 1.5f;
		Vector3 b = new Vector3(def.x + value, def.y + value2, def.z);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, Time.deltaTime * smooth);
	}
}
