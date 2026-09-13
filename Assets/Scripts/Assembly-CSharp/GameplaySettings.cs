using UnityEngine;

public class GameplaySettings : MonoBehaviour
{
	public bool showgui;

	public Texture2D black;

	public string cameratype = "Camera Mode:";

	public string accept = "Return";

	public string change = "Change";

	public int mode;

	public float value = 10f;

	public string modename;

	public string mousesensitivity = "Mouse Sensitivity";

	private void Awake()
	{
		mode = PlayerPrefs.GetInt("cameramode");
		if (PlayerPrefs.GetFloat("mouse") == 0f)
		{
			PlayerPrefs.SetFloat("mouse", 10f);
		}
		else
		{
			value = PlayerPrefs.GetFloat("mouse");
		}
		if (PlayerPrefs.GetInt("language") == 1)
		{
			cameratype = "Modo de Cámara:";
			accept = "Volver";
			mousesensitivity = "Sensibilidad del Mouse";
			change = "Cambiar";
		}
	}

	private void Update()
	{
		if (mode > 2)
		{
			mode = 0;
		}
		if (mode == 0)
		{
			modename = "    Normal";
		}
		if (mode == 1)
		{
			modename = "3D Anaglyph";
		}
		if (mode == 2)
		{
			modename = "Oculus Rift";
		}
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
			GUI.Label(new Rect(Screen.width / 2 - 60, 10f, 300f, 25f), mousesensitivity);
			value = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 40f, 200f, 25f), value, 1f, 15f);
			GUI.Label(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 35, 300f, 20f), cameratype);
			GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 10, 200f, 20f), modename);
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100f, 20f), change))
			{
				mode++;
			}
			PlayerPrefs.SetInt("cameramode", mode);
			PlayerPrefs.SetFloat("mouse", value);
			if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 30, 100f, 20f), accept))
			{
				showgui = false;
			}
		}
	}
}
