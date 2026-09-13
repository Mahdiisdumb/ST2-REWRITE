using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

public class ConnectMenu : Photon.MonoBehaviour
{
	[Serializable]
	public class AllMaps
	{
		public string mapName;

		public Texture2D mapPreview;
	}

	public GUISkin guiSKin;

	public Texture blackScreen;

	public Texture top;

	public Texture bottom;

	public bool maxplayers2;

	public int roundDuration = 600;

	private List<int> maxPlayersOptions = new List<int>();

	public List<AllMaps> allMaps;

	public string totplayers = "25";

	private string newRoomName;

	private string playerName;

	public int maxPlayers;

	private int selectedMap;

	private string gameMode;

	private string deathmatch = "Versus";

	private string teamdeathmatch = "Co-op";

	private string privateword = "Private";

	private bool hidedeathmatch;

	private bool privatematch;

	private bool haslogin;

	private Vector2 scroll;

	private Vector2 mapScroll;

	private float fadeValue;

	private int fadeDir;

	private RoomInfo[] allRooms;

	private bool createRoom;

	private bool connectingToRoom;

	private string connecting = "Connecting...";

	private string lobby = "Lobby";

	private string joinroom = "Join Room";

	private string noroomscreated = "No rooms created...";

	private string createroom = "Create Room";

	private string playername = "Player Name: ";

	private string roomname = "Room Name: ";

	private string custardamount = "Custard Amount: ";

	private string returntolobby = "Return To Lobby";

	private string play = "Play";

	private string loading = "Loading...";

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			connecting = "Conexión...";
			lobby = "Lobby";
			joinroom = "Unirse";
			noroomscreated = "No hay habitaciones creadas...";
			createroom = "Crear Sala";
			playername = "nombre del jugador: ";
			roomname = "nombre del jugador: ";
			custardamount = "Cantidad Tubipapillas: ";
			returntolobby = "Volver al Lobby";
			play = "jugar";
			loading = "Loading...";
			privateword = "Privado";
		}
	}

	private void Start()
	{
		PhotonNetwork.isMessageQueueRunning = true;
		Screen.lockCursor = false;
		allRooms = PhotonNetwork.GetRoomList();
		newRoomName = "Room Name " + UnityEngine.Random.Range(111, 999);
		playerName = "Player " + UnityEngine.Random.Range(111, 999);
		maxPlayersOptions.Add(5);
		maxPlayersOptions.Add(10);
		maxPlayersOptions.Add(15);
		maxPlayersOptions.Add(20);
		maxPlayersOptions.Add(25);
		maxPlayers = maxPlayersOptions[2];
		selectedMap = 0;
		if (roundDuration == 0)
		{
			roundDuration = 600;
		}
		gameMode = "TDM";
		if (ObscuredPrefs.GetString("ZWName") != string.Empty && ObscuredPrefs.GetString("PlayerType") != string.Empty && ObscuredPrefs.HasKey("PlayerType"))
		{
			haslogin = true;
			playerName = ObscuredPrefs.GetString("ZWName");
		}
		else
		{
			if (ObscuredPrefs.HasKey("PlayerName") && ObscuredPrefs.GetString("PlayerName") != string.Empty)
			{
				playerName = ObscuredPrefs.GetString("PlayerName");
			}
			else
			{
				playerName = "Guest" + UnityEngine.Random.Range(0, 999);
			}
			haslogin = false;
		}
		Cursor.visible = true;
		Screen.lockCursor = false;
	}

	private void Update()
	{
		newRoomName = newRoomName.Replace("<", string.Empty);
		if (!haslogin)
		{
			playerName = playerName.Replace("<", string.Empty);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PhotonNetwork.Disconnect();
			PhotonNetwork.LeaveLobby();
		}
		float num = 3f;
		float num2 = 0f;
		if (!PhotonNetwork.connected)
		{
			if (Time.time - num > num2)
			{
				num2 = Time.time - Time.deltaTime;
			}
			while (num2 < Time.time)
			{
				num2 += num;
				if (PhotonNetwork.connectionState != ConnectionState.Connecting && PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState == ConnectionState.Disconnecting)
				{
				}
			}
		}
		if (PhotonNetwork.connected && allRooms.Length != PhotonNetwork.GetRoomList().Length)
		{
			allRooms = PhotonNetwork.GetRoomList();
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiSKin;
		GUI.color = Color.white;
		GUI.depth = -2;
		if (!PhotonNetwork.connected)
		{
			GUI.enabled = false;
		}
		else
		{
			GUI.enabled = true;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f);
		GUI.DrawTexture(new Rect(Screen.width - bottom.width, Screen.height - bottom.height, bottom.width, bottom.height), bottom, ScaleMode.ScaleToFit);
		GUI.color = Color.white;
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 150, 500f, 340f), lobby, GUI.skin.GetStyle("window"));
		ShowConnectMenu();
		GUILayout.EndArea();
		if (!PhotonNetwork.connected)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 15, 150f, 30f), connecting);
		}
		FadeScreen();
	}

	private void ShowConnectMenu()
	{
		GUILayout.Space(10f);
		if (!createRoom)
		{
			scroll = GUILayout.BeginScrollView(scroll, GUILayout.Width(480f), GUILayout.Height(225f));
			if (allRooms != null && allRooms.Length > 0)
			{
				RoomInfo[] array = allRooms;
				foreach (RoomInfo roomInfo in array)
				{
					if (allRooms.Length <= 0)
					{
						continue;
					}
					GUILayout.BeginHorizontal("box");
					GUILayout.Label(roomInfo.name, GUILayout.Width(150f));
					GUILayout.Label((string)roomInfo.customProperties["MapName"], GUILayout.Width(135f));
					GUILayout.Label(roomInfo.playerCount + "/" + roomInfo.maxPlayers, GUILayout.Width(60f));
					GUILayout.FlexibleSpace();
					if (GUILayout.Button(joinroom, GUILayout.Width(100f)))
					{
						PhotonNetwork.JoinRoom(roomInfo.name);
						if (!haslogin)
						{
							PhotonNetwork.playerName = playerName;
						}
						else
						{
							PhotonNetwork.playerName = ObscuredPrefs.GetString("ZWName");
						}
						connectingToRoom = true;
						CheckPlayerNameAndRoom();
						ObscuredPrefs.SetString("PlayerName", playerName);
					}
					GUILayout.EndHorizontal();
				}
			}
			else
			{
				GUILayout.Label(noroomscreated);
			}
			GUILayout.EndScrollView();
			GUILayout.Space(5f);
			GUILayout.BeginHorizontal();
			GUILayout.Label(playername);
			if (!haslogin)
			{
				playerName = GUILayout.TextField(playerName, 15, GUILayout.Height(25f));
			}
			else
			{
				string @string = ObscuredPrefs.GetString("PlayerType");
				if (@string == "2")
				{
					GUI.color = Color.green;
				}
				if (@string == "3")
				{
					GUI.color = Color.cyan;
				}
				if (@string == "4")
				{
					GUI.color = Color.red;
				}
				if (@string == "5")
				{
					GUI.color = Color.grey;
				}
				if (@string == "6")
				{
					GUI.color = Color.cyan;
				}
				if (@string == "7")
				{
					GUI.color = Color.grey;
				}
				if (@string == "8")
				{
					GUI.color = Color.red;
				}
				if (@string == "9")
				{
					GUI.color = Color.yellow;
				}
				if (@string == "10")
				{
					GUI.color = Color.blue;
				}
				GUILayout.TextField(playerName, 15, GUILayout.Height(25f));
				GUI.color = Color.white;
			}
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(createroom, GUILayout.Width(130f), GUILayout.Height(25f)))
			{
				createRoom = true;
				CheckPlayerNameAndRoom();
				ObscuredPrefs.SetString("PlayerName", playerName);
			}
			GUILayout.EndHorizontal();
			return;
		}
		GUILayout.BeginHorizontal();
		GUILayout.Label(roomname, GUILayout.Width(130f));
		newRoomName = GUILayout.TextField(newRoomName, 15, GUILayout.Height(25f));
		GUILayout.EndHorizontal();
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		GUILayout.Label(custardamount, GUILayout.Width(130f));
		for (int j = 0; j < maxPlayersOptions.Count; j++)
		{
			if (maxPlayers == maxPlayersOptions[j])
			{
				GUI.color = Color.green;
			}
			else
			{
				GUI.color = Color.white;
			}
			if (GUILayout.Button(maxPlayersOptions[j].ToString(), GUILayout.Width(27f), GUILayout.Height(25f)))
			{
				maxPlayers = maxPlayersOptions[j];
			}
		}
		GUI.color = Color.white;
		GUILayout.EndHorizontal();
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Game Mode: ", GUILayout.Width(130f));
		if (gameMode == "TDM")
		{
			GUI.color = Color.green;
		}
		if (GUILayout.Button(teamdeathmatch, GUILayout.Width(140f), GUILayout.Height(25f)))
		{
			gameMode = "TDM";
		}
		GUI.color = Color.white;
		if (gameMode == "DM")
		{
			GUI.color = Color.green;
		}
		if (!hidedeathmatch && GUILayout.Button(deathmatch, GUILayout.Width(140f), GUILayout.Height(25f)))
		{
			gameMode = "DM";
		}
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		mapScroll = GUILayout.BeginScrollView(mapScroll, false, true, GUILayout.Width(240f), GUILayout.Height(160f));
		for (int k = 0; k < allMaps.Count; k++)
		{
			if (selectedMap == k)
			{
				GUI.color = Color.green;
			}
			else
			{
				GUI.color = Color.white;
			}
			if (GUILayout.Button(allMaps[k].mapName, GUILayout.Height(25f)))
			{
				selectedMap = k;
			}
		}
		GUI.color = Color.white;
		GUILayout.EndScrollView();
		GUILayout.Space(10f);
		if (allMaps[selectedMap].mapPreview != null)
		{
			GUILayout.Label(allMaps[selectedMap].mapPreview, GUILayout.Width(230f), GUILayout.Height(160f));
		}
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button(returntolobby, GUILayout.Width(160f), GUILayout.Height(25f)))
		{
			createRoom = false;
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button(play, GUILayout.Width(130f), GUILayout.Height(25f)))
		{
			CheckPlayerNameAndRoom();
			PhotonNetwork.player.name = playerName;
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable["MapName"] = allMaps[selectedMap].mapName;
			hashtable["RoundDuration"] = roundDuration;
			hashtable["GameMode"] = gameMode;
			string[] propsToListInLobby = new string[3] { "MapName", "RoundDuration", "GameMode" };
			PlayerPrefs.SetString("gamemode", gameMode);
			PlayerPrefs.SetInt("custardamount", maxPlayers);
			if (!maxplayers2)
			{
				PhotonNetwork.CreateRoom(string.Empty + newRoomName + string.Empty, true, true, 6, hashtable, propsToListInLobby);
			}
			else
			{
				PhotonNetwork.CreateRoom(newRoomName, true, true, int.Parse(totplayers), hashtable, propsToListInLobby);
			}
		}
		GUILayout.EndHorizontal();
	}

	private void FadeScreen()
	{
		if (connectingToRoom)
		{
			fadeDir = 1;
			fadeValue += (float)(fadeDir * 15) * Time.deltaTime;
			fadeValue = Mathf.Clamp01(fadeValue);
			GUI.color = new Color(1f, 1f, 1f, fadeValue);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), blackScreen);
			GUI.color = Color.white;
			GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 15, 150f, 30f), loading);
		}
	}

	private IEnumerator LoadMap(string sceneName)
	{
		connectingToRoom = true;
		PhotonNetwork.isMessageQueueRunning = false;
		fadeDir = 1;
		yield return new WaitForSeconds(1f);
		Application.backgroundLoadingPriority = ThreadPriority.High;
		yield return Application.LoadLevelAsync(sceneName);
		Debug.Log("Loading complete");
	}

	private void CheckPlayerNameAndRoom()
	{
		string text = playerName.Replace(" ", string.Empty);
		if (text == string.Empty)
		{
			playerName = "Player " + UnityEngine.Random.Range(111, 999);
		}
		string text2 = newRoomName.Replace(" ", string.Empty);
		if (text2 == string.Empty)
		{
			newRoomName = "Room Name " + UnityEngine.Random.Range(111, 999);
		}
	}

	private void OnJoinedRoom()
	{
		UnityEngine.MonoBehaviour.print("Joined room: " + newRoomName);
		connectingToRoom = true;
		if (!PhotonNetwork.offlineMode)
		{
			StartCoroutine(LoadMap((string)PhotonNetwork.room.customProperties["MapName"]));
		}
	}

	private void OnJoinedLobby()
	{
		UnityEngine.MonoBehaviour.print("Joined master server");
	}

	private void OnLeftRoom()
	{
		connectingToRoom = false;
	}

	private void OnPhotonJoinRoomFailed()
	{
		UnityEngine.MonoBehaviour.print("Failed on connecting to room");
		connectingToRoom = false;
	}

	private void OnPhotonCreateRoomFailed()
	{
		UnityEngine.MonoBehaviour.print("Failed on creating room");
		connectingToRoom = false;
	}

	private void OnPhotonPlayerConnected()
	{
		UnityEngine.MonoBehaviour.print("Player connected");
	}

	private void OnConnectedToPhoton()
	{
		UnityEngine.MonoBehaviour.print("We connected to Photon Cloud");
		if (PhotonNetwork.room != null)
		{
			PhotonNetwork.LeaveRoom();
		}
		connectingToRoom = false;
	}

	private void OnDisconnectedFromPhoton()
	{
		Application.LoadLevel(1);
		UnityEngine.MonoBehaviour.print("We disconencted from Photon Cloud");
	}
}
