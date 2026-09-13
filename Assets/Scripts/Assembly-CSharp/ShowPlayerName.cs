using Photon;
using UnityEngine;

public class ShowPlayerName : Photon.MonoBehaviour
{
	public GameObject[] players;

	public bool show = true;

	public bool freeze;

	private void Awake()
	{
		if (!base.photonView.isMine)
		{
			base.enabled = false;
		}
		if (PlayerPrefs.GetInt("cameramode") == 2 || base.gameObject.tag == "Monster")
		{
			players = GameObject.FindGameObjectsWithTag("Player");
			GameObject[] array = players;
			foreach (GameObject gameObject in array)
			{
				gameObject.GetComponent<PlayerName>().showname = false;
			}
			freeze = true;
		}
	}

	private void Update()
	{
		if (!freeze && Input.GetKeyDown(KeyCode.P))
		{
			if (show)
			{
				HideNames();
				show = false;
			}
			else
			{
				ShowNames();
				show = true;
			}
		}
	}

	private void ShowNames()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array = players;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<PlayerName>().showname = true;
		}
	}

	private void HideNames()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array = players;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<PlayerName>().showname = false;
		}
	}
}
