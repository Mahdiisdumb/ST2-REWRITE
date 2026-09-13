using UnityEngine;

public class Menu2ChangeMenu : MonoBehaviour
{
	public string menu;

	public Color overColor;

	public Color exitColor;

	public bool loadmap;

	public bool issingleplayer;

	public Texture2D image;

	public GameObject preview;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
	}

	private void OnMouseDown()
	{
		GameObject.Find("root").GetComponent<Menu2Root>().Show(menu);
		if (menu == "Online")
		{
			GameObject.Find("root").GetComponent<ServerSelect>().enabled = true;
		}
		if (issingleplayer && loadmap)
		{
			PlayerPrefs.SetString("gamemode", "TDM");
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom("Singleplayer");
			Application.LoadLevel(menu);
		}
	}

	private void OnMouseOver()
	{
		GetComponent<Renderer>().material.color = overColor;
		if (issingleplayer && loadmap)
		{
			preview.GetComponent<Renderer>().material.mainTexture = image;
		}
	}

	private void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = exitColor;
	}
}
