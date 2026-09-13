using Photon;
using UnityEngine;

public class LoadLobby : Photon.MonoBehaviour
{
	public bool issingleplayer;

	public bool iscustomserver;

	public string appID;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		PhotonNetwork.offlineMode = false;
		PlayerPrefs.SetString("appid", appID);
		if (iscustomserver)
		{
			PlayerPrefs.SetInt("customserver", 1);
			if (PlayerPrefs.GetString("id") != null && PlayerPrefs.GetString("id") != string.Empty && PlayerPrefs.GetString("id") != " ")
			{
				Application.LoadLevel(2);
			}
		}
		else
		{
			PlayerPrefs.SetInt("customserver", 0);
			Application.LoadLevel(2);
		}
	}
}
