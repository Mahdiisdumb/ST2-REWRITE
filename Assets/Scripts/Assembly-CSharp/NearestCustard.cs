using UnityEngine;

public class NearestCustard : MonoBehaviour
{
	public string searchTag = "custard";

	public GameObject[] taggedGameObjects;

	public float curdis;

	public float newdis;

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
