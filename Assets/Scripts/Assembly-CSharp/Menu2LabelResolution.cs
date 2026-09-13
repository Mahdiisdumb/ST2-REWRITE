using UnityEngine;

public class Menu2LabelResolution : MonoBehaviour
{
	private void Update()
	{
		int @int = PlayerPrefs.GetInt("tmpResolution");
		int num = 0;
		int num2 = 0;
		if (@int == 0)
		{
			num = 640;
			num2 = 480;
		}
		if (@int == 1)
		{
			num = 800;
			num2 = 480;
		}
		if (@int == 2)
		{
			num = 800;
			num2 = 600;
		}
		if (@int == 3)
		{
			num = 1024;
			num2 = 600;
		}
		if (@int == 4)
		{
			num = 1024;
			num2 = 768;
		}
		if (@int == 5)
		{
			num = 1280;
			num2 = 768;
		}
		if (@int == 6)
		{
			num = 1280;
			num2 = 960;
		}
		if (@int == 7)
		{
			num = 1360;
			num2 = 768;
		}
		if (@int == 8)
		{
			num = 1366;
			num2 = 768;
		}
		if (@int == 9)
		{
			num = 1600;
			num2 = 900;
		}
		GetComponent<TextMesh>().text = num + "/" + num2;
	}
}
