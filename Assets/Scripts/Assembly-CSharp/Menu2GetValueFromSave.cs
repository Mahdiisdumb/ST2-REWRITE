using UnityEngine;

public class Menu2GetValueFromSave : MonoBehaviour
{
	public string nameOfSave;

	private void Update()
	{
		GetComponent<TextMesh>().text = PlayerPrefs.GetInt(nameOfSave).ToString();
	}
}
