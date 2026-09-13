using UnityEngine;

public class Menu2OptionVideoApply : MonoBehaviour
{
	public Color overColor;

	public Color exitColor;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		int @int = PlayerPrefs.GetInt("qualitySettings");
		int int2 = PlayerPrefs.GetInt("fullScreen");
		int int3 = PlayerPrefs.GetInt("resolution");
		QualitySettings.SetQualityLevel(@int);
		int num = 0;
		int num2 = 0;
	}

	private void OnMouseDown()
	{
		int @int = PlayerPrefs.GetInt("tmpQualitySettings");
		int int2 = PlayerPrefs.GetInt("tmpFullScreen");
		int int3 = PlayerPrefs.GetInt("tmpResolution");
		PlayerPrefs.SetInt("qualitySettings", @int);
		PlayerPrefs.SetInt("fullScreen", int2);
		PlayerPrefs.SetInt("resolution", int3);
		QualitySettings.SetQualityLevel(@int);
		int width = 0;
		int height = 0;
		if (int3 == 0)
		{
			width = 640;
			height = 480;
		}
		if (int3 == 1)
		{
			width = 800;
			height = 480;
		}
		if (int3 == 2)
		{
			width = 800;
			height = 600;
		}
		if (int3 == 3)
		{
			width = 1024;
			height = 600;
		}
		if (int3 == 4)
		{
			width = 1024;
			height = 768;
		}
		if (int3 == 5)
		{
			width = 1280;
			height = 768;
		}
		if (int3 == 6)
		{
			width = 1280;
			height = 960;
		}
		if (int3 == 7)
		{
			width = 1360;
			height = 768;
		}
		if (int3 == 8)
		{
			width = 1366;
			height = 768;
		}
		if (int3 == 9)
		{
			width = 1600;
			height = 900;
		}
		bool fullscreen = false;
		if (int2 == 1)
		{
			fullscreen = true;
		}
		Screen.SetResolution(width, height, fullscreen);
	}

	private void OnMouseOver()
	{
		GetComponent<Renderer>().material.color = overColor;
	}

	private void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = exitColor;
	}
}
