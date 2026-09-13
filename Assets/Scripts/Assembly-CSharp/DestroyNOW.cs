using Photon;

public class DestroyNOW : MonoBehaviour
{
	private void Start()
	{
		PhotonNetwork.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
