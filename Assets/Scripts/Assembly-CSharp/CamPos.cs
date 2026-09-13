using UnityEngine;

public class CamPos : MonoBehaviour
{
	public Transform orgpos;

	public Transform newpos;

	public Camera screencam;

	public Camera MainCamera;

	private Camera camleft;

	private Camera camright;

	public bool isanaglyph;

	public bool isovr;

	private bool hascams;

	public bool iszoomed;

	public float myTimer = 10f;

	public Transform remaining;

	public bool changesettings;

	private bool canuse = true;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("cameramode") == 1)
		{
			isanaglyph = true;
		}
		if (PlayerPrefs.GetInt("cameramode") == 2)
		{
			isovr = true;
		}
		remaining.parent = null;
		remaining.localPosition = new Vector3(0.1f, 0.9f, 0f);
	}

	private void Update()
	{
		myTimer = 10f;
		if (Input.GetKeyDown(KeyCode.Mouse1) && myTimer > 0f && canuse)
		{
			iszoomed = !iszoomed;
		}
		if (myTimer > 0f && iszoomed)
		{
			myTimer -= Time.deltaTime;
		}
		if (myTimer <= 0f && iszoomed)
		{
			myTimer = 0.1f;
			canuse = false;
			iszoomed = false;
			myTimer += Time.deltaTime / 1.5f;
			remaining.GetComponent<GUIText>().color = Color.red;
		}
		if (myTimer >= 0f && !canuse)
		{
			myTimer += Time.deltaTime;
		}
		if (!canuse && myTimer >= 10f)
		{
			myTimer = 10f;
			remaining.GetComponent<GUIText>().color = Color.white;
			canuse = true;
		}
		if (isanaglyph && !hascams)
		{
			camleft = GameObject.Find("leftEye").GetComponent<Camera>();
			camright = GameObject.Find("rightEye").GetComponent<Camera>();
			hascams = true;
		}
		if (isovr && !hascams)
		{
			camleft = GameObject.Find("CameraLeft").GetComponent<Camera>();
			camright = GameObject.Find("CameraRight").GetComponent<Camera>();
			hascams = true;
		}
		if (iszoomed)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, newpos.position, 1.5f * Time.deltaTime);
			if (MainCamera.fieldOfView > 24f)
			{
				MainCamera.fieldOfView -= Time.deltaTime * 70f * 2f;
				if (hascams)
				{
					camleft.fieldOfView -= Time.deltaTime * 70f * 2f;
					camright.fieldOfView -= Time.deltaTime * 70f * 2f;
				}
			}
			else
			{
				MainCamera.fieldOfView = 24f;
				if (hascams)
				{
					camleft.fieldOfView = 24f;
					camright.fieldOfView = 24f;
					camleft.cullingMask = 1 << LayerMask.NameToLayer("CamCorder");
					camleft.clearFlags = CameraClearFlags.Color;
					camright.cullingMask = 1 << LayerMask.NameToLayer("CamCorder");
					camright.clearFlags = CameraClearFlags.Color;
				}
				RenderSettings.fog = false;
				MainCamera.cullingMask = 1 << LayerMask.NameToLayer("CamCorder");
				MainCamera.clearFlags = CameraClearFlags.Color;
				changesettings = true;
			}
		}
		else
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, orgpos.position, 1.5f * Time.deltaTime);
			if (MainCamera.fieldOfView < 60f)
			{
				MainCamera.fieldOfView += Time.deltaTime * 70f * 2f;
				if (hascams)
				{
					camleft.fieldOfView += Time.deltaTime * 70f * 2f;
					camright.fieldOfView += Time.deltaTime * 70f * 2f;
				}
				if (changesettings)
				{
					RenderSettings.fog = true;
					MainCamera.cullingMask = -2049;
					MainCamera.clearFlags = CameraClearFlags.Skybox;
					if (hascams)
					{
						camleft.cullingMask = -2049;
						camleft.clearFlags = CameraClearFlags.Skybox;
						camright.cullingMask = -1025;
						camright.clearFlags = CameraClearFlags.Skybox;
					}
					changesettings = false;
				}
			}
			else
			{
				MainCamera.fieldOfView = 60f;
				if (isanaglyph)
				{
					camleft.fieldOfView = 60f;
					camright.fieldOfView = 60f;
				}
			}
		}
		if (Input.GetKey(KeyCode.E))
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
			if (screencam.fieldOfView > 10f)
			{
				screencam.fieldOfView -= 10f * Time.deltaTime * 3f;
			}
			else
			{
				screencam.fieldOfView = 10f;
			}
		}
		if (Input.GetKey(KeyCode.Q))
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
			if (screencam.fieldOfView < 60f)
			{
				screencam.fieldOfView += 10f * Time.deltaTime * 3f;
			}
			else
			{
				screencam.fieldOfView = 60f;
			}
		}
		remaining.GetComponent<GUIText>().text = Mathf.FloorToInt(myTimer).ToString();
	}
}
