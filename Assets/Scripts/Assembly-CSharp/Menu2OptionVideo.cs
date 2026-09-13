using UnityEngine;

public class Menu2OptionVideo : MonoBehaviour
{
	public Color overColor;

	public Color exitColor;

	public GameObject resolutionSlider;

	public GameObject fullScreen;

	public GameObject qualityLevel;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
	}

	private void OnMouseDown()
	{
		int value = 2;
		if (PlayerPrefs.HasKey("qualitySettings"))
		{
			value = PlayerPrefs.GetInt("qualitySettings");
		}
		int value2 = 0;
		if (PlayerPrefs.HasKey("fullScreen"))
		{
			value2 = PlayerPrefs.GetInt("fullScreen");
		}
		int value3 = 4;
		if (PlayerPrefs.HasKey("resolution"))
		{
			value3 = PlayerPrefs.GetInt("resolution");
		}
		PlayerPrefs.SetInt("tmpQualitySettings", value);
		PlayerPrefs.SetInt("tmpFullScreen", value2);
		PlayerPrefs.SetInt("tmpResolution", value3);
		resolutionSlider.GetComponent<Menu2ResolutionHorizontalSlider>().Setup();
		fullScreen.GetComponent<Menu2FullScreen>().Setup();
		qualityLevel.GetComponent<Menu2QualityLevelHorizontalSlider>().Setup();
		GameObject.Find("root").GetComponent<Menu2Root>().Show("Option Video");
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
