using UnityEngine;

public class ToggleLight : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
		}
	}
}
