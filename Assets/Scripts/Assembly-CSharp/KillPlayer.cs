using UnityEngine;

public class KillPlayer : MonoBehaviour
{
	public Transform deathobj;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player" && other.gameObject.GetComponent<PhotonView>().isMine)
		{
			PhotonNetwork.Destroy(other.gameObject);
			Object.Instantiate(deathobj, base.transform.position, base.transform.rotation);
		}
	}
}
