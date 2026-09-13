using CodeStage.AntiCheat.ObscuredTypes;
using Photon;
using UnityEngine;

public class PlayerName : Photon.MonoBehaviour
{
	public string playerName;

	public string playerType;

	public Transform positionDisplay;

	public bool showname = true;

	private string tempname;

	private string temptype;

	public Texture2D crown;

	private bool hidename;

	public bool isadmin;

	private void Awake()
	{
		if (!positionDisplay)
		{
			if (!base.photonView.isMine)
			{
				positionDisplay = base.transform.Find("multiplayertubby/Cylinder002");
			}
			else
			{
				positionDisplay = base.transform;
			}
		}
		if (base.photonView.isMine)
		{
			if (ObscuredPrefs.GetString("PlayerType") == string.Empty)
			{
				tempname = ObscuredPrefs.GetString("PlayerName");
			}
			else
			{
				tempname = ObscuredPrefs.GetString("ZWName");
			}
			temptype = ObscuredPrefs.GetString("PlayerType");
			if (tempname == string.Empty)
			{
				tempname = "Guest";
			}
			playerType = temptype;
			base.photonView.RPC("SetName", PhotonTargets.AllBuffered, tempname, temptype);
			hidename = true;
		}
		if (GameObject.Find("Mechanics").GetComponent<Mechanics>().isversus)
		{
			showname = false;
		}
	}

	private void Start()
	{
		if (base.gameObject.name == "player(Clone)" || base.gameObject.name == "zeoworksplayer(Clone)")
		{
			base.gameObject.name = "Player";
		}
	}

	private void Update()
	{
		playerName = base.gameObject.name;
		if (base.gameObject.name.Contains("<"))
		{
			base.gameObject.name = base.gameObject.name.Replace("<", string.Empty);
		}
	}

	private void OnGUI()
	{
		GUI.depth = 2;
		float num = 0f;
		if (!showname || hidename)
		{
			return;
		}
		if (isadmin)
		{
			GUI.color = Color.red;
		}
		else
		{
			GUI.color = Color.white;
		}
		if (!Camera.main)
		{
			return;
		}
		Vector3 vector = Camera.main.WorldToScreenPoint(positionDisplay.position);
		if (vector.z * 3f < 50f)
		{
			num = vector.z * 3f;
		}
		else
		{
			num = 50f;
		}
		if (vector.z > 0f)
		{
			if (playerName.Contains("<"))
			{
				playerName = playerName.Replace("<", string.Empty);
			}
			string text = playerType;
			if (text == "2")
			{
				GUI.color = Color.green;
			}
			if (text == "3")
			{
				GUI.color = Color.cyan;
			}
			if (text == "4")
			{
				GUI.color = Color.red;
			}
			if (text == "5")
			{
				GUI.color = Color.grey;
			}
			if (text == "6")
			{
				GUI.color = Color.cyan;
			}
			if (text == "7")
			{
				GUI.color = Color.grey;
			}
			if (text == "8")
			{
				GUI.color = Color.red;
			}
			if (text == "9")
			{
				GUI.color = Color.yellow;
			}
			if (text == "10")
			{
				GUI.color = Color.blue;
			}
			GUI.Label(new Rect(vector.x - 30f, (float)Screen.height - vector.y - 1f - 21f, 200f, 30f), playerName);
		}
	}

	[RPC]
	private void SetName(string newname, string type)
	{
		if (base.photonView.isMine)
		{
			newname = tempname;
			type = temptype;
		}
		base.gameObject.name = newname;
		playerType = type;
	}
}
