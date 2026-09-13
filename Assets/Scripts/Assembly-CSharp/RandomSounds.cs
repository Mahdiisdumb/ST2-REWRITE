using System.Collections;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
	public bool isplaying;

	public AudioClip[] sounds;

	private void Start()
	{
	}

	private void Update()
	{
		if (!isplaying)
		{
			StartCoroutine(PlaySound());
			isplaying = true;
		}
	}

	private IEnumerator PlaySound()
	{
		int randomnum = Random.Range(1, sounds.Length);
		int waittime = Random.Range(10, 60);
		yield return new WaitForSeconds(waittime);
		GetComponent<AudioSource>().clip = sounds[randomnum];
		GetComponent<AudioSource>().Play();
		isplaying = false;
	}
}
