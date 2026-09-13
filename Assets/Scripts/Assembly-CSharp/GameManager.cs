using Photon;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour
{
	public Transform playerPrefab;

	public void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(2);
		}
		else
		{
			PhotonNetwork.isMessageQueueRunning = true;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PhotonNetwork.LeaveRoom();
			Application.LoadLevel(1);
		}
	}

	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		if (PhotonNetwork.connected)
		{
			base.photonView.RPC("SendChatMessage", PhotonNetwork.masterClient, "YOU ARE THE NEW HOST From:" + PhotonNetwork.player);
		}
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}

	public void OnReceivedRoomList()
	{
		Debug.Log("OnReceivedRoomList");
	}

	public void OnReceivedRoomListUpdate()
	{
		Debug.Log("OnReceivedRoomListUpdate");
	}

	public void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
	}

	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);
	}
}
