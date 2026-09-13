using System.Collections;
using Photon;
using UnityEngine;

public class ChangeSkin : Photon.MonoBehaviour
{
	public string haturl;

	public string headurl;

	public string bodyurl;

	public string armsurl;

	public string legsurl;

	public Transform spotLight;

	public Texture2D head;

	public Texture2D body;

	public Texture2D arms;

	public Texture2D legs;

	public Texture2D hat;

	public Transform headmat;

	public Transform bodymat;

	public Transform leftarmmat;

	public Transform rightarmmat;

	public Transform leftlegmat;

	public Transform rightlegmat;

	public Transform hatmat;

	public bool haschanged;

	private Material[] headmats = new Material[3];

	private void Awake()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		if (array.Length > 6 && !base.photonView.isMine)
		{
			spotLight.gameObject.active = false;
		}
		if (base.photonView.isMine && PlayerPrefs.GetInt("CS") == 2)
		{
			haturl = PlayerPrefs.GetString("haturl");
			headurl = PlayerPrefs.GetString("headurl");
			bodyurl = PlayerPrefs.GetString("bodyurl");
			armsurl = PlayerPrefs.GetString("armsurl");
			legsurl = PlayerPrefs.GetString("legsurl");
		}
		if (PlayerPrefs.GetInt("CS") != 2 && base.photonView.isMine)
		{
			haturl = "n";
		}
	}

	private void Update()
	{
		if (!haschanged && (haturl != string.Empty || headurl != string.Empty || bodyurl != string.Empty || armsurl != string.Empty || legsurl != string.Empty))
		{
			StartCoroutine(ChangeSkinNow());
		}
	}

	private IEnumerator ChangeSkinNow()
	{
		haschanged = true;
		UnityEngine.MonoBehaviour.print("NewSKIN");
		if (haturl.Contains(".png") || haturl.Contains(".jpg") || haturl.Contains(".PNG"))
		{
			WWW www5 = new WWW(haturl);
			yield return www5;
			hat = www5.texture;
			hatmat.GetComponent<Renderer>().material.mainTexture = hat;
		}
		if (bodyurl.Contains(".png") || bodyurl.Contains(".jpg") || bodyurl.Contains(".PNG"))
		{
			WWW www4 = new WWW(bodyurl);
			yield return www4;
			body = www4.texture;
			bodymat.GetComponent<Renderer>().material.mainTexture = body;
		}
		if (headurl.Contains(".png") || headurl.Contains(".jpg") || headurl.Contains(".PNG"))
		{
			WWW www3 = new WWW(headurl);
			yield return www3;
			head = www3.texture;
			headmat.GetComponent<Renderer>().materials[0].mainTexture = head;
			if (body != null)
			{
				headmat.GetComponent<Renderer>().materials[1].mainTexture = body;
				headmat.GetComponent<Renderer>().materials[2].mainTexture = body;
			}
		}
		if (armsurl.Contains(".png") || armsurl.Contains(".jpg") || armsurl.Contains(".PNG"))
		{
			WWW www2 = new WWW(armsurl);
			yield return www2;
			arms = www2.texture;
			leftarmmat.GetComponent<Renderer>().material.mainTexture = arms;
			rightarmmat.GetComponent<Renderer>().material.mainTexture = arms;
		}
		if (legsurl.Contains(".png") || legsurl.Contains(".jpg") || legsurl.Contains(".PNG"))
		{
			WWW www = new WWW(legsurl);
			yield return www;
			legs = www.texture;
			leftlegmat.GetComponent<Renderer>().material.mainTexture = legs;
			rightlegmat.GetComponent<Renderer>().material.mainTexture = legs;
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(haturl);
			stream.SendNext(headurl);
			stream.SendNext(bodyurl);
			stream.SendNext(armsurl);
			stream.SendNext(legsurl);
		}
		else
		{
			haturl = (string)stream.ReceiveNext();
			headurl = (string)stream.ReceiveNext();
			bodyurl = (string)stream.ReceiveNext();
			armsurl = (string)stream.ReceiveNext();
			legsurl = (string)stream.ReceiveNext();
		}
	}
}
