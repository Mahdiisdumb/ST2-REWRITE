using UnityEngine;

public class GuiTextLanguage : MonoBehaviour
{
	public string spanish;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			GetComponent<TextMesh>().text = spanish;
		}
	}
}
