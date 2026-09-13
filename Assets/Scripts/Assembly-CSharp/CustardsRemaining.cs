using Photon;
using UnityEngine;

public class CustardsRemaining : Photon.MonoBehaviour
{
	public GameObject textobj;

	private void Start()
	{
		textobj = GameObject.Find("CustardsRemaining");
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player" && base.photonView.isMine)
		{
			base.photonView.RPC("CollectNow", PhotonTargets.AllBuffered);
		}
	}

	[RPC]
	private void CollectNow()
	{
		textobj.GetComponent<CustardCounter>().showcounter = true;
		if (base.photonView.isMine)
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
	}
}
