using UnityEngine;

public class PlayerModelMovement : MonoBehaviour
{
	private Vector3 oldpos;

	public Transform player;

	private void Awake()
	{
		oldpos = player.position;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (oldpos != player.position)
		{
			GetComponent<Animation>().CrossFade("Movement");
		}
		else
		{
			GetComponent<Animation>().CrossFade("Idle");
		}
		oldpos = player.position;
	}
}
