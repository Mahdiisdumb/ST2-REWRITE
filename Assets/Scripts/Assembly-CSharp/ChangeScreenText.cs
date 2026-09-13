using UnityEngine;

public class ChangeScreenText : MonoBehaviour
{
	public string spanish;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			base.gameObject.GetComponent<GUIText>().text = spanish;
		}
	}

	private void Update()
	{
	}
}
