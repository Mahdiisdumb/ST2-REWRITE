using UnityEngine;

public class NewScream : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player" && !GetComponent<AudioSource>().isPlaying && other.GetComponent<PhotonView>().isMine)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
