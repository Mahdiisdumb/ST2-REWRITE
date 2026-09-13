using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
	public NavMeshAgent agent;

	public Transform target;

	private void Start()
	{
	}

	private void Update()
	{
		agent.SetDestination(target.position);
	}
}
