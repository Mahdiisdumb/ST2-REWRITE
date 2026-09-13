using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class Menu2Root : MonoBehaviour
{
	public GameObject[] menus;

	public GameObject show;

	public float damp = 0.2f;

	private GameObject down;

	private GameObject up;

	private float yVelocity;

	public Texture2D welcome;

	public Texture2D welcomespanish;

	public bool showwelcome;

	public string exit = "Show me the controls!";

	private string login = "Login To ZeoWorks Account";

	private string logout = "Log Out";

	public bool haslogin;

	public GUISkin guiSkin;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			exit = "¡Muestrame los controles!";
			welcome = welcomespanish;
			logout = "Cerrar Sesión";
			login = "Iniciar Sesión en Cuenta de ZeoWorks";
		}
	}

	private void Start()
	{
		string @string = ObscuredPrefs.GetString("ZWName");
		if (@string != string.Empty)
		{
			haslogin = true;
		}
		for (int i = 0; i < menus.Length; i++)
		{
			menus[i].transform.position = new Vector3(0f, -55f, 0f);
		}
		show.transform.position = new Vector3(-5f, 0f, 5f);
		Cursor.visible = true;
		Screen.lockCursor = false;
		if (PlayerPrefs.GetInt("firsttime") == 0)
		{
			PlayerPrefs.SetInt("firsttime", 1);
		}
	}

	private void Update()
	{
		if (down != null)
		{
			float y = Mathf.SmoothDamp(down.transform.position.y, -15f, ref yVelocity, damp);
			down.transform.position = new Vector3(down.transform.position.x, y, down.transform.position.z);
			if (down.transform.position.y <= -14.5f)
			{
				down = null;
			}
		}
		else if (up != null)
		{
			float y2 = Mathf.SmoothDamp(up.transform.position.y, 0f, ref yVelocity, damp);
			up.transform.position = new Vector3(up.transform.position.x, y2, up.transform.position.z);
			if (up.transform.position.y >= -0.5f)
			{
				up = null;
			}
		}
	}

	public void Show(string _show)
	{
		down = show;
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].name == _show)
			{
				up = menus[i];
				show = up;
			}
		}
	}

	private void OnGUI()
	{
		if (showwelcome)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), welcome);
			if (GUI.Button(new Rect(Screen.width - 180, Screen.height - 35, 170f, 25f), exit))
			{
				showwelcome = false;
			}
			return;
		}
		GUI.skin = guiSkin;
		if (GetComponent<ServerSelect>().enabled || GetComponent<Login>().enabled)
		{
			return;
		}
		if (!haslogin)
		{
			if (GUI.Button(new Rect(Screen.width - 250, 0f, 250f, 50f), "<size=12>" + login + "</size>"))
			{
				GetComponent<Login>().enabled = true;
				Show("Online");
			}
		}
		else if (GUI.Button(new Rect(Screen.width - 230, 0f, 230f, 50f), logout))
		{
			ObscuredPrefs.SetString("ZWName", string.Empty);
			ObscuredPrefs.SetString("PlayerType", string.Empty);
			haslogin = false;
		}
	}
}
