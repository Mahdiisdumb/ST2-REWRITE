using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			GetComponent<AudioSource>().Stop();
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			GetComponent<AudioSource>().pitch = 1.5f;
		}
		else
		{
			GetComponent<AudioSource>().pitch = 1f;
		}
	}
}
