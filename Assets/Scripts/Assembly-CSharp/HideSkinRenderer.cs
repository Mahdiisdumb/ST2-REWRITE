using UnityEngine;

public class HideSkinRenderer : MonoBehaviour
{
	public PhotonView player;

	private void Start()
	{
		if (player.isMine)
		{
			GetComponent<SkinnedMeshRenderer>().enabled = false;
		}
	}

	private void Update()
	{
	}
}
