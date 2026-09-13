using UnityEngine;

public class CheckCamSettings : MonoBehaviour
{
	public bool anaglyphmode;

	public bool ovrmode;

	public Transform ovr;

	public Transform camerastuff;

	private void Awake()
	{
		Cursor.visible = false;
		Screen.lockCursor = true;
		if (PlayerPrefs.GetInt("cameramode") == 1)
		{
			anaglyphmode = true;
		}
		if (PlayerPrefs.GetInt("cameramode") == 2)
		{
			ovrmode = true;
		}
		if (anaglyphmode)
		{
			GetComponent<AnaglyphizerC>().enabled = true;
		}
		if (ovrmode)
		{
			camerastuff.parent = ovr;
			Object.Destroy(base.gameObject.GetComponent<AudioListener>());
			ovr.gameObject.SetActive(true);
			base.enabled = false;
		}
	}
}
