using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.AI;

public class SlendyTankMovement : Photon.MonoBehaviour
{
	public bool isskintubby;

	public Transform target;

	public NavMeshAgent agent;

	public float walkspeed = 6f;

	public float runspeed = 12f;

	public Transform model;

	public bool chase;

	public bool attack;

	private bool isattacking;

	private Vector3 oldpos;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		oldpos = base.transform.position;
	}

	private void Start()
	{
		if (PhotonNetwork.isMasterClient && GameObject.Find("Mechanics").GetComponent<Mechanics>().isversus)
		{
			base.photonView.RPC("DestoryNPC", PhotonTargets.AllBuffered);
		}
	}

	private void Update()
	{
		if (oldpos != base.transform.position)
		{
			if (chase)
			{
				if (!attack)
				{
					if (!isskintubby)
					{
						model.GetComponent<Animation>().Play("run");
					}
					else
					{
						model.GetComponent<Animation>().Play("skintubby_moving");
						model.GetComponent<Animation>()["skintubby_moving"].speed = 3f;
					}
					agent.speed = runspeed;
				}
			}
			else if (!attack)
			{
				if (!isskintubby)
				{
					model.GetComponent<Animation>().Play("walk");
				}
				else
				{
					model.GetComponent<Animation>().Play("skintubby_moving");
					model.GetComponent<Animation>()["skintubby_moving"].speed = 1f;
				}
				agent.speed = walkspeed;
			}
		}
		else if (!attack)
		{
			if (!isskintubby)
			{
				model.GetComponent<Animation>().Play("idle");
			}
			else
			{
				model.GetComponent<Animation>().Play("skintubby_idle");
				model.GetComponent<Animation>()["skintubby_idle"].speed = 1f;
			}
		}
		if (chase)
		{
			if (!attack)
			{
				target = base.gameObject.GetComponent<FindPlayers>().target.transform;
				if (base.photonView.isMine)
				{
					agent.SetDestination(target.position);
				}
			}
		}
		else
		{
			target = base.gameObject.GetComponent<FindPlayers>().target.transform;
			if (base.photonView.isMine)
			{
				agent.SetDestination(target.gameObject.GetComponent<NearestCustard>().target.transform.position);
			}
		}
		if (attack && !isattacking)
		{
			StartCoroutine(AttackNow());
		}
		if (base.photonView.isMine)
		{
			GetComponent<NavMeshAgent>().enabled = true;
		}
		else
		{
			GetComponent<NavMeshAgent>().enabled = false;
		}
		oldpos = base.transform.position;
	}

	private IEnumerator AttackNow()
	{
		base.transform.LookAt(target.position);
		isattacking = true;
		agent.enabled = false;
		model.GetComponent<Animation>().Play("attack");
		yield return new WaitForSeconds(2f);
		attack = false;
		isattacking = false;
		agent.enabled = true;
	}

	[RPC]
	private void DestoryNPC()
	{
		Object.Destroy(base.gameObject);
	}
}
