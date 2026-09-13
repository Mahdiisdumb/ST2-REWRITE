using UnityEngine;
using UnityEngine.UI;

public class TranslateText : MonoBehaviour
{
	public string[] translation;

	public int newSize;

	private void Start()
	{
		int @int = PlayerPrefs.GetInt("Language");
		if (@int > 0)
		{
			GetComponent<Text>().text = translation[@int - 1];
			if (newSize > 0)
			{
				GetComponent<Text>().fontSize = newSize;
			}
		}
	}
}
