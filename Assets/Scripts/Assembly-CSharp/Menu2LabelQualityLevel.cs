using UnityEngine;

public class Menu2LabelQualityLevel : MonoBehaviour
{
	private void Update()
	{
		int @int = PlayerPrefs.GetInt("tmpQualitySettings");
		GetComponent<TextMesh>().text = QualitySettings.names[@int].ToString();
	}
}
