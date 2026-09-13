using UnityEngine;

public class CharacterSkinWindow : MonoBehaviour
{
	public GUISkin guiSkin;

	public string cs = "Character Custom Skin";

	public string hat = "Hat Texture";

	public string head = "Head Texture";

	public string body = "Body Texture";

	public string arms = "Arms Texture";

	public string legs = "Legs Texture";

	public new string enabled = "Enabled";

	public string disabled = "Disabled";

	public string accept = "Accept";

	public string gms = "Get More Skins";

	public string haturl;

	public string headurl;

	public string bodyurl;

	public string armsurl;

	public string legsurl;

	public int isenabled;

	public bool showwindow;

	private void Awake()
	{
		haturl = PlayerPrefs.GetString("haturl");
		headurl = PlayerPrefs.GetString("headurl");
		bodyurl = PlayerPrefs.GetString("bodyurl");
		armsurl = PlayerPrefs.GetString("armsurl");
		legsurl = PlayerPrefs.GetString("legsurl");
		isenabled = PlayerPrefs.GetInt("CS");
		if (PlayerPrefs.GetInt("language") == 1)
		{
			cs = "Skin Customizable del Personaje";
			hat = "Gorro";
			head = "Cabeza";
			body = "Cuerpo";
			arms = "Brazos";
			legs = "Piernas";
			accept = "Aceptar";
			enabled = "Encendido";
			disabled = "Desactivado";
			gms = "Obtener más skins";
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 40, 300f, 40f), cs))
		{
			showwindow = !showwindow;
		}
		GUI.skin = null;
		if (showwindow)
		{
			Rect clientRect = new Rect(Screen.width - 310, 20f, 300f, 370f);
			clientRect = GUI.Window(0, clientRect, DoMyWindow, cs);
		}
	}

	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(10f, 20f, 100f, 20f), hat);
		haturl = GUI.TextField(new Rect(10f, 40f, 280f, 20f), haturl);
		GUI.Label(new Rect(10f, 70f, 100f, 20f), head);
		headurl = GUI.TextField(new Rect(10f, 90f, 280f, 20f), headurl);
		GUI.Label(new Rect(10f, 120f, 100f, 20f), body);
		bodyurl = GUI.TextField(new Rect(10f, 140f, 280f, 20f), bodyurl);
		GUI.Label(new Rect(10f, 170f, 100f, 20f), arms);
		armsurl = GUI.TextField(new Rect(10f, 190f, 280f, 20f), armsurl);
		GUI.Label(new Rect(10f, 220f, 100f, 20f), legs);
		legsurl = GUI.TextField(new Rect(10f, 240f, 280f, 20f), legsurl);
		if (isenabled != 2)
		{
			if (GUI.Button(new Rect(10f, 280f, 280f, 30f), disabled))
			{
				isenabled = 2;
			}
		}
		else if (GUI.Button(new Rect(10f, 280f, 280f, 30f), enabled))
		{
			isenabled = 0;
		}
		if (GUI.Button(new Rect(10f, 310f, 280f, 20f), gms))
		{
			Application.OpenURL("http://zeoworks.com/home/forum-17.html");
		}
		if (GUI.Button(new Rect(10f, 330f, 280f, 30f), accept))
		{
			PlayerPrefs.SetString("haturl", haturl);
			PlayerPrefs.SetString("headurl", headurl);
			PlayerPrefs.SetString("bodyurl", bodyurl);
			PlayerPrefs.SetString("armsurl", armsurl);
			PlayerPrefs.SetString("legsurl", legsurl);
			PlayerPrefs.SetInt("CS", isenabled);
			showwindow = false;
		}
	}
}
