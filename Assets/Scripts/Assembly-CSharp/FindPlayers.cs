using UnityEngine;

public class FindPlayers : MonoBehaviour
{
	public string searchTag = "Player";

	public GameObject[] taggedGameObjects;

	public float curdis;

	public float newdis;

	public Transform detectsound;

	private bool hasspotted;

	public float attackradius = 3.5f;

	public float scanFrequency = 1f;

	public GameObject target;

	private void Start()
	{
		InvokeRepeating("ScanForTarget", 0f, scanFrequency);
	}

	private void Update()
	{
		if (target == null)
		{
			ScanForTarget();
		}
		else
		{
			newdis = Vector3.Distance(target.transform.position, base.transform.position);
		}
		if (newdis <= curdis && !hasspotted)
		{
			hasspotted = true;
			base.gameObject.GetComponent<SlendyTankMovement>().chase = true;
			detectsound.GetComponent<AudioSource>().Play();
		}
		if (newdis <= attackradius)
		{
			base.gameObject.GetComponent<SlendyTankMovement>().attack = true;
		}
	}

	private void ScanForTarget()
	{
		target = GetNearestTaggedObject();
	}

	private GameObject GetNearestTaggedObject()
	{
		float num = float.PositiveInfinity;
		taggedGameObjects = GameObject.FindGameObjectsWithTag(searchTag);
		GameObject result = null;
		GameObject[] array = taggedGameObjects;
		foreach (GameObject gameObject in array)
		{
			Vector3 position = gameObject.transform.position;
			float sqrMagnitude = (position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				result = gameObject;
				num = sqrMagnitude;
			}
		}
		return result;
	}
}
