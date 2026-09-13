using UnityEngine;

public class HideMeshRenderer : MonoBehaviour
{
	public PhotonView player;

	private void Start()
	{
		if (player.isMine)
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void Update()
	{
	}
}
