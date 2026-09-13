using UnityEngine;

public class CustomID : MonoBehaviour
{
	public bool showgui;

	public string id;

	public Texture2D black;

	public string customid = "Custom Server ID:";

	public string accept = "Return";

	private void Awake()
	{
		id = PlayerPrefs.GetString("id");
		if (PlayerPrefs.GetInt("language") == 1)
		{
			customid = "ID de Servidor Personalizado:";
			accept = "Volver";
		}
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		showgui = true;
	}

	private void OnGUI()
	{
		if (showgui)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), black);
			GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 2 - 35, 300f, 20f), customid);
			id = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200f, 20f), id);
			PlayerPrefs.SetString("id", id);
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 35, 100f, 20f), accept))
			{
				showgui = false;
			}
		}
	}
}
