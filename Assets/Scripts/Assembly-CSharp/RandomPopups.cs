using System.Collections;
using UnityEngine;

public class RandomPopups : MonoBehaviour
{
	public bool isplaying;

	public Texture2D[] images;

	private void Start()
	{
	}

	private void Update()
	{
		if (!isplaying)
		{
			StartCoroutine(ShowImage());
			isplaying = true;
		}
	}

	private IEnumerator ShowImage()
	{
		int randomnum = Random.Range(1, images.Length);
		int waittime = Random.Range(60, 300);
		yield return new WaitForSeconds(waittime);
		GetComponent<GUITexture>().texture = images[randomnum];
		GetComponent<GUITexture>().enabled = true;
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(0.15f);
		GetComponent<GUITexture>().enabled = false;
		GetComponent<AudioSource>().Stop();
		isplaying = false;
	}
}
