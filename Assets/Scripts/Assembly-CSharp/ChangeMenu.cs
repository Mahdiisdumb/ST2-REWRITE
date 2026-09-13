using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
	private void Update()
	{
	}

	private void OnGUI()
	{
		if (Application.loadedLevelName == "Menu 1")
		{
			if (GUI.Button(new Rect(0f, 0f, 140f, 40f), "3D Menu"))
			{
				Application.LoadLevel("Menu 2");
			}
		}
		else if (GUI.Button(new Rect(0f, 0f, 140f, 40f), "2D Menu"))
		{
			Application.LoadLevel("Menu 1");
		}
	}
}
