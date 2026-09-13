using UnityEngine;

public class CheckRain : MonoBehaviour
{
	private void Awake()
	{
		if (GameObject.Find("HasRain") == null)
		{
			base.gameObject.active = false;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
