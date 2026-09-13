using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerSelect : MonoBehaviour
{
	[Serializable]
	public class serverInfo
	{
		public string serverID;

		public string curPlayers = "-";

		public string maxPlayers = "20";
	}

	public GUISkin guiSkin;

	public string menu = "Main Menu";

	public GameObject deactivate;

	private string selectaserver = "Please Select A Server";

	private string back = "Back";

	private string server = "Server #";

	private string connecting = "Connecting...";

	private string error = "Could not connect, server may be full.  Please try another server...";

	public List<serverInfo> Servers;

	public Vector2 scrollPosition = Vector2.zero;

	private bool isConnecting;

	private bool isError;

	private bool connectFailed;

	private string errorDialog;

	private double timeToClearDialog;

	private bool checkRoom = true;

	public string ErrorDialog
	{
		get
		{
			return errorDialog;
		}
		private set
		{
			errorDialog = value;
			if (!string.IsNullOrEmpty(value))
			{
				timeToClearDialog = Time.time + 4f;
			}
		}
	}

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			server = "Servidor #";
			back = "Volver";
			selectaserver = "Por Favor Selecciona Un Servidor";
			connecting = "Conectando...";
			error = "No se pudo conectar, el servidor quizás esté lleno.  Por favor intenta otro servidor...";
		}
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			Application.LoadLevel(1);
		}
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		deactivate.SetActive(false);
	}

	private void OnDisable()
	{
		deactivate.SetActive(true);
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (!isError && !isConnecting)
		{
			if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
			{
				GameObject.Find("root").GetComponent<Menu2Root>().Show(menu);
				base.enabled = false;
			}
			GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 400f), selectaserver);
			scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 195, Screen.height / 2 - 170, 390f, 350f), scrollPosition, new Rect(0f, 0f, 355f, Servers.Count * 80 + 40));
			for (int i = 0; i < Servers.Count; i++)
			{
				if (GUI.Button(new Rect(10f, 20 + 80 * i, 350f, 80f), server + (i + 1)))
				{
					PlayerPrefs.SetString("appid", Servers[i].serverID);
					PlayerPrefs.SetInt("customserver", 0);
					PhotonNetwork.offlineMode = false;
					PhotonNetwork.ConnectUsingSettings("v2.2");
					isConnecting = true;
					isError = false;
				}
			}
			GUI.EndScrollView();
		}
		else if (isConnecting)
		{
			GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 30, 300f, 60f), "<b>" + connecting + "</b>");
		}
		else
		{
			GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 30, 600f, 60f), "<b>" + error + "</b>");
			if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
			{
				isError = false;
				isConnecting = false;
			}
		}
	}

	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}

	public void OnPhotonCreateRoomFailed()
	{
		ErrorDialog = "Error: Can't create room (room name maybe already used).";
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}

	public void OnPhotonJoinRoomFailed(object[] cause)
	{
		ErrorDialog = "Error: Can't join room (full or unknown room name). " + cause[1];
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}

	public void OnPhotonRandomJoinFailed()
	{
		ErrorDialog = "Error: Can't join random room (none found).";
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}

	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
		if (checkRoom)
		{
			isConnecting = false;
			isError = true;
		}
	}

	public void OnJoinedLobby()
	{
		Application.LoadLevel("Lobby");
	}

	public void OnFailedToConnectToPhoton(object parameters)
	{
		connectFailed = true;
		Debug.Log(string.Concat("OnFailedToConnectToPhoton. StatusCode: ", parameters, " ServerAddress: ", PhotonNetwork.ServerAddress));
		isConnecting = false;
		isError = true;
	}
}
