using UnityEngine;

public class Menu2LabelDifficulty : MonoBehaviour
{
	private void Update()
	{
		GetComponent<TextMesh>().text = PlayerPrefs.GetString("difficultyName");
	}
}
