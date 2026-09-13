using System.Collections;
using Photon;
using UnityEngine;

public class PlayerMonsterMechanics : Photon.MonoBehaviour
{
	public bool attack;

	public bool isattacking;

	public GUIText textobj;

	public Transform model;

	public string walk = "run";

	public string idle = "idle";

	public Vector3 oldpos;

	private void Start()
	{
		if (base.photonView.isMine)
		{
			RenderSettings.fog = false;
			RenderSettings.ambientLight = Color.gray;
			textobj.transform.parent = null;
			textobj.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
		}
		else
		{
			Object.Destroy(textobj.gameObject);
		}
	}

	private void Update()
	{
		if (oldpos != base.transform.position)
		{
			if (!isattacking)
			{
				model.GetComponent<Animation>().Play(walk);
			}
		}
		else if (!isattacking)
		{
			model.GetComponent<Animation>().Play(idle);
		}
		if (attack && !isattacking)
		{
			StartCoroutine(Attacking());
			isattacking = true;
		}
		if (base.photonView.isMine && Input.GetKeyDown(KeyCode.Mouse1) && !attack)
		{
			base.photonView.RPC("AttackNow", PhotonTargets.All);
		}
		oldpos = base.transform.position;
	}

	private IEnumerator Attacking()
	{
		isattacking = true;
		model.GetComponent<Animation>().Play("attack");
		if (base.photonView.isMine)
		{
			textobj.enabled = true;
		}
		yield return new WaitForSeconds(2.5f);
		if (base.photonView.isMine)
		{
			textobj.enabled = false;
		}
		isattacking = false;
		attack = false;
	}

	[RPC]
	private void AttackNow()
	{
		attack = true;
	}
}
