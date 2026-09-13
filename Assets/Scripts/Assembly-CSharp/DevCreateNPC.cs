using Photon;
using UnityEngine;

public class DevCreateNPC : Photon.MonoBehaviour
{
	public string npcname = "PONPC";

	private void Awake()
	{
		if (!base.photonView.isMine)
		{
			Object.Destroy(this);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			PhotonNetwork.Instantiate(npcname, base.transform.position, base.transform.rotation, 0, null);
		}
	}
}
