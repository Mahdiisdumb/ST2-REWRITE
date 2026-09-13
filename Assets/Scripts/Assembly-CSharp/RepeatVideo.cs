using UnityEngine;

public class RepeatVideo : MonoBehaviour
{
	public MovieTexture movTexture;

	private void Start()
	{
	}

	private void Update()
	{
		if (!movTexture.isPlaying)
		{
			GetComponent<Renderer>().materials[1].mainTexture = movTexture;
			movTexture.Play();
			movTexture.loop = true;
		}
	}
}
