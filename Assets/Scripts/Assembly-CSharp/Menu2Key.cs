using UnityEngine;

public class Menu2Key : MonoBehaviour
{
	public string keycode = string.Empty;

	private bool changeKeycode;

	private bool canChangeKeycode = true;

	public string saveName = string.Empty;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		Vector3 size = GetComponent<BoxCollider>().size;
		GetComponent<BoxCollider>().size = new Vector3(2.78f, size.y, size.z);
		GetComponent<BoxCollider>().center = new Vector3(1.33f, 0f, 0f);
		if (!PlayerPrefs.HasKey(saveName))
		{
			PlayerPrefs.SetString(saveName, keycode);
		}
		keycode = PlayerPrefs.GetString(saveName);
	}

	private void OnGUI()
	{
		if (changeKeycode)
		{
			GetComponent<TextMesh>().text = "...";
			if (Input.GetMouseButtonDown(0))
			{
				keycode = KeyCode.Mouse1.ToString();
				changeKeycode = false;
				canChangeKeycode = false;
				PlayerPrefs.SetString(saveName, keycode);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				keycode = KeyCode.Mouse2.ToString();
				changeKeycode = false;
				PlayerPrefs.SetString(saveName, keycode);
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				keycode = Event.current.keyCode.ToString();
				changeKeycode = false;
				PlayerPrefs.SetString(saveName, keycode);
			}
		}
		else
		{
			GetComponent<TextMesh>().text = keycode;
		}
	}

	private void OnMouseDown()
	{
		if (canChangeKeycode && !changeKeycode)
		{
			changeKeycode = true;
		}
		canChangeKeycode = true;
	}
}
