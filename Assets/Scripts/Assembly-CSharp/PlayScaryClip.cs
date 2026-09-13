using UnityEngine;

public class PlayScaryClip : MonoBehaviour
{
	public MovieTexture movTexture;

	private void Start()
	{
	}

	private void Update()
	{
		if (!movTexture.isPlaying)
		{
			GetComponent<Renderer>().materials[0].mainTexture = movTexture;
			movTexture.Play();
			movTexture.loop = true;
		}
	}
}
