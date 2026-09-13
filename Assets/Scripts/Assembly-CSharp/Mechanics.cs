using System.Collections;
using Photon;
using UnityEngine;

public class Mechanics : Photon.MonoBehaviour
{
	public GameObject[] custardpos;

	public Transform custard;

	public bool showobjective;

	public Transform startpoint;

	public bool gamestart;

	public Transform NPC1;

	public Transform NPC2;

	public Transform versuspoint;

	public GameObject myplayer;

	public GameObject mymonster;

	public int maxcustards = 10;

	public int custardsleft;

	public string npcplayer = "playerTank";

	private string objective;

	public bool isversus;

	public bool atbeginning;

	public GameManager gm;

	private void Awake()
	{
		npcplayer += "1";
		gm = GetComponent<GameManager>();
		if (base.photonView.isMine)
		{
			maxcustards = PlayerPrefs.GetInt("custardamount");
			atbeginning = true;
		}
		else
		{
			maxcustards = GameObject.FindGameObjectsWithTag("custard").Length;
		}
		if (PhotonNetwork.isMasterClient)
		{
			if (PlayerPrefs.GetString("gamemode") == "DM")
			{
				mymonster = PhotonNetwork.Instantiate(npcplayer, versuspoint.position, versuspoint.rotation, 0, null);
				base.photonView.RPC("IsVersus", PhotonTargets.AllBuffered);
			}
			else
			{
				myplayer = PhotonNetwork.Instantiate("player", startpoint.position, startpoint.rotation, 0, null);
			}
		}
		else
		{
			myplayer = PhotonNetwork.Instantiate("player", startpoint.position, startpoint.rotation, 0, null);
		}
	}

	private void Update()
	{
		if (PhotonNetwork.isMasterClient && isversus && mymonster == null)
		{
			PhotonNetwork.Destroy(myplayer.gameObject);
			mymonster = PhotonNetwork.Instantiate(npcplayer, versuspoint.position, versuspoint.rotation, 0, null);
		}
		if (base.photonView.isMine && atbeginning)
		{
			if (custardsleft < maxcustards)
			{
				custardpos = GameObject.FindGameObjectsWithTag("custardPOS");
				GameObject[] array = custardpos;
				foreach (GameObject gameObject in array)
				{
					if (custardsleft < maxcustards)
					{
						int num = Random.Range(1, 3);
						if (num == 1)
						{
							PhotonNetwork.InstantiateSceneObject("custard", gameObject.transform.position, gameObject.transform.rotation, 0, null);
							gameObject.transform.tag = "Untagged";
							custardsleft++;
						}
					}
				}
			}
			if (custardsleft == maxcustards && !showobjective)
			{
				StartCoroutine(ShowObjectiveNow());
			}
		}
		else if (!showobjective)
		{
			maxcustards = GameObject.FindGameObjectsWithTag("custard").Length;
			StartCoroutine(ShowObjectiveNow());
		}
		if (gamestart && GameObject.FindGameObjectsWithTag("custard").Length == 0)
		{
			PhotonNetwork.LeaveRoom();
			Application.LoadLevel("Win");
		}
	}

	private IEnumerator ShowObjectiveNow()
	{
		showobjective = true;
		if (PlayerPrefs.GetInt("language") == 1)
		{
			objective = "Recoge todas " + maxcustards + " las tubipapillas";
		}
		else
		{
			objective = "Collect All " + maxcustards + " Teletubby Custards";
		}
		base.gameObject.GetComponent<GUIText>().text = objective;
		base.gameObject.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(5f);
		base.gameObject.GetComponent<GUIText>().enabled = false;
		gamestart = true;
	}

	[RPC]
	private void IsVersus()
	{
		isversus = true;
	}
}
