using System.Collections;
using Photon;
using UnityEngine;

public class SpawnBlocks : Photon.MonoBehaviour
{
	public string blockname = "wood";

	public GameObject[] allblocks;

	private IEnumerator Start()
	{
		allblocks = GameObject.FindGameObjectsWithTag("blockpiece");
		GameObject[] array = allblocks;
		foreach (GameObject gameObject in array)
		{
			GameObject gameObject2 = PhotonNetwork.Instantiate(blockname, base.transform.position, base.transform.rotation, 0);
			if (gameObject.transform.parent == null)
			{
				gameObject2.transform.position = gameObject.transform.position;
				gameObject2.transform.rotation = gameObject.transform.rotation;
				gameObject2.transform.localScale = gameObject.transform.localScale;
			}
			else
			{
				gameObject.transform.parent = null;
				gameObject2.transform.position = gameObject.transform.position;
				gameObject2.transform.rotation = gameObject.transform.rotation;
				gameObject2.transform.localScale = gameObject.transform.localScale;
				Object.Destroy(gameObject);
			}
		}
		yield return new WaitForSeconds(1f);
		PhotonNetwork.Destroy(base.gameObject);
	}
}
