using System.Collections.Generic;
using Photon;
using UnityEngine;

public class PlayerHandler : Photon.MonoBehaviour
{
	public List<GameObject> remoteObjectsToDeactivate;

	public List<UnityEngine.MonoBehaviour> remoteScriptsToDeactivate;

	public List<GameObject> localObjectsToDeactivate;

	public List<UnityEngine.MonoBehaviour> localScriptsToDeactivate;

	private Vector3 correctPlayerPos = new Vector3(0f, -100f, 0f);

	private Quaternion correctPlayerRot = Quaternion.identity;

	private void Start()
	{
		if (!base.photonView.isMine)
		{
			for (int i = 0; i < remoteObjectsToDeactivate.Count; i++)
			{
				Object.Destroy(remoteObjectsToDeactivate[i]);
			}
			for (int j = 0; j < remoteScriptsToDeactivate.Count; j++)
			{
				remoteScriptsToDeactivate[j].enabled = false;
			}
		}
		else
		{
			for (int k = 0; k < localObjectsToDeactivate.Count; k++)
			{
				localObjectsToDeactivate[k].active = false;
			}
			for (int l = 0; l < localScriptsToDeactivate.Count; l++)
			{
				Object.Destroy(localScriptsToDeactivate[l]);
			}
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
		}
		else
		{
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 8f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, correctPlayerRot, Time.deltaTime * 8f);
		}
	}
}
