using System.Collections;
using UnityEngine;

public class PlayIntro : MonoBehaviour
{
	public MovieTexture movTexture;

	public MovieTexture movTexture2;

	public bool isplaying;

	public bool playnow;

	public Texture2D title;

	public Texture2D english;

	public Texture2D spanish;

	public Texture2D logo;

	private bool start;

	private void Awake()
	{
	}

	private void Update()
	{
		if (!movTexture.isPlaying && !isplaying && playnow && start)
		{
			if (PlayerPrefs.GetInt("language") == 1)
			{
				GetComponent<GUITexture>().texture = movTexture2;
				movTexture2.Play();
			}
			else
			{
				GetComponent<GUITexture>().texture = movTexture;
				movTexture.Play();
			}
		}
		if (playnow && !start)
		{
			StartCoroutine(LoadMenu());
		}
	}

	private void OnGUI()
	{
		if (!playnow)
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - 200, 10f, 400f, 150f), title);
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 90, 200f, 75f), english))
			{
				PlayerPrefs.SetInt("language", 0);
				playnow = true;
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 10, 200f, 75f), spanish))
			{
				PlayerPrefs.SetInt("language", 1);
				playnow = true;
			}
		}
	}

	private IEnumerator LoadMenu()
	{
		start = true;
		yield return new WaitForSeconds(3.5f);
		Application.LoadLevel(1);
	}
}
