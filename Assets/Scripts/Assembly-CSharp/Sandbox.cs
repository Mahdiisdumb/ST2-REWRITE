using Photon;
using UnityEngine;

public class Sandbox : Photon.MonoBehaviour
{
	public int daytime;

	public int music;

	public int trees;

	public int disco;

	public Transform terrain;

	public string commandbox;

	public Material daymat;

	public Material nightmat;

	public Transform sunlight;

	public Color dayfog;

	public Color nightfog;

	public Color daycolor;

	public Color nightcolor;

	public Color[] discocolors;

	public AudioClip[] themusic;

	private float treetot;

	private void Start()
	{
		terrain = GameObject.Find("Terrain").transform;
		sunlight = GameObject.Find("Directional light").transform;
		if (terrain != null)
		{
			treetot = terrain.GetComponent<Terrain>().treeDistance;
		}
	}

	private void Update()
	{
		if (daytime == 1 && disco == 0)
		{
			RenderSettings.skybox = daymat;
			RenderSettings.fogColor = dayfog;
			if (sunlight != null)
			{
				sunlight.gameObject.active = true;
			}
			RenderSettings.ambientLight = daycolor;
		}
		if (daytime == 2 && disco == 0)
		{
			RenderSettings.skybox = nightmat;
			RenderSettings.fogColor = nightfog;
			if (sunlight != null)
			{
				sunlight.gameObject.active = false;
			}
			RenderSettings.ambientLight = nightcolor;
		}
		if (disco == 1)
		{
			int num = Random.Range(0, 10);
			int num2 = Random.Range(0, 15);
			if (num2 == 1)
			{
				RenderSettings.ambientLight = discocolors[num];
			}
		}
		if (trees == 0 && terrain != null)
		{
			terrain.GetComponent<Terrain>().treeDistance = treetot;
		}
		if (trees == 1 && terrain != null)
		{
			terrain.GetComponent<Terrain>().treeDistance = 0f;
		}
		if (music == 0)
		{
			GetComponent<AudioSource>().Stop();
		}
		if (music > 0)
		{
			GetComponent<AudioSource>().clip = themusic[music];
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		if (base.photonView.isMine && Input.GetKeyDown(KeyCode.K))
		{
			GameObject.Find("Mechanics").GetComponent<MultiplayerChat>().enabled = !GameObject.Find("Mechanics").GetComponent<MultiplayerChat>().enabled;
		}
	}

	private void ChangeNow()
	{
		if (commandbox.Contains("time:0") && disco == 0)
		{
			daytime = 1;
			RenderSettings.ambientLight = daycolor;
		}
		if (commandbox.Contains("time:1") && disco == 0)
		{
			daytime = 2;
			RenderSettings.ambientLight = nightcolor;
		}
		if (commandbox.Contains("hidetrees:0"))
		{
			trees = 0;
		}
		if (commandbox.Contains("hidetrees:1"))
		{
			trees = 1;
		}
		if (commandbox.Contains("disco:0"))
		{
			disco = 0;
		}
		if (commandbox.Contains("disco:1"))
		{
			disco = 1;
		}
		if (commandbox.Contains("music:"))
		{
			string[] array = commandbox.Split(":"[0]);
			int num = int.Parse(array[1]);
			music = num;
		}
		if (commandbox.Contains("kick:"))
		{
			string[] array2 = commandbox.Split(":"[0]);
			GameObject gameObject = GameObject.Find(array2[1]);
			PhotonPlayer owner = gameObject.GetComponent<PhotonView>().owner;
			PhotonNetwork.CloseConnection(owner);
		}
		if (commandbox.Contains("spawn:"))
		{
			string[] array3 = commandbox.Split(":"[0]);
			PhotonNetwork.Instantiate(array3[1], base.transform.position, base.transform.rotation, 0);
		}
		if (commandbox.Contains("hostspawn:"))
		{
			string[] array4 = commandbox.Split(":"[0]);
			PhotonNetwork.InstantiateSceneObject(array4[1], base.transform.position, base.transform.rotation, 0, null);
		}
	}

	private void OnGUI()
	{
		if (base.photonView.isMine)
		{
			GUI.SetNextControlName("SandboxCommand");
			commandbox = GUI.TextField(new Rect(Screen.width - 200, 0f, 200f, 30f), commandbox);
			if (GUI.Button(new Rect(Screen.width - 100, 30f, 100f, 30f), "SEND"))
			{
				ChangeNow();
			}
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(daytime);
			stream.SendNext(music);
			stream.SendNext(trees);
			stream.SendNext(disco);
		}
		else
		{
			daytime = (int)stream.ReceiveNext();
			music = (int)stream.ReceiveNext();
			trees = (int)stream.ReceiveNext();
			disco = (int)stream.ReceiveNext();
		}
	}
}
