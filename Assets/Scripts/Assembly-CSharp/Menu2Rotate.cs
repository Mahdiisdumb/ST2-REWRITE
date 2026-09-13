using UnityEngine;

public class Menu2Rotate : MonoBehaviour
{
	public float mouseSensitivityX = 1f;

	public float mouseSensitivityY = 1f;

	public float maxXRotation = 22f;

	public float minXRotation = 22f;

	public float maxYRotation = 1f;

	public float minYRotation = 1f;

	private float centerSceenX;

	private float centerSceenY;

	private void Start()
	{
		centerSceenX = Screen.width / 2;
		centerSceenY = Screen.height / 2;
	}

	private void Update()
	{
		float num = Input.mousePosition.x / (mouseSensitivityX * 10f) - centerSceenX / (mouseSensitivityX * 5f) / 2f;
		float num2 = (0f - Input.mousePosition.y) / (mouseSensitivityY * 10f) + centerSceenY / (mouseSensitivityY * 5f) / 2f;
		base.transform.eulerAngles = new Vector3(0f - num2, 0f - num, 0f);
		if (base.transform.eulerAngles.y > maxXRotation && base.transform.eulerAngles.y < 20f + maxXRotation)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, maxXRotation, base.transform.eulerAngles.z);
		}
		else if (base.transform.eulerAngles.y < 360f - minXRotation && base.transform.eulerAngles.y > 20f + minXRotation)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 360f - minXRotation, base.transform.eulerAngles.z);
		}
		else if (base.transform.eulerAngles.x > maxYRotation && base.transform.eulerAngles.x < 20f + maxYRotation)
		{
			base.transform.eulerAngles = new Vector3(maxYRotation, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
		}
		else if (base.transform.eulerAngles.x < 360f - minYRotation && base.transform.eulerAngles.x > 20f + minYRotation)
		{
			base.transform.eulerAngles = new Vector3(360f - minYRotation, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
		}
	}
}
