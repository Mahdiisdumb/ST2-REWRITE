using UnityEngine;

public class Menu2FullScreen : MonoBehaviour
{
	public bool Value;

	public Texture2D trueTexture;

	public Texture2D falseTexture;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		Setup();
	}

	public void Setup()
	{
		int @int = PlayerPrefs.GetInt("tmpFullScreen");
		if (@int == 1)
		{
			Value = true;
		}
		else
		{
			Value = false;
		}
		if (Value)
		{
			GetComponent<Renderer>().material.mainTexture = trueTexture;
		}
		else
		{
			GetComponent<Renderer>().material.mainTexture = falseTexture;
		}
	}

	private void OnMouseDown()
	{
		Value = !Value;
		if (Value)
		{
			GetComponent<Renderer>().material.mainTexture = trueTexture;
		}
		else
		{
			GetComponent<Renderer>().material.mainTexture = falseTexture;
		}
		if (Value)
		{
			PlayerPrefs.SetInt("tmpFullScreen", 1);
		}
		else
		{
			PlayerPrefs.SetInt("tmpFullScreen", 0);
		}
	}
}
