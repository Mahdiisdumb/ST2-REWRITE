using UnityEngine;

public class DeathobjAnimation : MonoBehaviour
{
	public Transform deadtext;

	public Transform normalcam;

	public Transform ovrcam;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("cammode") == 2)
		{
			normalcam.gameObject.SetActive(false);
		}
		else
		{
			ovrcam.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			deadtext.gameObject.GetComponent<GUIText>().text = "Estas muerto";
		}
		deadtext.parent = null;
		deadtext.localPosition = new Vector3(0.5f, 0.5f, 0f);
	}

	private void Update()
	{
		base.transform.Translate(Vector3.down * Time.deltaTime);
	}
}
